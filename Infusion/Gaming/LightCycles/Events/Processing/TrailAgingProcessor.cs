
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