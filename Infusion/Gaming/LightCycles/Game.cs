// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="Infusion">
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
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.EventProcessors;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    ///     The game.
    /// </summary>
    public class Game : IGame
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>
        public Game()
        {
            this.Reset();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the current game state.
        /// </summary>
        public IGameState CurrentState { get; protected set; }

        /// <summary>
        ///     Gets or sets the end conditions.
        /// </summary>
        public List<EndCondition> EndConditions { get; protected set; }

        /// <summary>
        ///     Gets or sets the event processors collection.
        /// </summary>
        public List<IEventProcessor> EventProcessors { get; protected set; }

        /// <summary>
        ///     Gets or sets the state of the game.
        /// </summary>
        public GameStateEnum GameState { get; protected set; }

        /// <summary>
        ///     Gets or sets the game mode.
        /// </summary>
        public GameModeEnum Mode { get; protected set; }

        /// <summary>
        ///     Gets or sets the previous game state.
        /// </summary>
        public IGameState PreviousState { get; protected set; }

        /// <summary>
        ///     Gets or sets the result of the game.
        /// </summary>
        public GameResultEnum Result { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Resets the game.
        /// </summary>
        public void Reset()
        {
            if (this.GameState == GameStateEnum.Running)
            {
                this.Stop();
            }

            this.Result = GameResultEnum.Undefined;
            this.GameState = GameStateEnum.Initializing;
            this.CurrentState = null;
            this.PreviousState = null;
        }

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
        /// <param name="endConditions">
        /// Game end conditions.
        /// </param>
        /// <param name="eventProcessors">
        /// Game events processors.
        /// </param>
        /// <returns>
        /// The initial state of the game <see cref="IGameState"/>.
        /// </returns>
        public IGameState Start(
            GameModeEnum mode, 
            IEnumerable<Player> players, 
            IMap initialMap, 
            IEnumerable<EndCondition> endConditions, 
            IEnumerable<IEventProcessor> eventProcessors)
        {
            if (this.GameState != GameStateEnum.Initializing)
            {
                throw new GameException("Can be started only while initializing");
            }

            if (players == null)
            {
                throw new ArgumentNullException("players");
            }

            if (initialMap == null)
            {
                throw new ArgumentNullException("initialMap");
            }

            if (endConditions == null)
            {
                throw new ArgumentNullException("endConditions");
            }

            if (eventProcessors == null)
            {
                throw new ArgumentNullException("eventProcessors");
            }

            this.Mode = mode;
            this.EndConditions = new List<EndCondition>(endConditions);
            this.EventProcessors = new List<IEventProcessor>(eventProcessors);
            this.CurrentState = this.CreateInitialState(players, initialMap);
            this.GameState = GameStateEnum.Running;
            return this.CurrentState;
        }

        /// <summary>
        /// Makes game step, transition form current game state to next one.
        /// </summary>
        /// <param name="gameEvents">
        /// The set of game events that occurs between game turns.
        /// </param>
        /// <returns>
        /// The state of the game after transition <see cref="IGameState"/>.
        /// </returns>
        public IGameState Step(IEnumerable<Event> gameEvents)
        {
            if (this.GameState != GameStateEnum.Running)
            {
                throw new GameException("Can step forward only when running");
            }

            this.TransitToNextState(gameEvents);
            this.CheckEndConditions();
            return this.CurrentState;
        }

        /// <summary>
        ///     Stops the game.
        /// </summary>
        public void Stop()
        {
            if (this.GameState != GameStateEnum.Running)
            {
                throw new GameException("Can be stopped only when running");
            }

            this.Result = GameResultEnum.Terminated;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The check of end conditions.
        /// </summary>
        protected void CheckEndConditions()
        {
            foreach (EndCondition endCondition in this.EndConditions)
            {
                if (endCondition.Check(this))
                {
                    this.Result = endCondition.Result;
                    this.GameState = GameStateEnum.Stopped;
                    break;
                }
            }
        }

        /// <summary>
        /// Creates initial state of the game.
        /// </summary>
        /// <param name="players">
        /// The initial set of players for the game.
        /// </param>
        /// <param name="initialMap">
        /// The initial game map.
        /// </param>
        /// <returns>
        /// The initial state of the game <see cref="IGameState"/>.
        /// </returns>
        protected IGameState CreateInitialState(IEnumerable<Player> players, IMap initialMap)
        {
            var playersInGame = new List<Player>(players);
            var playersToRemove = new List<Player>();
            foreach (Player player in initialMap.Players)
            {
                if (!playersInGame.Contains(player))
                {
                    playersToRemove.Add(player);
                }
            }

            initialMap.RemovePlayers(playersToRemove);
            var prevState = new GameState(0, initialMap.GetZeroStateMap());
            var initialState = new GameState(0, initialMap);
            initialState.UpdatePlayersDirection(prevState);
            initialState.UpdateTrailsAge(prevState);
            return initialState;
        }

        /// <summary>
        /// Transits game to the next state.
        /// </summary>
        /// <param name="gameEvents">
        /// Collection of game events that affects the state.
        /// </param>
        protected void TransitToNextState(IEnumerable<Event> gameEvents)
        {
            var nextState = new GameState(this.CurrentState.Turn + 1, this.CurrentState.Map.Clone());
            var eventsToProcess = new EventsCollection();
            eventsToProcess.Add(new TickEvent(nextState.Turn));
            eventsToProcess.AddRange(gameEvents);

            while (eventsToProcess.Count > 0)
            {
                foreach (IEventProcessor eventProcessor in this.EventProcessors)
                {
                    IEnumerable<Event> newEvents;
                    bool eventProcessed = eventProcessor.Process(
                        eventsToProcess[0], this.CurrentState, nextState, out newEvents);
                    eventsToProcess.AddRange(newEvents);
                    if (eventProcessed)
                    {
                        eventsToProcess.RemoveAt(0);
                        break;
                    }
                }
            }

            nextState.UpdatePlayersDirection(this.CurrentState);
            nextState.UpdateTrailsAge(this.CurrentState);
            this.PreviousState = this.CurrentState;
            this.CurrentState = nextState;
        }

        #endregion
    }
}