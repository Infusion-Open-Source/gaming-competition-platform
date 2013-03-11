
namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     Events processor interface
    /// </summary>
    public interface IEventProcessor
    {
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
        bool Process(Event e, IGameState currentState, IGameState nextState, out IEnumerable<Event> newEvents);

        #endregion
    }
}