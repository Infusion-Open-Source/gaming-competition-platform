// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMap.cs" company="Infusion">
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
//   The Map interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    ///     The Map interface.
    /// </summary>
    public interface IMap
    {
        #region Public Properties

        /// <summary>
        ///     Gets the height of the map.
        /// </summary>
        int Height { get; }

        /// <summary>
        ///     Gets the map locations.
        /// </summary>
        Location[,] Locations { get; }

        /// <summary>
        ///     Gets the teams.
        /// </summary>
        List<char> Teams { get; }

        /// <summary>
        ///     Gets the players.
        /// </summary>
        List < Player > Players { get; }

        /// <summary>
        ///     Gets the players locations.
        /// </summary>
        Dictionary < Player,
        Point > PlayersLocations
        {
            get;
        }

        /// <summary>
        ///     Gets the width of the map.
        /// </summary>
        int Width 
        {
            get;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Clone the map.
        /// </summary>
        /// <returns>
        ///     The cloned map <see cref="IMap" />.
        /// </returns>
        IMap Clone 
        ()
        ;

        /// <summary>
        /// Removes specified player from the map.
        /// </summary>
        /// <param name="player">
        /// The player to be removed.
        /// </param>
        void RemovePlayer 
        (Player
        player)
        ;

        /// <summary>
        /// Removes specified players from the map.
        /// </summary>
        /// <param name="playersToRemove">
        /// The players to be removed.
        /// </param>
        void RemovePlayers 
        (IEnumerable < Player > playersToRemove);

        #endregion
    }
    }