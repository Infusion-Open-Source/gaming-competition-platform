
using Infusion.Gaming.LightCycles.Model.Data;

namespace Infusion.Gaming.LightCycles.Events.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     Filter keeps events only from players that are still in game
    /// </summary>
    public class PlayersInGameFilter : IEventFilter
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
            foreach (Player player in state.PlayersData.Players)
            {
                results.AddRange(data.FilterBy(player));
            }

            return results;
        }

        #endregion
    }
}
