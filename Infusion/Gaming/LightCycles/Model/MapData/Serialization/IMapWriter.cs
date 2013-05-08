namespace Infusion.Gaming.LightCycles.Model.MapData.Serialization
{
    /// <summary>
    /// Interface for map writer
    /// </summary>
    public interface IMapWriter
    {
        /// <summary>
        /// Writes the map
        /// </summary>
        /// <param name="map">map to be written</param>
        void Write(IMap map);
    }
}
