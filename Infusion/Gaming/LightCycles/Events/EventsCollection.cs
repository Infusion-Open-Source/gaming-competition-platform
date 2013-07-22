using Infusion.Gaming.LightCycles.Model;

namespace Infusion.Gaming.LightCycles.Events
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The events collection.
    /// </summary>
    internal class EventsCollection : List<Event>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventsCollection" /> class.
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

        /// <summary>
        /// Gets the most recent event from collection.
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
        /// Gets players owning events.
        /// </summary>
        public List<Identity> Players
        {
            get
            {
                var results = new List<Identity>();
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

        /// <summary>
        /// Get collection filtered by specified player.
        /// </summary>
        /// <param name="player">
        /// The player for which events should be taken.
        /// </param>
        /// <returns>
        /// The filtered collection of players' events <see cref="EventsCollection"/>.
        /// </returns>
        public EventsCollection FilterBy(Identity player)
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
    }
}