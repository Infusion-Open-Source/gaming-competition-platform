namespace Infusion.Gaming.LightCycles.Model
{
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// The GameState interface.
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Gets the map.
        /// </summary>
        IMap Map { get; }

        /// <summary>
        /// Gets the players data.
        /// </summary>
        IPlayersData PlayersData { get; }

        /// <summary>
        /// Gets the turn.
        /// </summary>
        int Turn { get; }
    }
}