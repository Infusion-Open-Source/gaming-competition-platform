namespace Infusion.Gaming.LightCycles.Serialization
{
    using Infusion.Gaming.LightCycles.Model.Data.MapObjects;

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
        /// <param name="data">data to be read</param>
        /// <returns>map that has been read</returns>
        MapLocation[,] Read(T data);
    }
}
