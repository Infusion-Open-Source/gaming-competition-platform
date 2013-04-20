namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// Event logging processor.
    /// Prints out event to console.
    /// </summary>
    public class EventLoggingProcessor : IEventProcessor
    {
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

        /// <summary>
        /// Gets or sets a value indicating whether processor is silent or not
        /// </summary>
        public bool IsSilent { get; protected set; }

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
    }
}