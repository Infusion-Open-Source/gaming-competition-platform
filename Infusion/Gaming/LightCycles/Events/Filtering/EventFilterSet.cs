// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventFilterSet.cs" company="Infusion">
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

using Infusion.Gaming.LightCycles.Model;

namespace Infusion.Gaming.LightCycles.Events.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///     Event filter set
    /// </summary>
    public class EventFilterSet : List<IEventFilter>, IEventFilter
    {
        #region Public Methods and Operators

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilterSet"/> class.
        /// </summary>
        public EventFilterSet()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilterSet"/> class.
        /// </summary>
        /// <param name="filters">set of filters</param>
        public EventFilterSet(IEnumerable<IEventFilter> filters)
            : base(filters)
        {
        }

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
        /// filteres events list
        /// </returns>
        public IList<Event> Filter(IGameState state, IEnumerable<Event> events)
        {            
            IList<Event> results = new List<Event>(events);
            foreach (IEventFilter filter in this)
            {
                results = filter.Filter(state, results);
            }

            return results;
        }

        #endregion
    }
}
