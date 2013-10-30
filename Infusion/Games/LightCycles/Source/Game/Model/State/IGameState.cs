namespace Infusion.Gaming.LightCycles.Model.State
{
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The GameState interface.
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Gets the turn.
        /// </summary>
        int Turn { get; }

        /// <summary>
        /// Gets game objects states
        /// </summary>
        Objects Objects { get; }
    }
}