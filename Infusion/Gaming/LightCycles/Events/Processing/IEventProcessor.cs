using Infusion.Gaming.LightCycles.Model.State;

namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// Events processor interface
    /// </summary>
    public interface IEventProcessor
    {
        /// <summary>
        /// Process player move events
        /// </summary>
        /// <param name="e"> event to process </param>
        /// <param name="game"> game object </param>
        /// <param name="newEvents"> new events produced by processor </param>
        /// <returns> was event processed by processor </returns>
        bool Process(Event e, IGame game, out IEnumerable<Event> newEvents);
    }
}