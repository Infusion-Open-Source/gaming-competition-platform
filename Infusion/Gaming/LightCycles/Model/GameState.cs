// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameState.cs" company="Infusion">
//    Copyright (C) 2013 Paweł Drozdowski
//
//    This file is part of LightCycles Game Engine.
//
//    LightCycles Game Engine is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    LightCycles Game Engine is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with LightCycles Game Engine.  If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
//   The game state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Model.Data;

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
        public GameState(int turn, IMap map)
        {
            if (turn < 0)
            {
                throw new ArgumentOutOfRangeException("turn");
            }

            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            this.Turn = turn;
            this.Map = map;
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
            foreach (Player player in this.Map.Players)
            {
                if (!previousState.Map.PlayersLocations.ContainsKey(player))
                {
                    throw new ArgumentException("prevMap is inavlid, unable to find current player in T-1 map");
                }

                this.Directions.Add(player, DirectionHelper.CheckDirection(this.Map.PlayersLocations[player], previousState.Map.PlayersLocations[player]));
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

            // get previous state
            this.TrailsAge = new Dictionary<Point, int>();
            foreach (var pair in previousState.TrailsAge)
            {
                this.TrailsAge.Add(pair.Key, pair.Value);
            }

            // update to current state
            for (int y = 0; y < this.Map.Height; y++)
            {
                for (int x = 0; x < this.Map.Width; x++)
                {
                    Location currentLocation = this.Map.Locations[x, y];
                    Location prevLocation = previousState.Map.Locations[x, y];
                    if (prevLocation.LocationType == LocationTypeEnum.Trail
                        && currentLocation.LocationType != LocationTypeEnum.Trail)
                    {
                        // remove trails not existing any more
                        this.TrailsAge.Remove(new Point(x, y));
                    }
                    else if (prevLocation.LocationType != LocationTypeEnum.Trail
                             && currentLocation.LocationType == LocationTypeEnum.Trail)
                    {
                        // add new trails with age 0
                        this.TrailsAge.Add(new Point(x, y), 0);
                    }
                }
            }

            // age trails
            var updated = new Dictionary<Point, int>();
            foreach (var pair in this.TrailsAge)
            {
                updated.Add(pair.Key, pair.Value + 1);
            }

            this.TrailsAge = updated;
        }

        #endregion
    }
}