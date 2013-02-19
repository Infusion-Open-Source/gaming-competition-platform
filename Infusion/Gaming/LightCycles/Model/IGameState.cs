namespace Infusion.Gaming.LightCycles.Model
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The GameState interface.
    /// </summary>
    public interface IGameState
    {
        #region Public Properties

        /// <summary>
        /// Gets the players directions.
        /// </summary>
        Dictionary<Player, DirectionEnum> Directions { get; }

        /// <summary>
        /// Gets the map.
        /// </summary>
        IMap Map { get; }

        /// <summary>
        /// Gets the turn.
        /// </summary>
        int Turn { get; }

        #endregion
    }
}