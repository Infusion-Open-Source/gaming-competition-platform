using System.Text;
using Infusion.Gaming.LightCycles;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Logs;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using Infusion.Gaming.LightCycles.Events;
using Infusion.Gaming.LightCyclesNetworking.Bots;

namespace Infusion.Gaming.LightCyclesNetworking.NewFolder1
{/*
    /// <summary>
    /// Runs game in a loop
    /// </summary>
    public class GameRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRunner" /> class.
        /// </summary>
        /// <param name="log">message sink</param>
        public GameRunner(ILog log)
        {
            if (log == null)
            {
                throw new ArgumentNullException("log");
            }

            this.Log = log;
            this.MapsRootPath = @"..\..\..\Maps";
            this.Parameters = new GameParameters();
            this.GamesCycle = new GameInfoCycle();
            this.Lobby = new GameLobby();
            this.BotFactory = new BotFactory();
            this.GameEvents = new EventsCollection();
            this.Phase = GameRunnerPhase.Lobby;
            
            // add deafult game cycle
            //this.GamesCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, 50, 22));
            //this.GamesCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\infusion_logo.png")));
            //this.GamesCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\pac_man.png")));
            //this.GamesCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\pac_man2.png")));
            //this.GamesCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\spiral.png")));
            //this.GamesCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\world.png")));

            this.GamesCycle.Add(new GameMapInfo(8, 2, GameMode.TeamDeathMatch, 50, 22));
            //this.GamesCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\infusion_logo.png")));
            //this.GamesCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\pac_man.png")));
            //this.GamesCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\pac_man2.png")));
            //this.GamesCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\spiral.png")));
            //this.GamesCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\world.png")));
        }

        /// <summary>
        /// Gets or sets maps root path
        /// </summary>
        public string MapsRootPath { get; set; }

        /// <summary>
        /// Gets or sets game paramters
        /// </summary>
        public GameParameters Parameters { get; protected set; }

        /// <summary>
        /// Gets or sets game cycle
        /// </summary>
        public GameInfoCycle GamesCycle { get; protected set; }

        /// <summary>
        /// Gets or sets game lobby
        /// </summary>
        public GameLobby Lobby { get; protected set; }
        
        /// <summary>
        /// Gets or sets bot factory
        /// </summary>
        public BotFactory BotFactory { get; protected set; }

        /// <summary>
        /// Gets or sets game events collection factory
        /// </summary>
        public List<Event> GameEvents { get; protected set; }

        /// <summary>
        /// Current game runer phase
        /// </summary>
        public GameRunnerPhase Phase { get; set; }
        
        /// <summary>
        /// Gets or sets game main object
        /// </summary>
        public IGame Game { get; protected set; }

        /// <summary>
        /// Gets or set game renur delayed action
        /// </summary>
        public DelayedAction Delay { get; protected set; }

        /// <summary>
        /// Gets or sets logging interface
        /// </summary>
        public ILog Log { get; protected set; }

        /// <summary>
        /// Terminate currently played game
        /// </summary>
        public void TerminateGame()
        {
            this.Log.Write("Terminating game");
            this.Game.Terminate();
            this.Game = null;
            this.Delay = null;
            this.Phase = GameRunnerPhase.Lobby;
        }

        /// <summary>
        /// Forces quit from lobby and start of the game
        /// </summary>
        public void RemoteGameStart()
        {
            this.Log.Write("Remotely starting game");
            this.Phase = GameRunnerPhase.StartPending;
        }

        /// <summary>
        /// Run game runner logic
        /// </summary>
        public void Run()
        {
            switch (this.Phase)
            {
                case GameRunnerPhase.Lobby:
                    this.RunLobby();
                    break;
                case GameRunnerPhase.StartPending:
                    this.RunStartPending();
                    break;
                case GameRunnerPhase.Starting:
                    this.RunGame();
                    break;
                case GameRunnerPhase.Playing:
                    this.RunGameTurn();
                    break;
                case GameRunnerPhase.End:
                    this.RunGameEnd();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /// <summary>
        /// Gather players for the game
        /// </summary>
        protected void RunLobby()
        {
            if (this.Delay == null)
            {
                this.Delay = new DelayedAction(DateTime.Now.AddMilliseconds(100));
            }

            this.Delay.Run(() =>
                {
                    if (this.Game == null)
                    {
                        this.Log.Write("Creating new game in lobby");
                        this.Game = new Game(this.GamesCycle.Current);
                        this.Log.Write("Game mode: " + this.Game.Info.GameMode);
                        this.Log.Write("Map width: " + this.Game.Map.Width);
                        this.Log.Write("Map height: " + this.Game.Map.Height);
                        this.Log.Write("Number of players: " + this.Game.Info.NumberOfPlayers);
                        this.Log.Write("Number of teams: " + this.Game.Info.NumberOfTeams);
                        this.Log.Write("Map from file: " + this.Game.Info.UseMapFile);
                        this.Log.Write("Map file: " + this.Game.Info.MapFileName);
                        this.Log.Write("Number of free slots: " + this.Game.Pool.Slots.Free.Count);
                        this.Log.Write("Number of players in slots: " + this.Game.Pool.Slots.Free.Players.Count);
                        this.Log.Write("Number of teams in slots: " + this.Game.Pool.Slots.Free.Teams.Count);
                    }

                    // DEBUG: add bots, when full start game
                    if (this.Game.Pool.Slots.Free.Count == 0)
                    {
                        this.Phase = GameRunnerPhase.StartPending;
                    }
                    else
                    {
                        this.Log.Write("Adding bot");
                        PlayerInfo botInfo = this.BotFactory.CreatePlayerInfo();
                        this.Lobby.Add(botInfo);
                        this.Game.SlotPool.Slots.AssignToFirstFreeSlot(botInfo);
                        this.Log.Write("Number of free slots: " + this.Game.Pool.Slots.Free.Count);
                    }

                    this.Delay = null;
                });
        }

        /// <summary>
        /// run game start pending
        /// </summary>
        protected void RunStartPending()
        {
            if (this.Delay == null)
            {
                this.Log.Write("Starting in " + this.Parameters.DelayOnStart + " seconds");
                this.Delay = new DelayedAction(DateTime.Now.AddSeconds(this.Parameters.DelayOnStart)); 
            }

            this.Delay.Run(() =>
                {
                    this.Phase = GameRunnerPhase.Starting;
                    this.Delay = null;
                });
        }
        
        /// <summary>
        /// run game start
        /// </summary>
        protected void RunGame()
        {
            this.Log.Write("Creating initial state and starting");
            this.Game.CreateInitialState();
            this.FlushState();
            this.Phase = GameRunnerPhase.Playing;
        }

        /// <summary>
        /// run game turn
        /// </summary>
        /// <returns>returns a value indicating whether game is still running</returns>
        protected void RunGameTurn()
        {
            if (this.Delay == null)
            {
                this.Delay = new DelayedAction(DateTime.Now.AddMilliseconds(this.Parameters.TurnTime));
            }

            this.Delay.Run(() =>
                {

                    var events = this.GameEvents;
                    this.GameEvents = new EventsCollection();

                    this.Log.Write("Running bots logic");
                    foreach (GameSlot slot in this.Game.SlotPool.Slots.Assigned)
                    {
                        if (slot.PlayerType == PlayerType.Bot)
                        {
                            IBot bot = this.BotFactory.GetBot(slot.PlayerName);
                            events.AddRange(bot.GetPlayerEvents(slot.Player, this.Game.CurrentState, this.Game.Map));
                        }
                    }

                    this.Log.Write("Calculating next turn, player events gathered: " + events.Count);
                    this.Game.CreateNextState(events);
                    this.Log.Write("Turn " + this.Game.CurrentState.Turn + " calculated");
                    this.Game.CheckEndConditions();
                    this.FlushState();
                    if (this.Game.Result != GameResult.Running)
                    {
                        this.Log.Write("Game ends");
                        this.Phase = GameRunnerPhase.End;
                        switch (this.Game.Result)
                        {
                            case GameResult.FinshedWithWinners:
                                this.Log.Write("Winning team: [" + this.Game.WinningTeam.Identifier + "]");
                                break;
                            case GameResult.FinshedWithWinner:
                                this.Log.Write("Winning player: [" + this.Game.Winner.Identifier + "] " + this.Game.SlotPool.Slots.GetPlayersSlot(this.Game.Winner).PlayerName);
                                break;
                            case GameResult.FinishedWithoutWinner:
                                this.Log.Write("No winner");
                                break;
                            case GameResult.Terminated:
                                this.Log.Write("Terminated");
                                break;
                        }
                    }

                    this.Delay = null;
                });
        }

        /// <summary>
        /// End the game
        /// </summary>
        protected void RunGameEnd()
        {
            if (this.Delay == null)
            {
                this.Log.Write("Getting back to lobby in " + this.Parameters.DelayOnEnd);
                this.Delay = new DelayedAction(DateTime.Now.AddSeconds(this.Parameters.DelayOnEnd));
            }

            this.Delay.Run(() =>
                {
                    this.Game = null;
                    this.Delay = null;
                    this.GamesCycle.Cycle();
                    this.Phase = GameRunnerPhase.Lobby;
                });
        }


        /// <summary>
        /// Flushes game state
        /// </summary>
        private void FlushState()
        {
            StringBuilder builder = new StringBuilder(100*100);
            using (TextWriter writer = new StringWriter(builder))
            {
                new TextGameStateWriter(writer).Write(this.Game.Map, this.Game.CurrentState);
                writer.Close();
            }
            
            this.Log.Write(builder.ToString());
        }
    }*/
}
