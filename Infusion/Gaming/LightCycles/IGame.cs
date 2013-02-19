namespace Infusion.Gaming.LightCycles
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The Game interface.
    /// </summary>
    public interface IGame
    {
        #region Public Properties

        /// <summary>
        /// Gets the current game state.
        /// </summary>
        IGameState CurrentState { get; }

        /// <summary>
        /// Gets the state of the game.
        /// </summary>
        GameStateEnum GameState { get; }

        /// <summary>
        /// Gets the previous game state.
        /// </summary>
        IGameState PreviousState { get; }

        /// <summary>
        /// Gets the result of the game.
        /// </summary>
        GameResultEnum Result { get; }

        /// <summary>
        /// Gets the game settings.
        /// </summary>
        Settings Settings { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Resets the game.
        /// </summary>
        void Reset();

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="players">
        /// The set of players taking part in the game.
        /// </param>
        /// <param name="initialMap">
        /// The initial map of the game.
        /// </param>
        /// <param name="settings">
        /// The settings of the game.
        /// </param>
        /// <returns>
        /// The initial state of the game <see cref="IGameState"/>.
        /// </returns>
        IGameState Start(IEnumerable<Player> players, IMap initialMap, Settings settings);

        /// <summary>
        /// Makes game step, transition form current game state to next one.
        /// </summary>
        /// <param name="gameEvents">
        /// The set of game events that occurs between game turns.
        /// </param>
        /// <returns>
        /// The state of the game after transition <see cref="IGameState"/>.
        /// </returns>
        IGameState Step(EventsCollection gameEvents);

        /// <summary>
        /// Stops the game.
        /// </summary>
        void Stop();

        #endregion
    }
}