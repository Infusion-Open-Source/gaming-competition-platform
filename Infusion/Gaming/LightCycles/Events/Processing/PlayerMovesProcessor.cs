// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerMovesProcessor.cs" company="Infusion">
//    Copyright (C) 2013 Paweł Drozdowski
//
//    This file is part of LightCycles Game Engine.
//
//    LightCycles Game Engine is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    LightCycles Game Engine is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with LightCycles Game Engine.  If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
//   Processor for player move events.
//   Reflects result of player move events.
//   I player collides with something then feeds with new collision events.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Drawing;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Defines;

namespace Infusion.Gaming.LightCycles.Events.Processing
{
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
            newEvents = new EventsCollection();
            var moveEvent = e as PlayerMoveEvent;
            if (moveEvent == null)
            {
                return false;
            }

            Point location = currentState.Map.PlayersLocations[moveEvent.Player];
            DirectionEnum direction = currentState.Directions[moveEvent.Player];
            Point newLocation = DirectionHelper.NextLocation(location, direction, moveEvent.Direction);
            EventsCollection events = new EventsCollection();
            if (nextState.Map.Locations[newLocation.X, newLocation.Y].LocationType == LocationTypeEnum.Space)
            {
                // player moves to new loaction
                nextState.Map.Locations[newLocation.X, newLocation.Y] = new Location(
                    LocationTypeEnum.Player, moveEvent.Player);
                nextState.Map.Locations[location.X, location.Y] = new Location(LocationTypeEnum.Trail, moveEvent.Player);
            }
            else if (nextState.Map.Locations[newLocation.X, newLocation.Y].LocationType == LocationTypeEnum.Trail
                     || nextState.Map.Locations[newLocation.X, newLocation.Y].LocationType == LocationTypeEnum.Wall)
            {
                // player-trail collision 
                // player-wall collision 
                events.Add(new PlayerCollisionEvent(moveEvent.Player));
            }
            else if (nextState.Map.Locations[newLocation.X, newLocation.Y].LocationType == LocationTypeEnum.Player)
            {
                // player-player collision 
                events.Add(new PlayerCollisionEvent(moveEvent.Player));
                events.Add(new PlayerCollisionEvent(nextState.Map.Locations[newLocation.X, newLocation.Y].Player));
            }

            ((EventsCollection)newEvents).AddRange(events);
            return true;
        }

        #endregion
    }
}