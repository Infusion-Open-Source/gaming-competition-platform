namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;

    /// <summary>
    /// Event processor set
    /// </summary>
    internal class EventProcessorSet : List<IEventProcessor>, IEventProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorSet"/> class.
        /// </summary>
        public EventProcessorSet()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorSet"/> class.
        /// </summary>
        /// <param name="processors">set of filters</param>
        public EventProcessorSet(IEnumerable<IEventProcessor> processors)
            : base(processors)
        {
        }

        /// <summary>
        /// Process player move events
        /// </summary>
        /// <param name="e"> event to process </param>
        /// <param name="game"> game object </param>
        /// <param name="newEvents"> new events produced by processor </param>
        /// <returns> was event processed by processor </returns>
        public bool Process(Event e, IGame game, out IEnumerable<Event> newEvents)
        {
            foreach (IEventProcessor processor in this)
            {
                if (processor.Process(e, game, out newEvents))
                {
                    return true;
                }
            }

            newEvents = new List<Event>();
            return false;
        }
    }
}
