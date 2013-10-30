namespace Infusion.Gaming.LightCycles.Events.Filtering
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model.State;

    /// <summary>
    /// Events filter interface
    /// </summary>
    public interface IEventFilter
    {
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
        IList<Event> Filter(IGameState state, IEnumerable<Event> events);
    }
}