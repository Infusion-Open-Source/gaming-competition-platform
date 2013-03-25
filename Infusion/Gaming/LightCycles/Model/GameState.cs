namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// The game state.
    /// </summary>
    public class GameState : IGameState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="turn">
        /// The turn.
        /// </param>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="playersData">
        /// The players data.
        /// </param>
        public GameState(int turn, IMap map, IPlayersData playersData)
        {
            if (turn < 0)
            {
                throw new ArgumentOutOfRangeException("turn");
            }

            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            if (playersData == null)
            {
                throw new ArgumentNullException("playersData");
            }

            this.Turn = turn;
            this.Map = map;
            this.PlayersData = playersData;
        }

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        public IMap Map { get; protected set; }

        /// <summary>
        /// Gets or sets the players data.
        /// </summary>
        public IPlayersData PlayersData { get; protected set; }
        
        /// <summary>
        /// Gets or sets the turn.
        /// </summary>
        public int Turn { get; protected set; }
    }
}