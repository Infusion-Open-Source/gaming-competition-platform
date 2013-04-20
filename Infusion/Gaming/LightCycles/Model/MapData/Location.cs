namespace Infusion.Gaming.LightCycles.Model.MapData
{
    /// <summary>
    /// The location on the map.
    /// </summary>
    public abstract class Location
    {
        /// <summary>
        /// Gets a value indicating whether location is passable
        /// </summary>
        public abstract bool IsPassable { get; }
    }
}