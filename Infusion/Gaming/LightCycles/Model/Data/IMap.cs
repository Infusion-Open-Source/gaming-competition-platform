namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The Map interface.
    /// </summary>
    public interface IMap
    {
        #region Public Properties

        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the map locations.
        /// </summary>
        Location[,] Locations { get; }

        /// <summary>
        /// Gets the players locations.
        /// </summary>
        Dictionary<Player, Point> PlayerLocations { get; }

        /// <summary>
        /// Gets the players.
        /// </summary>
        List<Player> Players { get; }

        /// <summary>
        /// Gets the width of the map.
        /// </summary>
        int Width { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Clone the map.
        /// </summary>
        /// <returns>
        /// The cloned map <see cref="IMap"/>.
        /// </returns>
        IMap Clone();

        /// <summary>
        /// Gets zero state of the map. Creates T-1 map from initial map which is helpful to find out players initial directions.
        /// </summary>
        /// <returns>
        /// The cloned map <see cref="IMap"/>.
        /// </returns>
        IMap GetZeroStateMap();

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

        #endregion
    }
}