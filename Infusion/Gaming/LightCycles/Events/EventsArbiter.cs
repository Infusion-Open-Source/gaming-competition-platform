namespace Infusion.Gaming.LightCycles.Events
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The events arbiter.
    /// </summary>
    public class EventsArbiter
    {
        #region Public Methods and Operators

        /// <summary>
        /// Makes an arbitrage of a given set of players events. Only one valid event per player will be returned as a result.
        /// </summary>
        /// <param name="events">
        /// The set of gathered events to check.
        /// </param>
        /// <param name="playersInGame">
        /// The players in the game.
        /// </param>
        /// <returns>
        /// The set of valid players events <see cref="EventsCollection"/>.
        /// </returns>
        public EventsCollection Arbitrage(EventsCollection events, IEnumerable<Player> playersInGame)
        {
            if (events == null)
            {
                throw new ArgumentNullException("events");
            }

            if (playersInGame == null)
            {
                throw new ArgumentNullException("playersInGame");
            }

            var results = new EventsCollection();
            foreach (Player player in playersInGame)
            {
                Event e = events.FilterBy(player).MostRecent;
                if (e == null)
                {
                    e = new PlayerMoveEvent(player, RelativeDirectionEnum.Undefined);
                }

                results.Add(e);
            }

            return results;
        }

        #endregion
    }
}