namespace Infusion.Gaming.LightCycles.Bots
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// interface for AI player
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Gets assigned player
        /// </summary>
        Player Player { get; }

        /// <summary>
        /// Gets list of players events
        /// </summary>
        /// <param name="state">
        /// Current state of the game
        /// </param>
        /// <returns>set of player events for given game state</returns>
        List<Event> GetPlayerEvents(IGameState state);
    }
}
