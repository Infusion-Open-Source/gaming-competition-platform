namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using Infusion.Gaming.LightCycles.Config;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Serialization;

    /// <summary>
    /// Runs the game
    /// </summary>
    public class GameRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRunner" /> class.
        /// </summary>
        /// <param name="runSettings">run settings</param>
        /// <param name="settings">game rules</param>
        /// <param name="teamsAndPlayers">teams and players info</param>
        public GameRunner(RunSettings runSettings, GameSettings settings, TeamsAndPlayers teamsAndPlayers)
        {
            if (runSettings == null)
            {
                throw new ArgumentNullException("runSettings");
            }

            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (teamsAndPlayers == null)
            {
                throw new ArgumentNullException("teamsAndPlayers");
            }

            this.Settings = settings;
            this.RunSettings = runSettings;
            this.TeamsAndPlayers = teamsAndPlayers;
        }

        /// <summary>
        /// Gets or sets player controller
        /// </summary>
        public PlayerController PlayerController { get; protected set; }

        /// <summary>
        /// Gets or sets run settings
        /// </summary>
        public RunSettings RunSettings { get; protected set; }

        /// <summary>
        /// Gets or sets game settings
        /// </summary>
        public GameSettings Settings { get; protected set; }

        /// <summary>
        /// Gets or sets teams and players settings
        /// </summary>
        public TeamsAndPlayers TeamsAndPlayers { get; protected set; }

        /// <summary>
        /// Gets or sets players information
        /// </summary>
        public Dictionary<Identity, PlayerInfo> PlayerInfos { get; protected set; }

        /// <summary>
        /// Gets or sets teams information
        /// </summary>
        public Dictionary<Identity, TeamInfo> TeamsInfos { get; protected set; }

        /// <summary>
        /// Runs the game
        /// </summary>
        public void Run()
        {
            GameMode gameMode = this.SetupGameMode();
            PlayerSetup playersSetup = this.SetupPlayers(gameMode);
            GameInfo mapInfo = this.SetupMap();

            // create game
            Game game = new Game(gameMode, mapInfo, playersSetup);
            game.CreateInitialState();

            // make general game initial state output
            this.OutputInitialGameState(game);
            
            // create player processes
            this.PlayerController = new PlayerController(playersSetup.PlayersIdentities, this.TeamsAndPlayers.GetPlayersExePaths(this.PlayerInfos));

            // check if players are ready to play
            int playersReady;
            while (!this.CheckIfPlayersAreReady(playersSetup, out playersReady))
            {
                Console.Out.WriteLine("Players ready: " + playersReady);
            }

            Console.Out.WriteLine("Players ready: " + playersReady);

            // make player context based initial game state output
            foreach (Identity identity in playersSetup.PlayersIdentities)
            {
                this.OutputInitialGameState(game, identity);
            }

            // give it some time
            Thread.Sleep(5000);

            while (game.Result == GameResult.Undefined)
            {
                // make general game state output
                this.OutputGameState(game);
                
                // make player context based game state output (can take a while)
                Dictionary<Identity, string> stateDict = new Dictionary<Identity, string>();
                foreach (Identity identity in playersSetup.PlayersIdentities)
                {
                    stateDict.Add(identity, this.PrepareOutputGameState(game, identity));
                }

                // clear all data from players 
                this.PlayerController.Clear();

                // quickly send state
                foreach (Identity identity in playersSetup.PlayersIdentities)
                {
                    this.OutputGameState(game, identity, stateDict[identity]);
                }
                
                Thread.Sleep(this.RunSettings.TimeLimit); // give player time to produce an output, response after that time will be ignored

                // gather player events
                game.CreateNextState(this.GatherPlayerEvents(game, playersSetup.PlayersIdentities));
                game.CheckEndConditions();
            }

            // output game result
            this.OutputGameState(game);
            this.OutputGameFinalState(game);
            foreach (Identity identity in playersSetup.PlayersIdentities)
            {
                this.OutputGameFinalState(game, identity);
            }

            this.PlayerController.Close();
            this.PlayerController.Dispose();
        }

        /// <summary>
        /// Outputs to console game initial state
        /// </summary>
        /// <param name="game">game to output</param>
        private void OutputInitialGameState(Game game)
        {
            Console.Out.WriteLine("Game mode: " + this.Settings.GameMode);
            Console.Out.WriteLine("Players: " + this.Settings.PlayerSlotAssignment);
            Console.Out.WriteLine("Teams: " + this.Settings.TeamSlotAssignment);
            Console.Out.WriteLine("Map source: " + this.Settings.MapSource);
            Console.Out.WriteLine("Map name: " + this.Settings.MapName);
            Console.Out.WriteLine("Map file name: " + this.Settings.MapFileName);
            Console.Out.WriteLine("Map width: " + this.Settings.MapWidth);
            Console.Out.WriteLine("Map height: " + this.Settings.MapHeight);
            Console.Out.WriteLine("Trail aging: " + this.Settings.TrailAging);
            Console.Out.WriteLine("Obstacle ratio: " + this.Settings.ObstacleRatio);
            Console.Out.WriteLine("Clean move score: " + this.Settings.CleanMoveScore);
            Console.Out.WriteLine("Trail hit score: " + this.Settings.TrailHitScore);
            Console.Out.WriteLine("Last man stand score: " + this.Settings.LastManStandScore);

            Console.Out.WriteLine("Players data: " + this.PlayerInfos.Keys.Count);
            foreach (Identity id in this.PlayerInfos.Keys)
            {
                PlayerInfo info = this.PlayerInfos[id];
                Console.Out.WriteLine("Player " + id.Identifier + ":{0}:{1}", info.Name, info.TrailColor);
            }

            Console.Out.WriteLine("Teams data: " + this.TeamsInfos.Keys.Count);
            foreach (Identity id in this.TeamsInfos.Keys)
            {
                TeamInfo info = this.TeamsInfos[id];
                Console.Out.WriteLine("Team " + id.Identifier + ":{0}:{1}", info.Name, info.TrailColor);
            }

            Console.Out.Flush();
        }

        /// <summary>
        /// Check whether players are ready to play
        /// </summary>
        /// <param name="playersSetup">players setup</param>
        /// <param name="playersReady">number of players ready</param>
        /// <returns>whether all players are ready</returns>
        private bool CheckIfPlayersAreReady(PlayerSetup playersSetup, out int playersReady)
        {
            this.PlayerController.Clear();
            foreach (Identity identity in playersSetup.PlayersIdentities)
            {
                StreamWriter outStream = this.PlayerController.GetProcess(identity).StandardInput;
                outStream.WriteLine("Ready?");
                outStream.Flush();
            }

            Thread.Sleep(500);
            
            Dictionary<Identity, Queue<string>> data = this.PlayerController.ReadAllMessages(playersSetup.PlayersIdentities);
            playersReady = data.Keys.Count;
            return playersSetup.PlayersIdentities.Count == playersReady;
        }

        /// <summary>
        /// Outputs to player console game initial state
        /// </summary>
        /// <param name="game">current game </param>
        /// <param name="playerId">player identity</param>
        private void OutputInitialGameState(Game game, Identity playerId)
        {
            StreamWriter outStream = this.PlayerController.GetProcess(playerId).StandardInput;
            outStream.WriteLine("You Are: " + playerId);
            outStream.WriteLine("Players: " + this.Settings.PlayerSlotAssignment);
            outStream.WriteLine("Teams: " + this.Settings.TeamSlotAssignment);
            outStream.WriteLine("Game mode: " + this.Settings.GameMode);
            outStream.Flush();
        }

        /// <summary>
        /// Outputs to general console game state
        /// </summary>
        /// <param name="game">current game</param>
        private void OutputGameState(Game game)
        {
            Console.Out.WriteLine("Turn: " + game.CurrentState.Turn);
            TextGameStateWriter writer = new TextGameStateWriter();
            Console.Out.Write(writer.Write(game.Map, game.CurrentState));
            Console.Out.Flush();
        }

        /// <summary>
        /// Prepares output to player console game state
        /// </summary>
        /// <param name="game">current game</param>
        /// <param name="playerId">player identity</param>
        /// <returns>serialized game state</returns>
        private string PrepareOutputGameState(Game game, Identity playerId)
        {
            StringBuilder outStream = new StringBuilder();
            outStream.AppendLine("Turn: " + game.CurrentState.Turn);

            if (game.CurrentState.Objects.Players.Contains(playerId))
            {
                TextPlayerGameStateWriter writer = new TextPlayerGameStateWriter(playerId, this.RunSettings.ViewArea, this.RunSettings.FogOfWar);
                outStream.Append(writer.Write(game.Map, game.CurrentState));
            }
            else
            {
                outStream.AppendLine("1");
                outStream.AppendLine("You are dead!");
            }

            return outStream.ToString();
        }

        /// <summary>
        /// Outputs to player console game state
        /// </summary>
        /// <param name="game">current game</param>
        /// <param name="playerId">player identity</param>
        /// <param name="state">game current state already serialized</param>
        private void OutputGameState(Game game, Identity playerId, string state)
        {
            StreamWriter outStream = this.PlayerController.GetProcess(playerId).StandardInput;
            outStream.WriteLine(state);
            outStream.Flush();
        }

        /// <summary>
        /// Outputs to general console game final state
        /// </summary>
        /// <param name="game">current game</param>
        private void OutputGameFinalState(Game game)
        {
            Console.Out.WriteLine("Game ends");
            switch (game.Result)
            {
                case GameResult.FinshedWithWinner:
                    Console.Out.WriteLine("Winner: " + game.Winner);
                    break;
                case GameResult.FinshedWithWinners:
                    Console.Out.WriteLine("WinningTeam: " + game.WinningTeam);
                    break;
                case GameResult.FinishedWithoutWinner:
                    Console.Out.WriteLine("Game is draw");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("game");
            }

            Console.Out.WriteLine("Scores data: " + this.PlayerInfos.Keys.Count);
            foreach (Identity id in this.PlayerInfos.Keys)
            {
                Console.Out.WriteLine("Score " + id.Identifier + ":" + game.PlayerSetup.Scoreboard[id]);
            }
        }

        /// <summary>
        /// Outputs to players console game final state
        /// </summary>
        /// <param name="game">game to output</param>
        /// <param name="playerId">player identity</param>
        private void OutputGameFinalState(Game game, Identity playerId)
        {
            StreamWriter outStream = this.PlayerController.GetProcess(playerId).StandardInput;
            outStream.WriteLine("Game ends");
            switch (game.Result)
            {
                case GameResult.FinshedWithWinner:
                    if (game.Winner.Equals(playerId))
                    {
                        outStream.WriteLine("You win!");
                    }
                    else
                    {
                        outStream.WriteLine("You lose!");
                    }

                    break;
                case GameResult.FinshedWithWinners:
                    outStream.WriteLine("WinningTeam: " + game.WinningTeam);
                    if (game.WinningTeam.Equals(game.PlayerSetup.TeamsIdentities[game.PlayerSetup.PlayersIdentities.IndexOf(playerId)]))
                    {
                        outStream.WriteLine("Your team wins!");
                    }
                    else
                    {
                        outStream.WriteLine("Your team loses!");
                    }

                    break;
                case GameResult.FinishedWithoutWinner:
                    outStream.WriteLine("Game is draw");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("game");
            }

            outStream.WriteLine("Score: " + game.PlayerSetup.Scoreboard[playerId]);
        }

        /// <summary>
        /// Gathers players events
        /// </summary>
        /// <param name="game">current game</param>
        /// <param name="identities">players identities</param>
        /// <returns>list of players events</returns>
        private List<Event> GatherPlayerEvents(Game game, IEnumerable<Identity> identities)
        {
            int lines = 0;
            StringBuilder playerCommansLog = new StringBuilder();

            EventsCollection events = new EventsCollection();
            Dictionary<Identity, Queue<string>> data = this.PlayerController.ReadAllMessages(identities);
            foreach (Identity playerId in data.Keys)
            {
                // skip dead players
                if (game.CurrentState.Objects.Players.Contains(playerId)) 
                {
                    while (data[playerId].Count > 0)
                    {
                        string line = data[playerId].Dequeue();

                        // gather commands like: R - player goes right, L - goes left, S goeas straight ahead
                        if (line.Length == 1) 
                        {
                            RelativeDirection direction = RelativeDirection.StraightAhead;
                            if (line[0] == 'R' || line[0] == 'r')
                            {
                                direction = RelativeDirection.Right;
                            }

                            if (line[0] == 'L' || line[0] == 'l')
                            {
                                direction = RelativeDirection.Left;
                            }

                            events.Add(new PlayerMoveEvent(playerId, direction));
                            playerCommansLog.AppendLine(playerId + ":" + direction);
                            lines++;
                        }
                    }
                }
            }
            
            Console.Out.Write("Commands: " + lines + Environment.NewLine + playerCommansLog);
            return events;
        }

        /// <summary>
        /// Sets up map
        /// </summary>
        /// <returns>game info (map) read from settings</returns>
        private GameInfo SetupMap()
        {
            if (this.Settings.MapSource == "File")
            {
                return new GameInfo(Path.GetFileName(this.Settings.MapName), this.Settings.MapFileName, this.Settings.TrailAging, this.Settings.CleanMoveScore, this.Settings.TrailHitScore, this.Settings.LastManStandScore);
            }

            if (this.Settings.MapSource == "Generate")
            {
                return new GameInfo(this.Settings.MapName, this.Settings.MapWidth, this.Settings.MapHeight, this.Settings.TrailAging, this.Settings.ObstacleRatio, this.Settings.CleanMoveScore, this.Settings.TrailHitScore, this.Settings.LastManStandScore);
            }

            throw new ArgumentOutOfRangeException("this.Settings.MapSource", "Map source not recognized");
        }

        /// <summary>
        /// Sets up game mode
        /// </summary>
        /// <returns>Game mode</returns>
        private GameMode SetupGameMode()
        {
            if (this.Settings.GameMode == "FreeForAll")
            {
                return GameMode.FreeForAll;
            }

            if (this.Settings.GameMode == "TeamDeathMatch")
            {
                return GameMode.TeamDeathMatch;
            }

            throw new ArgumentOutOfRangeException("this.Settings.GameMode", "Game mode not recognized");
        }

        /// <summary>
        /// Sets up players
        /// </summary>
        /// <param name="gameMode">current game mode</param>
        /// <returns>player setup</returns>
        private PlayerSetup SetupPlayers(GameMode gameMode)
        {
            if (gameMode == GameMode.FreeForAll)
            {
                this.Settings.TeamSlotAssignment = this.Settings.PlayerSlotAssignment;
                PlayerSetup playersSetup = new PlayerSetup(this.Settings.PlayerSlotAssignment, this.Settings.TeamSlotAssignment);
                this.PlayerInfos = this.TeamsAndPlayers.GetPlayersInformation(playersSetup, this.RunSettings.PlayerMappings);
                this.TeamsInfos = new Dictionary<Identity, TeamInfo>();
                return playersSetup;
            }

            if (gameMode == GameMode.TeamDeathMatch)
            {
                PlayerSetup playersSetup = new PlayerSetup(this.Settings.PlayerSlotAssignment, this.Settings.TeamSlotAssignment);
                this.PlayerInfos = this.TeamsAndPlayers.GetPlayersInformation(playersSetup, this.RunSettings.PlayerMappings);
                this.TeamsInfos = this.TeamsAndPlayers.GetTeamsInformation(playersSetup, this.RunSettings.TeamMappings);
                return playersSetup;
            }

            throw new ArgumentOutOfRangeException("gameMode");
        }
    }
}
