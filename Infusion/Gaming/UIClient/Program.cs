namespace Infusion.Gaming.LightCycles.UIClient
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net;
    using System.Threading;
    using Infusion.Gaming.LightCycles.UIClient.Data;
    using Infusion.Gaming.LightCyclesCommon.Networking;

    /// <summary>
    /// Program entry point
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// syncRoot for thread abort flag
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// thread aborting flag
        /// </summary>
        private static bool abortGameThread;

        /// <summary>
        /// Gets or sets a value indicating whether game thread should be aborted
        /// </summary>
        private static bool AbortGameThread
        {
            get
            {
                lock (SyncRoot)
                {
                    return abortGameThread;
                }
            }

            set
            {
                lock (SyncRoot)
                {
                    abortGameThread = value;
                }
            }
        }

        /// <summary>          
        /// The application entry point.
        /// </summary>          
        [STAThread]
        public static void Main()
        {
            Console.WriteLine("LightCycles Game Engine Server - Copyright (C) 2013 Paweł Drozdowski");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");

            // star view
            using (GameView view = new GameView())
            {
                // start looped game on a separate thread 
                AbortGameThread = false;
                System.Threading.Thread thread = new Thread(NetworkingThread);
                thread.Start(view);

                view.Run();

                // end game thread
                AbortGameThread = true;
                thread.Join();
            }
        }

        /// <summary>
        /// Thread gathering game broadcasts from network
        /// </summary>
        /// <param name="arg">thread argument</param>
        public static void NetworkingThread(object arg)
        {
            GameView view = (GameView)arg;
            var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);
            var listener = new BroadcastListener(endpoint);
            List<string> gameState = new List<string>();
            var messageParser = new MessageParser();
            var visualStateBuilder = new VisualStateBuilder();
            visualStateBuilder.EndPoint = endpoint.ToString();
            int ocupiedSlots;
            int totalSlots;
            int turn;
            int startsInCounter;
            string winningTeam;
            string winningPlayer;
            string gameMode;
            int mapWidth;
            int mapHeight;
            int numberOfPlayers;
            int numberOfTeams;

            List<int> processedTurns = new List<int>(); 

            while (true)
            {
                if (!view.IsInitialized)
                {
                    Thread.Sleep(10);
                    continue;
                }

                string message = listener.Receive();
                var windowRect = new RectangleF(0, 0, view.WindowWidth, view.WindowHeight);
                if (string.IsNullOrEmpty(message))
                {
                    continue;
                }

                if (messageParser.TryGetString(message, "[Initialize][Mode]", out gameMode))
                {
                    visualStateBuilder.GameMode = gameMode;
                }
                else if (messageParser.TryGetInt(message, "[Initialize][MapWidth]", out mapWidth))
                {
                    visualStateBuilder.MapWidth = mapWidth;
                }
                else if (messageParser.TryGetInt(message, "[Initialize][MapHeight]", out mapHeight))
                {
                    visualStateBuilder.MapHeight = mapHeight;
                }
                else if (messageParser.TryGetInt(message, "[Initialize][NumberOfPlayers]", out numberOfPlayers))
                {
                    visualStateBuilder.NumberOfPlayers = numberOfPlayers;
                }
                else if (messageParser.TryGetInt(message, "[Initialize][NumberOfTeams]", out numberOfTeams))
                {
                    visualStateBuilder.NumberOfTeams = numberOfTeams;
                }
                else if (messageParser.TryGetIntInt(message, "[Waiting for players][Slots]", "/", out ocupiedSlots, out totalSlots))
                {
                    view.UpdateVisualState(visualStateBuilder.GatheringPlayers(ocupiedSlots, totalSlots, windowRect));
                }
                else if (messageParser.TryGetInt(message, "[GameStart][In]", out startsInCounter))
                {
                    view.UpdateVisualState(visualStateBuilder.GameStarts(startsInCounter, windowRect));
                }
                else if (messageParser.TryGet(message, "[GameStart][Now]"))
                {
                    processedTurns.Clear();
                    view.UpdateVisualState(visualStateBuilder.GameStarts(0, windowRect));
                }
                else if (messageParser.TryGetInt(message, "[TurnStart]", out turn))
                {
                    gameState = new List<string>();
                    visualStateBuilder.Turn = turn;
                }
                else if (messageParser.TryGetInt(message, "[TurnEnd]", out turn))
                {
                    if (!processedTurns.Contains(turn))
                    {
                        // process each turn only once
                        processedTurns.Add(turn);
                        visualStateBuilder.Turn = turn;
                        view.UpdateVisualState(visualStateBuilder.GameTurn(gameState, windowRect));
                    }
                }
                else if (messageParser.TryGetString(message, "[GameEnd][Team wins]", out winningTeam))
                {
                    view.UpdateVisualState(visualStateBuilder.GameEnds(gameState, "team", winningTeam, windowRect));
                }
                else if (messageParser.TryGetString(message, "[GameEnd][Player wins]", out winningPlayer))
                {
                    view.UpdateVisualState(visualStateBuilder.GameEnds(gameState, "player", winningPlayer, windowRect));
                }
                else if (messageParser.TryGet(message, "[GameEnd][No winner]"))
                {
                    view.UpdateVisualState(visualStateBuilder.GameEnds(gameState, "no winner", string.Empty, windowRect));
                }
                else if (messageParser.TryGet(message, "[GameEnd][Terminated]"))
                {
                    view.UpdateVisualState(visualStateBuilder.GameEnds(gameState, "terminated", string.Empty, windowRect));
                }
                else
                {
                    gameState.AddRange(message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                }

                // exit networking thread
                if (AbortGameThread)
                {
                    break;
                }
            }

            listener.Close();
        }
    }
}
