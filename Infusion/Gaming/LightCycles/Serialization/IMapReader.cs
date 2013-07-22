using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Data.MapObjects;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// Interface for map reader
    /// </summary>
    /// <typeparam name="T">type of input data</typeparam>
    public interface IMapReader<in T>
        where T : class
    {
        /// <summary>
        /// Read a map 
        /// </summary>
        /// <returns>map that has been read</returns>
        MapLocation[,] Read(T data);
    }
}
