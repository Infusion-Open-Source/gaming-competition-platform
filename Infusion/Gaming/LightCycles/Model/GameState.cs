
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
            this.TrailsAge = new Dictionary<Point, int>();
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
        ///     Gets or sets the trails age.
        /// </summary>
        public Dictionary<Point, int> TrailsAge { get; protected set; }

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

        /// <summary>
        /// Updates age of players trails
        /// </summary>
        /// <param name="previousState">
        /// previous game state to compare to
        /// </param>
        public void UpdateTrailsAge(IGameState previousState)
        {
            if (previousState == null)
            {
                throw new ArgumentNullException("previousState");
            }

            this.TrailsAge = new Dictionary<Point, int>();
            for (int y = 0; y < this.Map.Height; y++)
            {
                for (int x = 0; x < this.Map.Width; x++)
                {
                    Point coordinates = new Point(x, y);
                    LocationData currentLocation = this.PlayersData[x, y];
                    if (currentLocation.PlayerDataType == PlayerDataTypeEnum.Trail)
                    {
                        int age = 1;
                        if (previousState.TrailsAge.ContainsKey(coordinates))
                        {
                            age = previousState.TrailsAge[coordinates] + 1;
                        }

                        this.TrailsAge.Add(new Point(x, y), age);
                    }
                }
            }
        }

        #endregion
    }
}