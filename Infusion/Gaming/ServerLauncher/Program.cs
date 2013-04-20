namespace Infusion.Gaming.ServerLauncher
{
    using System;
    using Infusion.Gaming.LightCycles;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Program main method.
        /// </summary>
        /// <param name="args">
        /// Program arguments.
        /// </param>
        private static void Main(string[] args)
        {
            Console.WriteLine("LightCycles Game Engine Server - Copyright (C) 2013 Paweł Drozdowski");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");

            const int NumberOfPlayers = 8;
            const int NumberOfTeams = 8;
            const GameModeEnum GameMode = GameModeEnum.FreeForAll;
            var gameInfo = new GameInfo(NumberOfPlayers, NumberOfTeams, GameMode, 50, 22);

            /*
             * TODO: Server should work in following cycle:
             * - waiting for clients mode, broadcasting info about clients connected and free slots on next game + game and players/teams assignemnts
             *   - after all slots are filled game starts in X seconds, counter is broadcasted
             *   - or when there are still empty slots after X seconds of wait timeout game fills empty places with bots and starts the game
             * - game runs as per normal
             * - at the end info about winner is broadcasted and persistes for x seconds
             * - start again
             */ 
            while (true)
            {
                var gameRunner = new GameRunner();
                gameRunner.StartGame(gameInfo);
                while (gameRunner.RunGame())
                {
                    System.Threading.Thread.Sleep(100);
                }

                gameRunner.EndGame();
                Console.ReadKey();
            }
        }
    }
}