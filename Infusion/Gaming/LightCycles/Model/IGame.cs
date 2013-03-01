﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGame.cs" company="Infusion">
//    Copyright (C) 2013 Paweł Drozdowski
//
//    This file is part of LightCycles Game Engine.
//
//    LightCycles Game Engine is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    LightCycles Game Engine is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with LightCycles Game Engine.  If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
//   The Game interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Infusion.Gaming.LightCycles.Events.Filtering;
using Infusion.Gaming.LightCycles.Events.Processing;
using System.Collections.Generic;
using Infusion.Gaming.LightCycles.Conditions;
using Infusion.Gaming.LightCycles.Events;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Defines;

namespace Infusion.Gaming.LightCycles.Model
{
    /// <summary>
    ///     The Game interface.
    /// </summary>
    public interface IGame
    {
        #region Public Properties

        /// <summary>
        ///     Gets the current game state.
        /// </summary>
        IGameState CurrentState { get; }

        /// <summary>
        ///     Gets the end condition.
        /// </summary>
        IEndCondition EndCondition { get; }

        /// <summary>
        ///     Gets the event processor.
        /// </summary>
        IEventProcessor EventProcessor { get; }

        /// <summary>
        ///     Gets the event filter.
        /// </summary>
        IEventFilter EventFilter { get; }

        /// <summary>
        ///     Gets the state of the game.
        /// </summary>
        GameStateEnum State { get; }

        /// <summary>
        ///     Gets the game mode.
        /// </summary>
        GameModeEnum Mode { get; }

        /// <summary>
        ///     Gets the previous game state.
        /// </summary>
        IGameState PreviousState { get; }

        /// <summary>
        ///     Gets the result of the game.
        /// </summary>
        GameResultEnum Result { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Resets the game.
        /// </summary>
        void Reset();

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="mode">
        /// Game mode.
        /// </param>
        /// <param name="players">
        /// The set of players taking part in the game.
        /// </param>
        /// <param name="initialMap">
        /// The initial map of the game.
        /// </param>
        /// <param name="endCondition">
        /// Game end condition.
        /// </param>
        /// <param name="eventFilter">
        /// Game events filter.
        /// </param>
        /// <param name="eventProcessor">
        /// Game events processor.
        /// </param>
        /// <returns>
        /// The initial state of the game <see cref="IGameState"/>.
        /// </returns>
        IGameState Start(
            GameModeEnum mode, 
            IEnumerable<Player> players, 
            IMap initialMap, 
            IEndCondition endCondition, 
            IEventFilter eventFilter,
            IEventProcessor eventProcessor);

        /// <summary>
        /// Makes game step, transition form current game state to next one.
        /// </summary>
        /// <param name="gameEvents">
        /// The set of game events that occurs between game turns.
        /// </param>
        /// <returns>
        /// The state of the game after transition <see cref="IGameState"/>.
        /// </returns>
        IGameState Step(IEnumerable<Event> gameEvents);

        /// <summary>
        ///     Stops the game.
        /// </summary>
        void Stop();

        #endregion
    }
}