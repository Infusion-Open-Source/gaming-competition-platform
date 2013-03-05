// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameState.cs" company="Infusion">
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
//   The GameState interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Model
{
    using System.Collections.Generic;
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The GameState interface.
    /// </summary>
    public interface IGameState
    {
        #region Public Properties

        /// <summary>
        ///     Gets the players directions.
        /// </summary>
        Dictionary<Player, DirectionEnum> Directions { get; }

        /// <summary>
        ///     Gets the map.
        /// </summary>
        IMap Map { get; }

        /// <summary>
        ///     Gets the trails age.
        /// </summary>
        Dictionary<Point, int> TrailsAge { get; }

        /// <summary>
        ///     Gets the turn.
        /// </summary>
        int Turn { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Updates direction on which players go to random values
        /// </summary>
        void RandomizePlayersDirection();

        /// <summary>
        /// Updates direction on which players go
        /// </summary>
        /// <param name="previousState">
        /// previous game state to compare to
        /// </param>
        void UpdatePlayersDirection(IGameState previousState);

        /// <summary>
        /// Updates age of players trails
        /// </summary>
        /// <param name="previousState">
        /// previous game state to compare to
        /// </param>
        void UpdateTrailsAge(IGameState previousState);

        #endregion
    }
}