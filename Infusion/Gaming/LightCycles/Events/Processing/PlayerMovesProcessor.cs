namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// Processor for player move events.
    /// Reflects result of player move events.
    /// I player collides with something then feeds with new collision events.
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
                Point location = currentState.PlayersData.PlayersLocations[moveEvent.Player];
                DirectionEnum direction = currentState.PlayersData.PlayersLightCycles[moveEvent.Player].Direction;
                DirectionEnum newDirection = DirectionHelper.ChangeDirection(direction, moveEvent.Direction);
                Point newLocation = DirectionHelper.NextLocation(location, newDirection);
                if (nextState.Map[newLocation.X, newLocation.Y].IsPassable && nextState.PlayersData[newLocation.X, newLocation.Y] == null)
                {
                    // player moves to new loaction
                    nextState.PlayersData[newLocation.X, newLocation.Y] = new LightCycleBike(moveEvent.Player, newDirection);
                    nextState.PlayersData[location.X, location.Y] = new Trail(moveEvent.Player, 1);
                }
                else
                {
                    // collision detected
                    if (!nextState.Map[newLocation.X, newLocation.Y].IsPassable)
                    {
                        // player-obstacle collision 
                        events.Add(new PlayerCollisionEvent(moveEvent.Player));
                    }
                    else if (nextState.PlayersData[newLocation.X, newLocation.Y] is Trail)
                    {
                        // player-trail collision 
                        events.Add(new PlayerCollisionEvent(moveEvent.Player));
                    }
                    else if (nextState.PlayersData[newLocation.X, newLocation.Y] is LightCycleBike)
                    {
                        // player-player collision 
                        events.Add(new PlayerCollisionEvent(moveEvent.Player));
                        events.Add(new PlayerCollisionEvent(((LightCycleBike)nextState.PlayersData[newLocation.X, newLocation.Y]).Player));
                    }
                }

                processed = true;
            }

            newEvents = events;
            return processed;
        }
    }
}