namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    /// <summary>
    /// Game state writer
    /// </summary>
    public interface IGameStateWriter
    {
        /// <summary>
        /// Writes game state
        /// </summary>
        /// <param name="gameState">game state to be written</param>
        void Write(IGameState gameState);
    }
}
