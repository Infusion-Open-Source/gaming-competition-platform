
namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The game state.
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
            this.Directions = new Dictionary<Player, DirectionEnum>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the players directions.
        /// </summary>
        public Dictionary<Player, DirectionEnum> Directions { get; protected set; }

        /// <summary>
        ///     Gets or sets the map.
        /// </summary>
        public IMap Map { get; protected set; }

        /// <summary>
        ///     Gets or sets the players data.
        /// </summary>
        public IPlayersData PlayersData { get; protected set; }
        
        /// <summary>
        ///     Gets or sets the turn.
        /// </summary>
        public int Turn { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Updates direction on which players go to random values
        /// </summary>
        public void RandomizePlayersDirection()
        {
            this.Directions = new Dictionary<Player, DirectionEnum>();
            foreach (Player player in this.PlayersData.Players)
            {
                this.Directions.Add(player, DirectionHelper.RandomDirection());
            }
        }

        /// <summary>
        /// Updates direction on which players go
        /// </summary>
        /// <param name="previousState">
        /// previous game state to compare to
        /// </param>
        public void UpdatePlayersDirection(IGameState previousState)
        {
            if (previousState == null)
            {
                throw new ArgumentNullException("previousState");
            }

            this.Directions = new Dictionary<Player, DirectionEnum>();
            foreach (Player player in this.PlayersData.Players)
            {
                if (!previousState.PlayersData.PlayersLocations.ContainsKey(player))
                {
                    throw new ArgumentException("prevMap is inavlid, unable to find current player in T-1 map");
                }

                DirectionEnum direction = DirectionHelper.CheckDirection(this.PlayersData.PlayersLocations[player], previousState.PlayersData.PlayersLocations[player]);
                if(direction == DirectionEnum.Undefined)
                {
                    throw new GameException("player hasn't moved!");
                }

                this.Directions.Add(player, direction);
            }
        }

        #endregion
    }
}