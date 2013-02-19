namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The game.
    /// </summary>
    public class Game : IGame
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.Reset();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the current game state.
        /// </summary>
        public IGameState CurrentState { get; protected set; }

        /// <summary>
        /// Gets or sets the state of the game.
        /// </summary>
        public GameStateEnum GameState { get; protected set; }

        /// <summary>
        /// Gets or sets the previous game state.
        /// </summary>
        public IGameState PreviousState { get; protected set; }

        /// <summary>
        /// Gets or sets the result of the game.
        /// </summary>
        public GameResultEnum Result { get; protected set; }

        /// <summary>
        /// Gets or sets the game settings.
        /// </summary>
        public Settings Settings { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Resets the game.
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
        public IGameState Start(IEnumerable<Player> players, IMap initialMap, Settings settings)
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

            this.Settings = settings;
            this.CurrentState = this.CreateInitialState(players, initialMap);
            this.GameState = GameStateEnum.Running;
            return this.CurrentState;
        }

        /// <summary>
        /// Makes game step, transition form current game state to next one.
        /// </summary>
        /// <param name="gameEvents">
        /// The set of game events that occurs between game turns.
        /// </param>s
        /// <returns>
        /// The state of the game after transition <see cref="IGameState"/>.
        /// </returns>
        public IGameState Step(EventsCollection gameEvents)
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
        /// Stops the game.
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
        /// The check of end conditions.
        /// </summary>
        protected void CheckEndConditions()
        {
            foreach (EndCondition endCondition in this.Settings.EndConditions)
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
            IMap zeroStateMap = initialMap.GetZeroStateMap();

            var resolver = new PlayerDirectionResolver();
            Dictionary<Player, DirectionEnum> playersDirections = resolver.Resolve(initialMap, zeroStateMap);

            return new GameState(0, initialMap, playersDirections);
        }

        /// <summary>
        /// Transits game to the next state.
        /// </summary>
        /// <param name="gameEvents">
        /// Collection of game events that affects the state.
        /// </param>
        protected void TransitToNextState(EventsCollection gameEvents)
        {
            int nextTurn = this.CurrentState.Turn + 1;
            IMap nextMap = this.CurrentState.Map.Clone();

            // execute events and transit to next state
            var events = new EventsCollection();
            events.Add(new TickEvent(nextTurn));
            events.AddRange(gameEvents);
            while (events.Count > 0)
            {
                Event e = events[0];
                events.RemoveAt(0);
                Console.WriteLine(e);

                var moveEvent = e as PlayerMoveEvent;
                if (moveEvent != null)
                {
                    if (this.CurrentState.Map.Players.Contains(moveEvent.Player))
                    {
                        Point location = this.CurrentState.Map.PlayerLocations[moveEvent.Player];
                        var newLocation = new Point(location.X, location.Y);
                        DirectionEnum currentDirection = this.CurrentState.Directions[moveEvent.Player];

                        DirectionEnum newDirection = DirectionHelper.ChangeDirection(currentDirection, moveEvent.Direction);
                        switch (newDirection)
                        {
                            case DirectionEnum.Up:
                                newLocation.Y--;
                                break;
                            case DirectionEnum.Down:
                                newLocation.Y++;
                                break;
                            case DirectionEnum.Left:
                                newLocation.X--;
                                break;
                            case DirectionEnum.Right:
                                newLocation.X++;
                                break;
                        }

                        if (nextMap.Locations[newLocation.X, newLocation.Y].LocationType == LocationTypeEnum.Space)
                        {
                            nextMap.Locations[newLocation.X, newLocation.Y] = new Location(
                                LocationTypeEnum.Player, moveEvent.Player);
                            nextMap.Locations[location.X, location.Y] = new Location(
                                LocationTypeEnum.Trail, moveEvent.Player);
                        }
                        else
                        {
                            // collision 
                            events.Add(new PlayerCollisionEvent(moveEvent.Player));
                        }
                    }
                }

                var collisionEvent = e as PlayerCollisionEvent;
                if (collisionEvent != null)
                {
                    nextMap.RemovePlayer(collisionEvent.Player);
                }
            }

            this.PreviousState = this.CurrentState;
            var resolver = new PlayerDirectionResolver();
            Dictionary<Player, DirectionEnum> playersDirections = resolver.Resolve(nextMap, this.CurrentState.Map);
            this.CurrentState = new GameState(nextTurn, nextMap, playersDirections);
        }

        #endregion
    }
}