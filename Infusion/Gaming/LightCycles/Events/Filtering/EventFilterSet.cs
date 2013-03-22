
namespace Infusion.Gaming.LightCycles.Events.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     Event filter set
    /// </summary>
    internal class EventFilterSet : List<IEventFilter>, IEventFilter
    {
        #region Public Methods and Operators

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilterSet"/> class.
        /// </summary>
        public EventFilterSet()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilterSet"/> class.
        /// </summary>
        /// <param name="filters">set of filters</param>
        public EventFilterSet(IEnumerable<IEventFilter> filters)
            : base(filters)
        {
        }

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
            IList<Event> results = new List<Event>(events);
            foreach (IEventFilter filter in this)
            {
                results = filter.Filter(state, results);
            }

            return results;
        }

        #endregion
    }
}
