namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// Trail aging processor.
    /// Adds trail fading on time feature.
    /// Speed of trail fading can be controlled by parameter.
    /// This processors hooks up to new turn event so will always run once per game turn.
    /// </summary>
    public class TrailAgingProcessor : IEventProcessor
    {
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

        /// <summary>
        /// Gets or sets speed of trail fading
        /// </summary>
        public float FadingSpeed { get; protected set; }

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

            nextState.PlayersData.AgeTrails(tickEvent.Turn, this.FadingSpeed);
            
            // don't remove tick event from processing queue
            return false;
        }
    }
}