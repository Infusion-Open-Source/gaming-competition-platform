namespace Infusion.Gaming.LightCycles.Events.Filtering
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.State;

    /// <summary>
    /// Filter keeps events only from players that are still in game
    /// </summary>
    public class PlayersInGameFilter : IEventFilter
    {
        /// <summary>
        /// Filter game events
        /// </summary>
        /// <param name="state"> current game state </param>
        /// <param name="events"> events to filter </param>
        /// <returns> filters events list </returns>
        public IList<Event> Filter(IGameState state, IEnumerable<Event> events)
        {
            var data = new EventsCollection(events);
            var results = new List<Event>();
            foreach (Identity player in state.Objects.Players)
            {
                results.AddRange(data.FilterBy(player));
            }

            return results;
        }
    }
}
