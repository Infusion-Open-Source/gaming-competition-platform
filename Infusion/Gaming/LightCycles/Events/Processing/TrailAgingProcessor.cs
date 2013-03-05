// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrailAgingProcessor.cs" company="Infusion">
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
//   Trail aging processor.
//   Adds trail fading on time feature.
//   Speed of trail fading can be controlled by parameter.
//   This processors hooks up to new turn event so will always run once per game turn.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     Trail aging processor.
    ///     Adds trail fading on time feature.
    ///     Speed of trail fading can be controlled by parameter.
    ///     This processors hooks up to new turn event so will always run once per game turn.
    /// </summary>
    public class TrailAgingProcessor : IEventProcessor
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrailAgingProcessor"/> class.
        /// </summary>
        /// <param name="fadingSpeed">
        /// Speed of trail fading
        /// </param>
        public TrailAgingProcessor(float fadingSpeed)
        {
            if (fadingSpeed < 0 || fadingSpeed > 1)
            {
                throw new ArgumentOutOfRangeException("fadingSpeed");
            }

            this.FadingSpeed = fadingSpeed;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets speed of trail fading
        /// </summary>
        public float FadingSpeed { get; protected set; }

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
            var tickEvent = e as TickEvent;
            if (tickEvent == null)
            {
                return false;
            }

            foreach (var pair in currentState.TrailsAge)
            {
                if (pair.Value >= tickEvent.Turn * (1 - this.FadingSpeed))
                {
                    nextState.Map.Locations[pair.Key.X, pair.Key.Y] = new Location(LocationTypeEnum.Space);
                }
            }

            // don't remove tick event from processing queue
            return false;
        }

        #endregion
    }
}