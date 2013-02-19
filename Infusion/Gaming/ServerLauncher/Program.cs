namespace Infusion.Gaming.ServerLauncher
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Infusion.Gaming.LightCycles;
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        /// The random number generator.
        /// </summary>
        private static readonly Random Random = new Random(1);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The next random direction.
        /// </summary>
        /// <returns>
        /// The random direction <see cref="RelativeDirectionEnum"/>.
        /// </returns>
        public static RelativeDirectionEnum NextDirection()
        {
            int choice = Random.Next(4);
            if (choice == 0)
            {
                return RelativeDirectionEnum.Left;
            }

            if (choice == 1)
            {
                return RelativeDirectionEnum.StraightForward;
            }

            if (choice == 2)
            {
                return RelativeDirectionEnum.StraightForward;
            }

            if (choice == 3)
            {
                return RelativeDirectionEnum.Right;
            }

            return RelativeDirectionEnum.Undefined;
        }

        /// <summary>
        /// Plays random game.
        /// </summary>
        public static void PlayRandomGame()
        {
            // init
            var generator = new MapStreamGenerator();
            const int NumberOfPlayers = 8;
            string mapStream = generator.Generate(50, 20, NumberOfPlayers);

            var mapSerializer = new MapSerializer();
            IMap map = mapSerializer.Read(mapStream);
            var settings = new Settings(
                GameModeEnum.FreeForAll, 
                new List<EndCondition>
                    {
                        new EndCondition(new NumberOfPlayers(0), GameResultEnum.FinishedWithoutWinner), 
                        new EndCondition(new NumberOfPlayers(1), GameResultEnum.FinshedWithWinner)
                    });

            var game = new Game();

            // start
            game.Start(map.Players, map, settings);

            // do initial UI
            Console.Clear();
            Console.Write(mapSerializer.Write(game.CurrentState.Map));
            while (game.GameState == GameStateEnum.Running)
            {
                // tick
                var eventsArbiter = new EventsArbiter();
                var events = new EventsCollection();
                for (int p = 0; p < NumberOfPlayers; p++)
                {
                    events.Add(new PlayerMoveEvent(new Player((char)('A' + p)), NextDirection()));
                }

                game.Step(eventsArbiter.Arbitrage(events, game.CurrentState.Map.Players));

                // do the UI
                Console.Clear();
                Console.Write(mapSerializer.Write(game.CurrentState.Map));
                Thread.Sleep(100);
            }

            // end
            if (game.Result == GameResultEnum.FinshedWithWinner)
            {
                Console.WriteLine("End, winner is: " + game.CurrentState.Map.Players[0]);
            }

            if (game.Result == GameResultEnum.FinishedWithoutWinner)
            {
                Console.WriteLine("End, no winner");
            }

            if (game.Result == GameResultEnum.Terminated)
            {
                Console.WriteLine("Terminated");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Program main method.
        /// </summary>
        /// <param name="args">
        /// Program arguments.
        /// </param>
        private static void Main(string[] args)
        {
            while (true)
            {
                PlayRandomGame();
                Console.ReadKey();
            }
        }

        #endregion
    }
}