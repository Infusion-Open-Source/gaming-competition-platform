namespace Infusion.Gaming.LightCycles.Serialization
{
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.State;

    /// <summary>
    /// Game state writer
    /// </summary>
    /// <typeparam name="T">type of output data</typeparam>
    public interface IGameStateWriter<out T>
        where T : class
    {
        /// <summary>
        /// Writes game state to stream
        /// </summary>
        /// <param name="map">game map</param>
        /// <param name="gameState">game state to be written to stream</param>
        /// <returns>output with data</returns>
        T Write(Map map, IGameState gameState);
    }
}
