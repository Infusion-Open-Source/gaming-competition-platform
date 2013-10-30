namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Events.Filtering;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Exceptions;
    using Infusion.Gaming.LightCycles.Extensions;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Data.GameObjects;
    using Infusion.Gaming.LightCycles.Model.State;
    using Infusion.Gaming.LightCycles.Serialization;
    using Infusion.Gaming.LightCycles.Util;
    using GameState = Infusion.Gaming.LightCycles.Model.State.GameState;

    /// <summary>
    /// The game.
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>
        /// <param name="mode">game mode</param>
        /// <param name="mapInfo">game map info</param>
        /// <param name="playerSetup">game player setup</param>
        public Game(GameMode mode, GameInfo mapInfo, PlayerSetup playerSetup)
        {
            this.Result = GameResult.Undefined;
            this.CurrentState = null;
            this.PreviousState = null;

            this.GameMode = mode;
            this.MapInfo = mapInfo;
            this.PlayerSetup = playerSetup;
            this.MapProvider = new MapProvider(mapInfo, playerSetup);
            this.Map = this.MapProvider.Provide();
            this.SlotPool = new GameSlotsPool(playerSetup, this.Map.StartLocations);

            // set end conditions
            var endConditions = new EndConditionSet();
            endConditions.Add(new EndCondition(new NumberOfPlayers(0), GameResult.FinishedWithoutWinner));
            switch (mode)
            {
                case GameMode.FreeForAll:
                    endConditions.Add(new EndCondition(new NumberOfPlayers(1), GameResult.FinshedWithWinner));
                    break;
                case GameMode.TeamDeathMatch:
                    endConditions.Add(new EndCondition(new NumberOfTeams(1), GameResult.FinshedWithWinners));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }

            this.EndCondition = endConditions;

            // set event fileters
            EventFilterSet eventFilters = new EventFilterSet();
            eventFilters.Add(new PlayersInGameFilter());
            eventFilters.Add(new PlayerRecentEventFilter());
            eventFilters.Add(new IdlePlayerMoveEventAppender(RelativeDirection.Undefined));
            this.EventFilter = eventFilters;

            // set processors
            EventProcessorSet eventProcesors = new EventProcessorSet();
            eventProcesors.Add(new EventLoggingProcessor(true));
            eventProcesors.Add(new PlayerMovesProcessor());
            eventProcesors.Add(new PlayerScoreProcessor(
                new Dictionary<PlayerScoreSource, int>
                    {
                        { PlayerScoreSource.CleanMoveScore, this.MapInfo.CleanMoveScore }, 
                        { PlayerScoreSource.TrailHitScore, this.MapInfo.TrailHitScore }, 
                        { PlayerScoreSource.LastManStandScore, this.MapInfo.LastManStandScore }
                    }));
            eventProcesors.Add(new PlayerCollisionProcessor());
            eventProcesors.Add(new TrailAgingProcessor(this.MapInfo.TrailAging));
            eventProcesors.Add(new GarbageProcessor(true));
            this.EventProcessor = eventProcesors;
        }

        /// <summary>
        /// Gets or sets game mode
        /// </summary>
        public GameMode GameMode { get; protected set; }

        /// <summary>
        /// Gets or sets game map info
        /// </summary>
        public GameInfo MapInfo { get; protected set; }

        /// <summary>
        /// Gets or sets game map
        /// </summary>
        public Map Map { get; protected set; }

        /// <summary>
        /// Gets or sets game players info
        /// </summary>
        public PlayerSetup PlayerSetup { get; protected set; }

        /// <summary>
        /// Gets or sets game map provider
        /// </summary>
        public IMapProvider MapProvider { get; protected set; }

        /// <summary>
        /// Gets or sets game map
        /// </summary>
        public IGameSlotsPool SlotPool { get; protected set; }

        /// <summary>
        /// Gets or sets the current game state.
        /// </summary>
        public IGameState CurrentState { get; protected set; }

        /// <summary>
        /// Gets or sets the next game state.
        /// </summary>
        public IGameState NextState { get; protected set; }

        /// <summary>
        /// Gets or sets the end conditions.
        /// </summary>
        public IEndCondition EndCondition { get; protected set; }

        /// <summary>
        /// Gets or sets the event processors collection.
        /// </summary>
        public IEventProcessor EventProcessor { get; protected set; }

        /// <summary>
        /// Gets or sets the event filter.
        /// </summary>
        public IEventFilter EventFilter { get; protected set; }

        /// <summary>
        /// Gets or sets the previous game state.
        /// </summary>
        public IGameState PreviousState { get; protected set; }

        /// <summary>
        /// Gets or sets the result of the game.
        /// </summary>
        public GameResult Result { get; protected set; }

        /// <summary>
        /// Gets game winner
        /// </summary>
        public Identity Winner
        {
            get
            {
                if (this.Result == GameResult.FinshedWithWinner)
                {
                    return this.CurrentState.Objects.LightCycles[0].Player;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets game winning team
        /// </summary>
        public Identity WinningTeam
        {
            get
            {
                if (this.Result == GameResult.FinshedWithWinners)
                {
                    return this.SlotPool.Slots.GetPlayersSlot(this.CurrentState.Objects.LightCycles[0].Player).Team;
                }

                return null;
            }
        }

        /// <summary>
        /// The check of end conditions.
        /// </summary>
        public void CheckEndConditions()
        {
            if (this.EndCondition.Check(this))
            {
                this.Result = this.EndCondition.Result;
            }
        }

        /// <summary>
        /// Creates game initial state
        /// </summary>
        /// <returns>game initial state</returns>
        public IGameState CreateInitialState()
        {
            // set LightCycles objects for assigned player slots on random directions
            Map map = this.MapProvider.Provide();
            GameObject[,] initialObjects = new GameObject[map.Width, map.Height];
            foreach (GameSlot slot in this.SlotPool.Slots)
            {
                if (slot.Player != null)
                {
                    initialObjects[slot.StartLocation.X, slot.StartLocation.Y] = new LightCycleBike(slot.StartLocation, slot.Player, DirectionHelper.RandomDirection());
                }
            }

            // run one turn
            this.Result = GameResult.Undefined;
            this.CurrentState = new GameState(0, new Objects(initialObjects, this.SlotPool));
            this.CreateNextState(new Event[] { });
            return this.CurrentState;
        }

        /// <summary>
        /// Creates next game state.
        /// </summary>
        /// <param name="gameEvents"> Collection of game events that affects the state. </param>
        /// <returns>created game state</returns>
        public IGameState CreateNextState(IEnumerable<Event> gameEvents)
        {
            this.NextState = new GameState(this.CurrentState.Turn + 1, this.CurrentState.Objects.Clone());
            var eventsToProcess = new Queue<Event>();
            eventsToProcess.Enqueue(new TickEvent(this.NextState.Turn));
            eventsToProcess.Enqueue(this.EventFilter.Filter(this.CurrentState, gameEvents));
            while (eventsToProcess.Count > 0)
            {
                Event e = eventsToProcess.Dequeue();
                IEnumerable<Event> newEvents;
                bool eventProcessed = this.EventProcessor.Process(e, this, out newEvents);
                eventsToProcess.Enqueue(newEvents);
                if (!eventProcessed)
                {
                    throw new GameException(string.Format("None of the processores have processed event: {0}", e));
                }
            }

            this.PreviousState = this.CurrentState;
            this.CurrentState = this.NextState;
            this.NextState = null;
            return this.CurrentState;
        }
    }
}