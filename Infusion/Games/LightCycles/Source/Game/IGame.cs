namespace Infusion.Gaming.LightCycles
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Events.Filtering;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.State;
    using Infusion.Gaming.LightCycles.Serialization;

    /// <summary>
    /// The Game interface.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Gets game mode
        /// </summary>
        GameMode GameMode { get; }

        /// <summary>
        /// Gets game map provider
        /// </summary>
        GameInfo MapInfo { get; }

        /// <summary>
        /// Gets game map provider
        /// </summary>
        PlayerSetup PlayerSetup { get; }

        /// <summary>
        /// Gets game map provider
        /// </summary>
        Map Map { get; }

        /// <summary>
        /// Gets game map provider
        /// </summary>
        IMapProvider MapProvider { get; }

        /// <summary>
        /// Gets game map
        /// </summary>
        IGameSlotsPool SlotPool { get; }

        /// <summary>
        /// Gets the current game state.
        /// </summary>
        IGameState CurrentState { get; }

        /// <summary>
        /// Gets the next game state.
        /// </summary>
        IGameState NextState { get; }

        /// <summary>
        /// Gets the end conditions.
        /// </summary>
        IEndCondition EndCondition { get; }

        /// <summary>
        /// Gets the event processors collection.
        /// </summary>
        IEventProcessor EventProcessor { get; }

        /// <summary>
        /// Gets the event filter.
        /// </summary>
        IEventFilter EventFilter { get; }
        
        /// <summary>
        /// Gets the previous game state.
        /// </summary>
        IGameState PreviousState { get; }

        /// <summary>
        /// Gets the result of the game.
        /// </summary>
        GameResult Result { get; }
        
        /// <summary>
        /// Gets game winner
        /// </summary>
        Identity Winner { get; }

        /// <summary>
        /// Gets game winning team
        /// </summary>
        Identity WinningTeam { get; }
        
        /// <summary>
        /// The check of end conditions.
        /// </summary>
        void CheckEndConditions();

        /// <summary>
        /// Creates game initial state
        /// </summary>
        /// <returns>game initial state</returns>
        IGameState CreateInitialState();

        /// <summary>
        /// Creates next game state.
        /// </summary>
        /// <param name="gameEvents"> Collection of game events that affects the state. </param>
        /// <returns>created game state</returns>
        IGameState CreateNextState(IEnumerable<Event> gameEvents);
    }
}