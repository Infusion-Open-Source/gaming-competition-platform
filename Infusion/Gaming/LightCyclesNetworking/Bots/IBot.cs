using Infusion.Gaming.LightCycles.Model.State;
using System.Collections.Generic;
using Infusion.Gaming.LightCycles.Events;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Model.Data;

namespace Infusion.Gaming.LightCyclesNetworking.Bots
{
    /// <summary>
    /// interface for AI player
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Gets bot name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets list of players events
        /// </summary>
        /// <param name="player">Player to be controlled by AI</param>
        /// <param name="state">Current state of the game</param>
        /// <param name="map">map of the game</param>
        /// <returns>set of player events for given game state</returns>
        List<Event> GetPlayerEvents(Identity player, IGameState state, Map map);
    }
}
