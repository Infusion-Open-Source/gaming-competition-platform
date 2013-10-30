namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Processor for player move events.
    /// Increases players score for each turn player is alive.
    /// </summary>
    public class PlayerScoreProcessor : IEventProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerScoreProcessor" /> class.
        /// </summary>
        /// <param name="scoreMap">score map</param>
        public PlayerScoreProcessor(IDictionary<PlayerScoreSource, int> scoreMap)
        {
            if (scoreMap == null)
            {
                throw new ArgumentNullException("scoreMap");
            }

            this.ScoreMap = scoreMap;
        }

        /// <summary>
        /// Gets or sets score map
        /// </summary>
        public IDictionary<PlayerScoreSource, int> ScoreMap { get; set; }

        /// <summary>
        /// Process player move events
        /// </summary>
        /// <param name="e"> event to process </param>
        /// <param name="game"> game object </param>
        /// <param name="newEvents"> new events produced by processor </param>
        /// <returns> was event processed by processor </returns>
        public bool Process(Event e, IGame game, out IEnumerable<Event> newEvents)
        {
            bool processed = false;
            EventsCollection events = new EventsCollection();
            var scoreEvent = e as PlayerScoreEvent;
            if (scoreEvent != null)
            {
                if (!this.ScoreMap.ContainsKey(scoreEvent.ScoreSource))
                {
                    throw new ArgumentOutOfRangeException("e");
                }

                game.PlayerSetup.Scoreboard[scoreEvent.Player] += this.ScoreMap[scoreEvent.ScoreSource];
                processed = true;
            }

            newEvents = events;
            return processed;
        }
    }
}