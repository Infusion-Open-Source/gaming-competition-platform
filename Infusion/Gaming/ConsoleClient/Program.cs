using System.Net;
using System;
using System.Threading;
using Infusion.Gaming.LightCyclesNetworking;
using Infusion.Gaming.LightCyclesNetworking.Response;
using Infusion.Networking.ControllingServer;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Networking.ControllingServer.Response;

namespace Infusion.Gaming.LightCycles.ConsoleClient
{
    /// <summary>
    /// Console client example application
    /// </summary>
    public class Program
    {
        public static Random Random = new Random();
        public static string PlayerName = "Player A";

        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">program arguments</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("LightCycles - Copyright (C) 2013 Paweł Drozdowski");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");
            
            //try
            //{
                //if (args.Length == 2)
                //{
                    // connect to controlling server
                    IControllerServerService controllerServerService = new ControllerServerServiceStub();

                    // IPAddress address = Dns.GetHostEntry(args[0]).AddressList[0];
                    // int port = int.Parse(args[1]);
                    // IControllerServerService controllerServerService = new ControllerServerService(new IPEndPoint(address, port));
                    controllerServerService.Connect();

                    // log in and get your player key
                    string playerKey = controllerServerService.JoinLobby(PlayerName);
                    
                    if(playerKey != null)
                    {
                        // wait for next game
                        NewGame newGame = null;
                        while (newGame == null)
                        {
                            newGame = controllerServerService.FindGame(playerKey);
                            Thread.Sleep(200);
                        }
                        
                        // connect to game server
                        IPAddress gameAddress = Dns.GetHostEntry(newGame.ServerAddress).AddressList[0];
                        IGameServerService gameServerService = new GameServerService(new IPEndPoint(gameAddress, newGame.ServerPort));
                        gameServerService.Connect();

                        // join the game 
                        if (gameServerService.JoinGame(playerKey, newGame.GameKey))
                        {
                            // get game details
                            GameDetails details = gameServerService.GetGameDetails(playerKey, newGame.GameKey);
                            int currentTurn = -1;
                            
                            while(true)
                            {
                                // check current state
                                GameStatus status = gameServerService.GetGameStatus(playerKey, newGame.GameKey);
                                if (status.State == GameState.Running && status.AmIAlive)
                                {
                                    // once per turn get game data and send an action
                                    if (currentTurn != status.TurnNumber)
                                    {
                                        GameData data = gameServerService.GetGameData(playerKey, newGame.GameKey);
                                        if (gameServerService.SendPlayerAction(playerKey, newGame.GameKey, CalculateNextAction(details, status, data)))
                                        {
                                            currentTurn = status.TurnNumber;
                                        }
                                    }
                                }
                                else if (status.State == GameState.Finished)
                                {
                                    GameEndResult endResult = gameServerService.GetGameEndResult(playerKey, newGame.GameKey);
                                    if (endResult.GameResult == GameResult.FinshedWithWinner)
                                    {
                                        Console.WriteLine(endResult.WinnerName);
                                    }
                                    else if (endResult.GameResult == GameResult.FinshedWithWinners)
                                    {
                                        Console.WriteLine(endResult.WinningTeamName);
                                    }
                                    else if (endResult.GameResult == GameResult.FinishedWithoutWinner)
                                    {
                                        Console.WriteLine("No winner");
                                    }
                                    else if (endResult.GameResult == GameResult.Terminated)
                                    {
                                        Console.WriteLine("Game terminated");
                                    }

                                    break;
                                }

                                Thread.Sleep(0);
                            }
                        }
                        
                        gameServerService.Disconnect();
                        controllerServerService.LeaveLobby(playerKey);
                    }

                    controllerServerService.Disconnect();
                //}
                //else
                //{
                //    Console.WriteLine("Invalid number of arguments, expected command line in following format:");
                //    Console.WriteLine("ConsoleClient.exe ControllingServerAddress ControllingServerPort");
                //    Console.WriteLine("example 1: ConsoleClient.exe 192.168.1.1 10001");
                //    Console.WriteLine("example 2: ConsoleClient.exe myServer.com 10001");
                //}
            //}
            //catch (Exception e)
            //{
            //    Console.Write(e);
            //}
        }

        /// <summary>
        /// Calucates next action for player, picks random action
        /// </summary>
        /// <param name="details">game details</param>
        /// <param name="state">game state</param>
        /// <param name="data">game data</param>
        /// <returns>direction in which player would like to move</returns>
        private static RelativeDirection CalculateNextAction(GameDetails details, GameStatus state, GameData data)
        {
            if (Random.NextDouble() < 0.5)
            {
                return RelativeDirection.Left;
            }
            
            if (Random.NextDouble() < 0.5)
            {
                return RelativeDirection.Right;
            }

            return RelativeDirection.StraightAhead;
        }
    }
}
