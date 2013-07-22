namespace Infusion.Gaming.LightCycles.Model
{
    /// <summary>
    /// Intereface for managing game slots. External access to game slots, player and team identities is read only 
    /// </summary>
    public interface IGameSlotsPool
    {
        /// <summary>
        /// Gets game slots
        /// </summary>
        GameSlotsCollection Slots { get; }
    }
}
