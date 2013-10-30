namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;

    /// <summary>
    /// Player collision event processor.
    /// When player collides with something then he/she is removed (with the trail) from the map.
    /// </summary>
    public class PlayerCollisionProcessor : IEventProcessor
    {
        /// <summary>
        /// Process player move events
        /// </summary>
        /// <param name="e"> event to process </param>
        /// <param name="game"> game object </param>
        /// <param name="newEvents"> new events produced by processor </param>
        /// <returns> was event processed by processor </returns>
        public bool Process(Event e, IGame game, out IEnumerable<Event> newEvents)
        {
            EventsCollection events = new EventsCollection();
            newEvents = events;

            var collisionEvent = e as PlayerCollisionEvent;
            if (collisionEvent == null)
            {
                return false;
            }

            game.NextState.Objects.RemovePlayer(collisionEvent.Player);
            if (game.NextState.Objects.Players.Count == 1)
            {
                events.Add(new PlayerScoreEvent(game.NextState.Objects.Players[0], PlayerScoreSource.LastManStandScore));
            }
            
            return true;
        }
    }
}