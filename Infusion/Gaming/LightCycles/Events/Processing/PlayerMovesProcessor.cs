namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// Processor for player move events.
    /// Reflects result of player move events.
    /// If player collides with something then feeds with new collision events.
    /// </summary>
    public class PlayerMovesProcessor : IEventProcessor
    {
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
            bool processed = false;
            EventsCollection events = new EventsCollection();
            var moveEvent = e as PlayerMoveEvent;
            if (moveEvent != null)
            {
                var location = currentState.PlayersData.PlayersLocations[moveEvent.Player];
                var direction = currentState.PlayersData.PlayersLightCycles[moveEvent.Player].Direction;
                var newDirection = DirectionHelper.ChangeDirection(direction, moveEvent.Direction);
                PlayerMoveResult result = nextState.PlayersData.MovePlayer(moveEvent.Player, location, newDirection, currentState.Map);

                if (result.Result == MoveResultEnum.CollisionWithObstacle)
                {
                    events.Add(new PlayerCollisionEvent(moveEvent.Player));
                }
                else if (result.Result == MoveResultEnum.CollisionWithTrail)
                {
                    events.Add(new PlayerCollisionEvent(moveEvent.Player));
                }
                else if (result.Result == MoveResultEnum.CollisionWithPlayer)
                {
                    events.Add(new PlayerCollisionEvent(moveEvent.Player));
                    events.Add(new PlayerCollisionEvent(result.OwningPlayer));
                }
                
                processed = true;
            }

            newEvents = events;
            return processed;
        }
    }
}