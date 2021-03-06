﻿namespace Infusion.Gaming.LightCycles.Events.Filtering
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.State;

    /// <summary>
    /// Adds undefined move event for players that are idle
    /// </summary>
    public class IdlePlayerMoveEventAppender : IEventFilter
    {
        /// <summary>
        /// Direction to append.
        /// </summary>
        private readonly RelativeDirection direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdlePlayerMoveEventAppender"/> class.
        /// </summary>
        /// <param name="direction">direction for the appended move event</param>
        public IdlePlayerMoveEventAppender(RelativeDirection direction)
        {
            this.direction = direction;
        }

        /// <summary>
        /// Filter game events
        /// </summary>
        /// <param name="state">
        /// current game state
        /// </param>
        /// <param name="events">
        /// events to filter
        /// </param>
        /// <returns>
        /// filters events list
        /// </returns>
        public IList<Event> Filter(IGameState state, IEnumerable<Event> events)
        {
            var data = new EventsCollection(events);
            foreach (Identity player in state.Objects.Players)
            {
                if (data.FilterBy(player).Count == 0)
                {
                    data.Add(new PlayerMoveEvent(player, this.direction));
                }
            }

            return data;
        }
    }
}
