using System.Drawing;
using System.Net;
using System.Threading;
using Infusion.Gaming.LightCycles.UIClient.Data;
using Infusion.Gaming.LightCyclesCommon.Definitions;
using Infusion.Gaming.LightCyclesCommon.Networking.Controlling;
using Infusion.Gaming.LightCyclesCommon.Networking.Controlling.Response;
using Infusion.Gaming.LightCyclesCommon.Networking.Game;
using Infusion.Gaming.LightCyclesCommon.Networking.Game.Response;

namespace Infusion.Gaming.LightCycles.UIClient
{
    using System;

    /// <summary>
    /// Program entry point
    /// </summary>
    public static class Program
    {
        public static IPAddress Address;
        public static int Port;

        /// <summary>          
        /// The application entry point.
        /// </summary>          
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("LightCycles - Copyright (C) 2013 Paweł Drozdowski");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");

            if (args.Length == 2)
            {
                Address = Dns.GetHostEntry(args[0]).AddressList[0];
                Port = int.Parse(args[1]);

                try
                {
                    using (GameView view = new GameView())
                    {
                        Thread netThread = new Thread(NetThread);
                        netThread.Start(view);
                        view.Run();
                        netThread.Abort();
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
            }
            else
            {
                Console.WriteLine("Invalid number of arguments, expected command line in following format:");
                Console.WriteLine("UIClient.exe GameServerAddress GameServerPort");
                Console.WriteLine("example 1: UIClient.exe 192.168.1.1 10001");
                Console.WriteLine("example 2: UIClient.exe myServer.com 10001");
            }
        }

        public static void NetThread(object arg)
        {
            GameView view = (GameView)arg;
            GameServerClient gameServerClient = new GameServerClient(new IPEndPoint(Address, Port));

            try
            {
                // connect to game server
                gameServerClient.Client.Connect();

                // join the game 
                string spectatorKey = gameServerClient.SpectateGame();
                if (!string.IsNullOrEmpty(spectatorKey))
                {
                    // get game details
                    GameDetails details = gameServerClient.GetGameDetails(spectatorKey, string.Empty);
                    int currentTurn = -1;

                    while (true)
                    {
                        // check current state
                        var visualStateBuilder = new VisualStateBuilder(gameServerClient.Client.RemoteEndPointName, new RectangleF(0, 0, view.WindowWidth, view.WindowHeight));
                        VisualState visualState = null;
                        GameStatus status = gameServerClient.GetGameStatus(spectatorKey, string.Empty);
                        if (status.State == GameState.Starting)
                        {
                            visualState = visualStateBuilder.BuildStartVisualState(details, status);
                        }
                        else if (status.State == GameState.Running)
                        {
                            // once per turn get game data and render
                            if (currentTurn != status.TurnNumber)
                            {
                                GameData data = gameServerClient.GetGameData(spectatorKey, string.Empty);
                                visualState = visualStateBuilder.BuildTurnVisualState(details, status, data);
                                currentTurn = status.TurnNumber;
                            }
                        }
                        else if (status.State == GameState.Finished)
                        {
                            GameEndResult endResult = gameServerClient.GetGameEndResult(spectatorKey, string.Empty);
                            visualState = visualStateBuilder.BuildFinishedVisualState(details, status, endResult);
                        }

                        if (visualState != null)
                        {
                            view.UpdateVisualState(visualState);
                        }
                        Thread.Sleep(50);
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            finally
            {
                gameServerClient.Client.Disconnect();
            }
        }
    }
}
