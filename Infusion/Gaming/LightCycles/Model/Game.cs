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

using Infusion.Gaming.LightCycles.Events.Filtering;
using Infusion.Gaming.LightCycles.Events.Processing;
using Infusion.Gaming.LightCycles.Extensions;
using System;
using System.Collections.Generic;
using Infusion.Gaming.LightCycles.Conditions;
using Infusion.Gaming.LightCycles.Events;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Defines;

namespace Infusion.Gaming.LightCycles.Model
{
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
        public IEndCondition EndCondition { get; protected set; }

        /// <summary>
        ///     Gets or sets the event processors collection.
        /// </summary>
        public IEventProcessor EventProcessor { get; protected set; }

        /// <summary>
        ///     Gets the event filter.
        /// </summary>
        public IEventFilter EventFilter { get; protected set; }

        /// <summary>
        ///     Gets or sets the state of the game.
        /// </summary>
        public GameStateEnum State { get; protected set; }

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
            if (this.State == GameStateEnum.Running)
            {
                this.Stop();
            }

            this.Result = GameResultEnum.Undefined;
            this.State = GameStateEnum.Initializing;
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
        public IGameState Start(
            GameModeEnum mode, 
            IEnumerable<Player> players, 
            IMap initialMap, 
            IEndCondition endCondition,
            IEventFilter eventFilter,
            IEventProcessor eventProcessor)
        {
            if (this.State != GameStateEnum.Initializing)
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

            if (endCondition == null)
            {
                throw new ArgumentNullException("endCondition");
            }

            if (eventFilter == null)
            {
                throw new ArgumentNullException("eventFilter");
            }

            if (eventProcessor == null)
            {
                throw new ArgumentNullException("eventProcessor");
            }

            this.Mode = mode;
            this.EndCondition = endCondition;
            this.EventFilter = eventFilter;
            this.EventProcessor = eventProcessor;
            this.CurrentState = this.CreateInitialState(players, initialMap);
            this.State = GameStateEnum.Running;
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
            if (this.State != GameStateEnum.Running)
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
            if (this.State != GameStateEnum.Running)
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
            if(this.EndCondition.Check(this))
            {
                this.Result = this.EndCondition.Result;
                this.State = GameStateEnum.Stopped;
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
            var givenPlayers = new List<Player>(players);
            var playersInGame = givenPlayers.Intersect(initialMap.Players);
            var playersToRemove = givenPlayers.Remove(playersInGame);
            
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
            var eventsToProcess = new Queue<Event>();
            eventsToProcess.Enqueue(new TickEvent(nextState.Turn));
            eventsToProcess.Enqueue(this.EventFilter.Filter(this.CurrentState, gameEvents));
            
            while (eventsToProcess.Count > 0)
            {
                Event e = eventsToProcess.Dequeue();
                IEnumerable<Event> newEvents;
                bool eventProcessed = this.EventProcessor.Process(e, this.CurrentState, nextState, out newEvents);
                eventsToProcess.Enqueue(newEvents);
                if (!eventProcessed)
                {
                    throw new GameException(string.Format("None of the processores have processed event: {0}", e));
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