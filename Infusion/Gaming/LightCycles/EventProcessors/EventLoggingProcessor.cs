// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventLoggingProcessor.cs" company="Infusion">
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
//   Event logging processor.
//   Prints out event to console.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.EventProcessors
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     Event logging processor.
    ///     Prints out event to console.
    /// </summary>
    public class EventLoggingProcessor : IEventProcessor
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLoggingProcessor"/> class.
        /// </summary>
        /// <param name="silent">
        /// flag whether processor is silent or not
        /// </param>
        public EventLoggingProcessor(bool silent)
        {
            this.IsSilent = silent;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether processor is silent or not
        /// </summary>
        public bool IsSilent { get; protected set; }

        #endregion

        #region Public Methods and Operators

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
            newEvents = new EventsCollection();
            if (!this.IsSilent)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        #endregion
    }
}