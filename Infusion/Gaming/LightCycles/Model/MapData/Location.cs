
namespace Infusion.Gaming.LightCycles.Model.MapData
{
    /// <summary>
    ///     The location on the map.
    /// </summary>
    public abstract class Location
    {
        /// <summary>
        /// Is location passable
        /// </summary>
        public abstract bool IsPassable { get; }
    }
}