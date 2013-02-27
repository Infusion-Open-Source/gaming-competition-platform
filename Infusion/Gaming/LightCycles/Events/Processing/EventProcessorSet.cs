// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventProcessorSet.cs" company="Infusion">
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

using System.ComponentModel;
using Infusion.Gaming.LightCycles.Model;

namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///     Event processor set
    /// </summary>
    public class EventProcessorSet : List<IEventProcessor>, IEventProcessor
    {
        #region Public Methods and Operators

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorSet"/> class.
        /// </summary>
        public EventProcessorSet()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorSet"/> class.
        /// </summary>
        /// <param name="processors">set of filters</param>
        public EventProcessorSet(IEnumerable<IEventProcessor> processors)
            : base(processors)
        {
        }

        /// <summary>
        /// Process player move events
        /// </summary>
        /// <param name="e">
        /// event to process
        /// </param>
        /// <param name="currentState">
        /// current game state
        /// </param>
        /// <param name="nextState">
        /// next game state
        /// </param>
        /// <param name="newEvents">
        /// new events produced by processor
        /// </param>
        /// <returns>
        /// was event processed by processor
        /// </returns>
        public bool Process(Event e, IGameState currentState, IGameState nextState, out IEnumerable<Event> newEvents)
        {
            foreach (IEventProcessor processor in this)
            {
                if (processor.Process(e, currentState, nextState, out newEvents))
                    return true;
            }

            newEvents = new List<Event>();
            return false;
        }

        #endregion
    }
}
