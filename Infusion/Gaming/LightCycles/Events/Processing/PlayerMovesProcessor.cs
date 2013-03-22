﻿
namespace Infusion.Gaming.LightCycles.Events.Processing
{
    using System.Collections.Generic;
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     Processor for player move events.
    ///     Reflects result of player move events.
    ///     I player collides with something then feeds with new collision events.
    /// </summary>
    public class PlayerMovesProcessor : IEventProcessor
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
            bool processed = false;
            EventsCollection events = new EventsCollection();
            var moveEvent = e as PlayerMoveEvent;
            if (moveEvent != null)
            {
                Point location = currentState.PlayersData.PlayersLocations[moveEvent.Player];
                DirectionEnum direction = currentState.Directions[moveEvent.Player];
                Point newLocation = DirectionHelper.NextLocation(location, direction, moveEvent.Direction);
                if (nextState.Map[newLocation.X, newLocation.Y].IsPassable && nextState.PlayersData[newLocation.X, newLocation.Y].IsPassable)
                {
                    // player moves to new loaction
                    nextState.PlayersData[newLocation.X, newLocation.Y] = new LocationData(moveEvent.Player, PlayerDataTypeEnum.Player);
                    nextState.PlayersData[location.X, location.Y] = new LocationData(moveEvent.Player, PlayerDataTypeEnum.Trail);
                }
                else
                {
                    // collision detected
                    if (nextState.PlayersData[newLocation.X, newLocation.Y].PlayerDataType == PlayerDataTypeEnum.Trail)
                    {
                        // player-trail collision 
                        events.Add(new PlayerCollisionEvent(moveEvent.Player));
                    }
                    else if (nextState.PlayersData[newLocation.X, newLocation.Y].PlayerDataType == PlayerDataTypeEnum.Player)
                    {
                        // player-player collision 
                        events.Add(new PlayerCollisionEvent(moveEvent.Player));
                        events.Add(new PlayerCollisionEvent(nextState.PlayersData[newLocation.X, newLocation.Y].Player));
                    }
                    else if (nextState.Map[newLocation.X, newLocation.Y].LocationType == LocationTypeEnum.Wall)
                    {
                        // player-wall collision 
                        events.Add(new PlayerCollisionEvent(moveEvent.Player));
                    }
                }

                processed = true;
            }

            newEvents = events;
            return processed;
        }

        #endregion
    }
}