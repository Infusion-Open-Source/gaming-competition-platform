namespace Infusion.Gaming.LightCycles.Model.MapData
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The Map interface.
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the width of the map.
        /// </summary>
        int Width { get; }
        
        /// <summary>
        /// Gets the players starting locations.
        /// </summary>
        Dictionary<PlayersStartingLocation, Point> StartingLocations { get; }

        /// <summary>
        /// Get location type for specified coordinates
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>location type at specified point</returns>
        Location this[int x, int y] { get; }
    }
}
