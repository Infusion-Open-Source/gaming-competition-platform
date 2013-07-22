using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Model.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Infusion.Gaming.LightCycles.Events;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Util;

namespace Infusion.Gaming.LightCyclesNetworking.Bots
{
    /// <summary>
    /// Class of AlwaysLeftBot Bot - computer player preferring turning left whenever possible
    /// </summary>
    public class AlwaysLeftBot : IBot
    {
        /// <summary>
        /// The internal random number generator.
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlwaysLeftBot" /> class.
        /// </summary>
        public AlwaysLeftBot()
        {
            this.random = new Random((int)DateTime.Now.Ticks);
            Thread.Sleep(0);
        }
        
        /// <summary>
        /// Gets bot name
        /// </summary>
        public string Name
        {
            get
            {
                return "AlwaysLeft";
            }
        }

        /// <summary>
        /// Gets list of players events
        /// </summary>
        /// <param name="player">Player to be controlled by AI</param>
        /// <param name="state">Current state of the game</param>
        /// <param name="map">map of the game</param>
        /// <returns>set of player events for given game state</returns>
        public List<Event> GetPlayerEvents(Identity player, IGameState state, Map map)
        {
            return new List<Event>
                       {
                           new PlayerMoveEvent(player, this.GetNextDirection(player, state, map))
                       };
        }

        /// <summary>
        /// The next direction for this player.
        /// </summary>
        /// <param name="player">Player to be controlled by AI</param>
        /// <param name="state">Current state of the game</param>
        /// <param name="map">map of the game</param>
        /// <returns>The random direction <see cref="RelativeDirection"/>.
        /// </returns>
        public RelativeDirection GetNextDirection(Identity player, IGameState state, Map map)
        {
            if (!state.Objects.Players.Contains(player))
            {
                return RelativeDirection.Undefined;
            }

            var bike = state.Objects.FindLightCycle(player);

            // get possible directions
            var safeDirections = new List<RelativeDirection>();
            safeDirections.Add(RelativeDirection.Right);
            safeDirections.Add(RelativeDirection.Left);
            safeDirections.Add(RelativeDirection.StraightAhead);

            // remove unsafe
            for (int i = 0; i < safeDirections.Count; i++)
            {
                Direction newDirection = DirectionHelper.ChangeDirection(bike.Direction, safeDirections[i]);
                Point newLocation = DirectionHelper.NextLocation(bike.Location, newDirection);
                if (!map[newLocation].IsPassable || state.Objects[newLocation] != null)
                {
                    safeDirections.RemoveAt(i--);
                }
            }

            // is no way to go, go straight ahead
            if (safeDirections.Count == 0)
            {
                return RelativeDirection.Left;
            }

            // if can go left then always pick left
            if (safeDirections.Contains(RelativeDirection.Left))
            {
                return RelativeDirection.Left;
            }

            // pick one randomly 
            return safeDirections[this.random.Next(safeDirections.Count)];
        }
    }
}
