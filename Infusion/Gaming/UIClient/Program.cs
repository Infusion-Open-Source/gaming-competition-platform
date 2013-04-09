﻿namespace UIClient
{
    using System;
    using System.Threading;
    using Infusion.Gaming.LightCycles;
    using Infusion.Gaming.LightCycles.Model.Defines;
    using UIClient.Data;
    
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
                System.Threading.Thread thread = new Thread(GameThread);
                thread.Start(view);

                view.Run();

                // end game thread
                AbortGameThread = true;
                thread.Join();
            }
        }

        /// <summary>
        /// Thread playing game in a loop until aborted
        /// </summary>
        /// <param name="arg">thread argument</param>
        public static void GameThread(object arg)
        {
            GameView view = (GameView)arg;
            const int NumberOfPlayers = 8;
            const GameModeEnum GameMode = GameModeEnum.FreeForAll;
            while (true)
            {
                var visualStateBuilder = new VisualStateBuilder();
                var gameRunner = new GameRunner();
                gameRunner.ConsoleOutputEnabled = false;
                gameRunner.StartGame(NumberOfPlayers, GameMode);

                view.UpdateVisualState(visualStateBuilder.CreateVisualState(gameRunner.Game));
                while (gameRunner.RunGame())
                {
                    view.UpdateVisualState(visualStateBuilder.CreateVisualState(gameRunner.Game));
                    if (AbortGameThread)
                    {
                        break;
                    }
                    
                    System.Threading.Thread.Sleep(100);
                }

                view.UpdateVisualState(visualStateBuilder.CreateVisualState(gameRunner.Game));
                gameRunner.EndGame();
                if (AbortGameThread)
                {
                    break;
                }
                
                System.Threading.Thread.Sleep(1000);    
            }
        }
    }
}