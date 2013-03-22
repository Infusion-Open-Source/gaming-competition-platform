
using Infusion.Gaming.LightCycles.Model.Serialization;

namespace Infusion.Gaming.ServerLauncher
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;

    using Infusion.Gaming.LightCycles;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

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
            if (!state.PlayersData.Players.Contains(player))
            {
                return RelativeDirectionEnum.Undefined;
            }

            Point location = state.PlayersData.PlayersLocations[player];
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
                if (!state.Map[newLocation.X, newLocation.Y].IsPassable || !state.PlayersData[newLocation.X, newLocation.Y].IsPassable)
                {
                    safeDirections.RemoveAt(i--);
                }
            }

            // is no way to go, go straight ahead
            if (safeDirections.Count == 0)
            {
                return RelativeDirectionEnum.StraightForward;
            }

            // if can go streight, go streight - makes it look more fancy ;)
            if (safeDirections.Contains(RelativeDirectionEnum.StraightForward))
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
            const int NumberOfPlayers = 8;
            var game = new LightCyclesGame();
            var stateRenderer = new GameStateRenderer();
            game.StartOnRandomMap(NumberOfPlayers, GameModeEnum.FreeForAll);
            
            // do initial UI
            stateRenderer.Render(game.CurrentState);
            while (game.State == GameStateEnum.Running)
            {
                // tick
                var events = new List<Event>();
                for (int p = 0; p < NumberOfPlayers; p++)
                {
                    var player = new Player((char)('A' + p));
                    events.Add(new PlayerMoveEvent(player, NextDirection(player, game.CurrentState)));
                }

                game.Step(events);

                // do the UI
                stateRenderer.Render(game.CurrentState);
                Thread.Sleep(100);
            }

            // end
            if (game.Result == GameResultEnum.FinshedWithWinners)
            {
                Console.WriteLine("End, winning team is: " + game.CurrentState.PlayersData.Players[0].Team.Id);
            }

            if (game.Result == GameResultEnum.FinshedWithWinner)
            {
                Console.WriteLine("End, winner is: " + game.CurrentState.PlayersData.Players[0]);
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