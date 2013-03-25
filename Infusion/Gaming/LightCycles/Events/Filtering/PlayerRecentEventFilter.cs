using Infusion.Gaming.LightCycles.Model.Data;

namespace Infusion.Gaming.LightCycles.Events.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     Filter keeps only recent event from each player
    /// </summary>
    public class PlayerRecentEventFilter : IEventFilter
    {
        #region Public Methods and Operators

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
        /// filters events list
        /// </returns>
        public IList<Event> Filter(IGameState state, IEnumerable<Event> events)
        {
            var data = new EventsCollection(events);
            var results = new List<Event>();
            foreach (Player player in data.Players)
            {
                results.Add(data.FilterBy(player).MostRecent);
            }

            return results;
        }

        #endregion
    }
}
