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

            while (true)
            {
                var gameRunner = new GameRunner();
                gameRunner.StartGame(NumberOfPlayers, NumberOfTeams, GameMode);
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