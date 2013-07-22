using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Data.MapObjects;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// Interface for map writer
    /// </summary>
    /// <typeparam name="T">type of input data</typeparam>
    public interface IMapWriter<out T>
        where T : class
    {
        /// <summary>
        /// Writes the map to string
        /// </summary>
        /// <param name="map">map to be written</param>
        T Write(Map map);
    }
}
