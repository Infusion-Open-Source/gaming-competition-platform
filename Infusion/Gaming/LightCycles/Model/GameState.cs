namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The game state.
    /// </summary>
    public class GameState : IGameState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="turn">
        /// The turn.
        /// </param>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="directions">
        /// The players directions.
        /// </param>
        public GameState(int turn, IMap map, Dictionary<Player, DirectionEnum> directions)
        {
            if (turn < 0)
            {
                throw new ArgumentOutOfRangeException("turn");
            }

            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            if (directions == null)
            {
                throw new ArgumentNullException("directions");
            }

            this.Turn = turn;
            this.Map = map;
            this.Directions = new Dictionary<Player, DirectionEnum>(directions);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the players directions.
        /// </summary>
        public Dictionary<Player, DirectionEnum> Directions { get; protected set; }

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        public IMap Map { get; protected set; }

        /// <summary>
        /// Gets or sets the turn.
        /// </summary>
        public int Turn { get; protected set; }

        #endregion
    }
}