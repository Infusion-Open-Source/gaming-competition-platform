namespace Infusion.Gaming.ServerLauncher
{
    using System;
    using System.IO;
    using Infusion.Gaming.LightCycles;
    using Infusion.Gaming.LightCycles.Messaging;
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
            Console.WriteLine("LightCycles Game Engine Server - Original authors: 2013 Paweł Drozdowski, 2013 Cyryl Płotnicki-Chudyk");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");

            const string MapsPath = @"..\..\..\Maps";
            GameInfoCollection gameInfoCycle = new GameInfoCollection
            {
                new GameInfo(8, 8, GameModeEnum.FreeForAll, 50, 22),
                new GameInfo(8, 8, GameModeEnum.FreeForAll, Path.Combine(MapsPath, @"FreeForAll\infusion_logo.png")),
                new GameInfo(8, 8, GameModeEnum.FreeForAll, Path.Combine(MapsPath, @"FreeForAll\pac_man.png")),
                new GameInfo(8, 8, GameModeEnum.FreeForAll, Path.Combine(MapsPath, @"FreeForAll\pac_man2.png")),
                new GameInfo(8, 8, GameModeEnum.FreeForAll, Path.Combine(MapsPath, @"FreeForAll\spiral.png")),
                new GameInfo(8, 8, GameModeEnum.FreeForAll, Path.Combine(MapsPath, @"FreeForAll\world.png")),

                new GameInfo(8, 2, GameModeEnum.TeamDeathMatch, 50, 22),
                new GameInfo(8, 2, GameModeEnum.TeamDeathMatch, Path.Combine(MapsPath, @"TeamDeathmatch\infusion_logo.png")),
                new GameInfo(8, 2, GameModeEnum.TeamDeathMatch, Path.Combine(MapsPath, @"TeamDeathmatch\pac_man.png")),
                new GameInfo(8, 2, GameModeEnum.TeamDeathMatch, Path.Combine(MapsPath, @"TeamDeathmatch\pac_man2.png")),
                new GameInfo(8, 2, GameModeEnum.TeamDeathMatch, Path.Combine(MapsPath, @"TeamDeathmatch\spiral.png")),
                new GameInfo(8, 2, GameModeEnum.TeamDeathMatch, Path.Combine(MapsPath, @"TeamDeathmatch\world.png")),
            };

            while (true)
            {
                var gameRunner = new GameRunner(new MessageSinkSet
                    {
                        new GameStateConsoleWriter(),
                        new GameStateBroadcaster()
                    });
                
                gameRunner.InitilizeGame(gameInfoCycle.Cycle());
                gameRunner.GatherPlayers();
                gameRunner.StartGame();
                while (gameRunner.RunGame())
                {
                }

                gameRunner.EndGame();
            }
        }
    }
}