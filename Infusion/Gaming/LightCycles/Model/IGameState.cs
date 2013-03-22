
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
        #region Public Properties

        /// <summary>
        ///     Gets the players directions.
        /// </summary>
        Dictionary<Player, DirectionEnum> Directions { get; }

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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Updates direction on which players go to random values
        /// </summary>
        void RandomizePlayersDirection();

        /// <summary>
        /// Updates direction on which players go
        /// </summary>
        /// <param name="previousState">
        /// previous game state to compare to
        /// </param>
        void UpdatePlayersDirection(IGameState previousState);
        
        #endregion
    }
}