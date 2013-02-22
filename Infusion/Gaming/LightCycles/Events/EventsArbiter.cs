// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsArbiter.cs" company="Infusion">
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
//   The events arbiter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Events
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    ///     The events arbiter.
    /// </summary>
    public class EventsArbiter
    {
        #region Public Methods and Operators

        /// <summary>
        /// Makes an arbitrage of a given set of players events. Only one valid event per player will be returned as a result.
        /// </summary>
        /// <param name="events">
        /// The set of gathered events to check.
        /// </param>
        /// <param name="playersInGame">
        /// The players in the game.
        /// </param>
        /// <returns>
        /// The set of valid players events <see cref="EventsCollection"/>.
        /// </returns>
        public IEnumerable<Event> Arbitrage(IEnumerable<Event> events, IEnumerable<Player> playersInGame)
        {
            if (events == null)
            {
                throw new ArgumentNullException("events");
            }

            if (playersInGame == null)
            {
                throw new ArgumentNullException("playersInGame");
            }

            var results = new EventsCollection();
            var eventsToCheck = new EventsCollection(events);
            foreach (Player player in playersInGame)
            {
                Event e = eventsToCheck.FilterBy(player).MostRecent;
                if (e == null)
                {
                    e = new PlayerMoveEvent(player, RelativeDirectionEnum.Undefined);
                }

                results.Add(e);
            }

            return results;
        }

        #endregion
    }
}