namespace Infusion.Gaming.LightCycles.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// Class of RandomPig Bot - computer player that is choosing its direction randomly w/o any preference
    /// </summary>
    public class RandomPigBot : IBot
    {
        /// <summary>
        /// The internal random number generator.
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomPigBot" /> class.
        /// </summary>
        /// <param name="player">assigned player</param>
        public RandomPigBot(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            this.Player = player;
            this.random = new Random((int)DateTime.Now.Ticks);
            Thread.Sleep(0);
        }

        /// <summary>
        /// Gets or sets assigned player
        /// </summary>
        public Player Player { get; protected set; }

        /// <summary>
        /// Gets list of players events
        /// </summary>
        /// <param name="state">
        /// Current state of the game
        /// </param>
        /// <returns>set of player events for given game state</returns>
        public List<Event> GetPlayerEvents(IGameState state)
        {
            return new List<Event>
                       {
                           new PlayerMoveEvent(this.Player, this.GetNextDirection(state))
                       };
        }

        /// <summary>
        /// The next direction for this player.
        /// </summary>
        /// <param name="state">
        /// Current state of the game
        /// </param>
        /// <returns>
        /// The random direction <see cref="RelativeDirectionEnum"/>.
        /// </returns>
        public RelativeDirectionEnum GetNextDirection(IGameState state)
        {
            if (!state.PlayersData.Players.Contains(this.Player))
            {
                return RelativeDirectionEnum.Undefined;
            }

            Point location = state.PlayersData.PlayersLocations[this.Player];
            DirectionEnum direction = state.PlayersData.PlayersLightCycles[this.Player].Direction;

            // get possible directions
            var safeDirections = new List<RelativeDirectionEnum>();
            safeDirections.Add(RelativeDirectionEnum.Right);
            safeDirections.Add(RelativeDirectionEnum.Left);
            safeDirections.Add(RelativeDirectionEnum.StraightForward);

            // remove unsafe
            for (int i = 0; i < safeDirections.Count; i++)
            {
                DirectionEnum newDirection = DirectionHelper.ChangeDirection(direction, safeDirections[i]);
                Point newLocation = DirectionHelper.NextLocation(location, newDirection);
                if (!state.Map[newLocation.X, newLocation.Y].IsPassable || state.PlayersData[newLocation.X, newLocation.Y] != null)
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
            return safeDirections[this.random.Next(safeDirections.Count)];
        }
    }
}
