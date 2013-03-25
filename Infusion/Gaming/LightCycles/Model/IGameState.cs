
using Infusion.Gaming.LightCycles.Model.MapData;

namespace Infusion.Gaming.LightCycles.Model
{
    using System.Collections.Generic;
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The GameState interface.
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        ///     Gets the map.
        /// </summary>
        IMap Map { get; }

        /// <summary>
        ///     Gets the players data.
        /// </summary>
        IPlayersData PlayersData { get; }

        /// <summary>
        ///     Gets the turn.
        /// </summary>
        int Turn { get; }
        
    }
}