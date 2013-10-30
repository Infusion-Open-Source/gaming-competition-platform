namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Util;

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
        /// <param name="e"> event to process </param>
        /// <param name="game"> game object </param>
        /// <param name="newEvents"> new events produced by processor </param>
        /// <returns> was event processed by processor </returns>
        public bool Process(Event e, IGame game, out IEnumerable<Event> newEvents)
        {
            bool processed = false;
            EventsCollection events = new EventsCollection();
            var moveEvent = e as PlayerMoveEvent;
            if (moveEvent != null)
            {
                var bike = game.CurrentState.Objects.FindLightCycle(moveEvent.Player);
                var newDirection = DirectionHelper.ChangeDirection(bike.Direction, moveEvent.Direction);
                PlayerMoveResult result = game.NextState.Objects.MovePlayer(moveEvent.Player, bike.Location, newDirection, game.Map);

                if (result.Result == MoveResult.CollisionWithObstacle)
                {
                    events.Add(new PlayerCollisionEvent(moveEvent.Player));
                }
                else if (result.Result == MoveResult.CollisionWithTrail)
                {
                    // don't give points for hitting your own trail :)
                    if (result.OwningPlayer.Identifier != moveEvent.Player.Identifier) 
                    {
                        events.Add(new PlayerScoreEvent(result.OwningPlayer, PlayerScoreSource.TrailHitScore));
                    }

                    events.Add(new PlayerCollisionEvent(moveEvent.Player));
                }
                else if (result.Result == MoveResult.CollisionWithPlayer)
                {
                    events.Add(new PlayerCollisionEvent(moveEvent.Player));
                    events.Add(new PlayerCollisionEvent(result.OwningPlayer));
                }
                else if (result.Result == MoveResult.Successful)
                {
                    events.Add(new PlayerScoreEvent(bike.Player, PlayerScoreSource.CleanMoveScore));
                }

                processed = true;
            }

            newEvents = events;
            return processed;
        }
    }
}