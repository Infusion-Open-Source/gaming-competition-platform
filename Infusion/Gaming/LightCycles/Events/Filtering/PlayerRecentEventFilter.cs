﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerRecentEventFilter.cs" company="Infusion">
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
//   Events processor interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Events.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     Filter keeps only recent event from each player
    /// </summary>
    public class PlayerRecentEventFilter : IEventFilter
    {
        #region Public Methods and Operators

        /// <summary>
        /// Filter game events
        /// </summary>
        /// <param name="state">
        /// current game state
        /// </param>
        /// <param name="events">
        /// events to filter
        /// </param>
        /// <returns>
        /// filters events list
        /// </returns>
        public IList<Event> Filter(IGameState state, IEnumerable<Event> events)
        {
            var data = new EventsCollection(events);
            var results = new List<Event>();
            foreach (Player player in data.Players)
            {
                results.Add(data.FilterBy(player).MostRecent);
            }

            return results;
        }

        #endregion
    }
}
