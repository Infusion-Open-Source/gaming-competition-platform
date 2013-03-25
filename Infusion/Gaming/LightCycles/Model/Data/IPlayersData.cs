namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// interface for players data map 
    /// </summary>
    public interface IPlayersData
    {
        /// <summary>
        /// Gets the height of the players data map.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the width of the players data map.
        /// </summary>
        int Width { get; }
        
        /// <summary>
        /// Gets the teams.
        /// </summary>
        List<Team> Teams { get; }

        /// <summary>
        /// Gets the players.
        /// </summary>
        List<Player> Players { get; }

        /// <summary>
        /// Gets the players light cycles.
        /// </summary>
        Dictionary<Player, LightCycleBike> PlayersLightCycles { get; }

        /// <summary>
        /// Gets the players locations.
        /// </summary>
        Dictionary<Player, Point> PlayersLocations { get; }

        /// <summary>
        /// Get location data for specified location
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>location data at specified point</returns>
        GameObject this[int x, int y] { get; set; }

        /// <summary>
        /// Removes specified player from the map.
        /// </summary>
        /// <param name="player">
        /// The player to be removed.
        /// </param>
        void RemovePlayer(Player player);

        /// <summary>
        /// Removes specified players from the map.
        /// </summary>
        /// <param name="playersToRemove">
        /// The players to be removed.
        /// </param>
        void RemovePlayers(IEnumerable<Player> playersToRemove);
    }
}
