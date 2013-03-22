
namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     Player collision event processor.
    ///     When player collides with something then he/she is removed (with the trail) from the map.
    /// </summary>
    public class PlayerCollisionProcessor : IEventProcessor
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
        public bool Process(Event e, IGameState currentState, IGameState nextState, out IEnumerable<Event> newEvents)
        {
            newEvents = new EventsCollection();
            var collisionEvent = e as PlayerCollisionEvent;
            if (collisionEvent == null)
            {
                return false;
            }

            nextState.PlayersData.RemovePlayer(collisionEvent.Player);
            return true;
        }

        #endregion
    }
}