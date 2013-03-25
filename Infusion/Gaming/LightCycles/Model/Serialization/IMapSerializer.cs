namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// Interface for map serialization
    /// </summary>
    public interface IMapSerializer
    {
        /// <summary>
        /// Gets current map buffer width
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets current map buffer height
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Create map buffer
        /// </summary>
        /// <param name="width">width of the buffer</param>
        /// <param name="height">height of the buffer</param>
        void Create(int width, int height);

        /// <summary>
        /// Read entire map from buffer
        /// </summary>
        /// <returns>map read form buffer</returns>
        IMap Read();

        /// <summary>
        /// Write entire map to buffer
        /// </summary>
        /// <param name="map">map to be written</param>
        void Write(IMap map);

        /// <summary>
        /// Load data from the source into the buffer
        /// </summary>
        void Load();

        /// <summary>
        /// Save data from the buffer to the source
        /// </summary>
        void Save();
    }
}
