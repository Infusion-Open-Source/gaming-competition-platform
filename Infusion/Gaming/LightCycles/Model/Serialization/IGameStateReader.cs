namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    /// <summary>
    /// Game state reader interface
    /// </summary>
    public interface IGameStateReader
    {
        /// <summary>
        /// Reads game state
        /// </summary>
        /// <returns>Read game state</returns>
        IGameState Read();
    }
}
