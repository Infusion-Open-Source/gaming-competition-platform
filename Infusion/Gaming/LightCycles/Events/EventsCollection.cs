// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsCollection.cs" company="Infusion">
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
//   The events collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Events
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    ///     The events collection.
    /// </summary>
    internal class EventsCollection : List<Event>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventsCollection" /> class.
        /// </summary>
        public EventsCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsCollection"/> class.
        /// </summary>
        /// <param name="events">
        /// The set of events to add initially.
        /// </param>
        public EventsCollection(IEnumerable<Event> events)
            : base(events)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the most recent event from collection.
        /// </summary>
        public Event MostRecent
        {
            get
            {
                long timeStamp = long.MinValue;
                Event result = null;
                for (int i = this.Count - 1; i >= 0; i--)
                {
                    Event e = this[i];
                    if (e.TimeStamp > timeStamp)
                    {
                        timeStamp = e.TimeStamp;
                        result = e;
                    }
                }

                return result;
            }
        }

        /// <summary>
        ///     Gets players owning events.
        /// </summary>
        public List<Player> Players
        {
            get
            {
                var results = new List<Player>();
                foreach (Event e in this)
                {
                    var playerEvent = e as PlayerEvent;
                    if (playerEvent != null && !results.Contains(playerEvent.Player))
                    {
                        results.Add(playerEvent.Player);
                    }
                }

                return results;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Get collection filtered by specified player.
        /// </summary>
        /// <param name="player">
        /// The player for which events should be taken.
        /// </param>
        /// <returns>
        /// The filtered collection of players' events <see cref="EventsCollection"/>.
        /// </returns>
        public EventsCollection FilterBy(Player player)
        {
            var results = new EventsCollection();
            foreach (Event e in this)
            {
                var playerEvent = e as PlayerEvent;
                if (playerEvent != null && playerEvent.Player.Equals(player))
                {
                    results.Add(playerEvent);
                }
            }

            return results;
        }

        #endregion
    }
}