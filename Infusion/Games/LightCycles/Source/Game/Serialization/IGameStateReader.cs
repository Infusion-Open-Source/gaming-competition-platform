namespace Infusion.Gaming.LightCycles.Serialization
{
    using Infusion.Gaming.LightCycles.Model.State;

    /// <summary>
    /// Game state reader interface
    /// </summary>
    /// <typeparam name="T">type of input data</typeparam>
    public interface IGameStateReader<in T>
        where T : class
    {
        /// <summary>
        /// Reads game state
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>Read game state</returns>
        IGameState Read(T data);
    }
}
