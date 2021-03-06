﻿namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Garbage event processor.
    /// Catches all events that weren't processed by other event processors and removes it from the queue.
    /// Should be put at the end of processors queue.
    /// </summary>
    public class GarbageProcessor : IEventProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarbageProcessor"/> class.
        /// </summary>
        /// <param name="silent">
        /// flag whether processor is silent or not
        /// </param>
        public GarbageProcessor(bool silent)
        {
            this.IsSilent = silent;
        }

        /// <summary>
        ///  Gets or sets a value indicating whether processor is silent or not
        /// </summary>
        public bool IsSilent { get; protected set; }

        /// <summary>
        /// Process player move events
        /// </summary>
        /// <param name="e"> event to process </param>
        /// <param name="game"> game object </param>
        /// <param name="newEvents"> new events produced by processor </param>
        /// <returns> was event processed by processor </returns>
        public bool Process(Event e, IGame game, out IEnumerable<Event> newEvents)
        {
            newEvents = new EventsCollection();
            if (!this.IsSilent)
            {
                Console.WriteLine("No processor to pick up {0} event. Removed by garbage processor", e);
            }

            return true;
        }
    }
}