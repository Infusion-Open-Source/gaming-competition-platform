namespace Infusion.Gaming.LightCycles.Model.MapData.Serialization
{
    /// <summary>
    /// Interface for map reader
    /// </summary>
    public interface IMapReader
    {
        /// <summary>
        /// Read a map 
        /// </summary>
        /// <returns>map that has been read</returns>
        IMap Read();
    }
}
