namespace Infusion.Gaming.LightCycles.Model.Data.MapObjects
{
    using System.Drawing;

    /// <summary>
    /// The location on the map.
    /// </summary>
    public abstract class MapLocation
    {
        /// <summary>
        /// Gets or sets object location
        /// </summary>
        public Point Location { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether location is passable
        /// </summary>
        public abstract bool IsPassable { get; }
    }
}