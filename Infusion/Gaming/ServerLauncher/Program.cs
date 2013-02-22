// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Infusion" author="Paweł Drozdowski">
//    Copyright (C) 2013 Paweł Drozdowski
//
//    This file is part of LightCycles Game Engine Server.
//
//    LightCycles Game Engine Server is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    LightCycles Game Engine Server is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with LightCycles Game Engine Server.  If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Infusion.Gaming.ServerLauncher
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;

    using Infusion.Gaming.LightCycles;
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.EventProcessors;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        ///     The random number generator.
        /// </summary>
        private static readonly Random Random = new Random(1);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The next random direction.
        /// </summary>
        /// <param name="player">
        /// Player for which takes next direction
        /// </param>
        /// <param name="state">
        /// state of the game
        /// </param>
        /// <returns>
        /// The random direction <see cref="RelativeDirectionEnum"/>.
        /// </returns>
        public static RelativeDirectionEnum NextDirection(Player player, IGameState state)
        {
            if (!state.Map.Players.Contains(player))
            {
                return RelativeDirectionEnum.Undefined;
            }

            Point location = state.Map.PlayersLocations[player];
            DirectionEnum direction = state.Directions[player];

            // get possible directions
            var safeDirections = new List<RelativeDirectionEnum>();
            safeDirections.Add(RelativeDirectionEnum.Right);
            safeDirections.Add(RelativeDirectionEnum.Left);
            safeDirections.Add(RelativeDirectionEnum.StraightForward);

            // remove unsafe
            for (int i = 0; i < safeDirections.Count; i++)
            {
                Point newLocation = DirectionHelper.NextLocation(location, direction, safeDirections[i]);
                if (state.Map.Locations[newLocation.X, newLocation.Y].LocationType != LocationTypeEnum.Space)
                {
                    safeDirections.RemoveAt(i--);
                }
            }

            // is no way to go, go straight ahead
            if (safeDirections.Count == 0)
            {
                return RelativeDirectionEnum.StraightForward;
            }

            // pick one randomly 
            return safeDirections[Random.Next(safeDirections.Count)];
        }

        /// <summary>
        ///     Plays random game.
        /// </summary>
        public static void PlayRandomGame()
        {
            // init
            var generator = new MapStreamGenerator();
            const int NumberOfPlayers = 8;
            string mapStream = generator.Generate(50, 20, NumberOfPlayers);

            var mapSerializer = new MapSerializer();
            IMap map = mapSerializer.Read(mapStream);
            var game = new Game();

            // start
            game.Start(
                GameModeEnum.FreeForAll, 
                map.Players, 
                map, 
                new List<EndCondition>
                    {
                        new EndCondition(new NumberOfPlayers(0), GameResultEnum.FinishedWithoutWinner), 
                        new EndCondition(new NumberOfPlayers(1), GameResultEnum.FinshedWithWinner)
                    }, 
                new List<IEventProcessor>
                    {
                        new EventLoggingProcessor(true), 
                        new PlayerMovesProcessor(), 
                        new PlayerCollisionProcessor(), 
                        new TrailAgingProcessor(0.2f), 
                        new GarbageProcessor(true)
                    });

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
                    var player = new Player((char)('A' + p));
                    events.Add(new PlayerMoveEvent(player, NextDirection(player, game.CurrentState)));
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
            Console.WriteLine("LightCycles Game Engine Server - Copyright (C) 2013 Paweł Drozdowski");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine(
                "This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");

            while (true)
            {
                PlayRandomGame();
                Console.ReadKey();
            }
        }

        #endregion
    }
}