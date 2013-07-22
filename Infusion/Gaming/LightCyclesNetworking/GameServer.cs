using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using Infusion.Gaming.LightCycles;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Events;
using Infusion.Gaming.LightCycles.Extensions;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Serialization;
using Infusion.Gaming.LightCyclesNetworking.Bots;
using Infusion.Gaming.LightCyclesNetworking.Error;
using Infusion.Gaming.LightCyclesNetworking.Request;
using Infusion.Gaming.LightCyclesNetworking.Response;
using Infusion.Networking;
using Infusion.Networking.ControllingServer;

namespace Infusion.Gaming.LightCyclesNetworking
{
    /// <summary>
    /// Player playing the game
    /// </summary>
    public class GameServer 
    {
        private readonly object syncRoot = new object();
        
        /// <summary>
        /// Creates new instance of a GameServer
        /// </summary>
        /// <param name="localEndPoint">local server endpoint</param>
        /// <param name="controllerServerService">controlling server endpoint</param>
        public GameServer(IPEndPoint localEndPoint, IControllerServerService controllerServerService)
        {
            if (localEndPoint == null)
            {
                throw new ArgumentNullException("localEndPoint");
            }

            if (controllerServerService == null)
            {
                throw new ArgumentNullException("controllerServerService");
            }
            
            this.Server = new Server(localEndPoint);
            this.ControllerServerService = controllerServerService;
            this.GameEvents = new List<Event>();
            this.Spectators = new List<string>();
        }

        /// <summary>
        /// Gets or sets networking client
        /// </summary>
        public IControllerServerService ControllerServerService { get; protected set; }

        /// <summary>
        /// Gets or sets networking server
        /// </summary>
        public Server Server { get; protected set; }

        /// <summary>
        /// Gets or sets game
        /// </summary>
        public IGame Game { get; protected set; }

        /// <summary>
        /// Gets or sets game key
        /// </summary>
        public string GameKey { get; protected set; }
        
        /// <summary>
        /// Gets or sets server players info
        /// </summary>
        public ServerPlayersInfo PlayersInfo { get; protected set; }

        /// <summary>
        /// Gets or sets game events
        /// </summary>
        public List<Event> GameEvents { get; protected set; }

        /// <summary>
        /// Gets or sets game events
        /// </summary>
        public List<string> Spectators { get; protected set; }

        /// <summary>
        /// Run one game
        /// </summary>
        public void RunGame()
        {
            // start client serving thread
            Thread clientServingThread = new Thread(ClientServingThread);
            clientServingThread.Start();


            // connect and register server
            this.ControllerServerService.Connect();
            if (this.ControllerServerService.RegisterServer())
            {
                // fing game to host
                this.GameKey = null;
                while (this.GameKey == null)
                {
                    this.GameKey = this.ControllerServerService.GetGameToHost();
                    Thread.Sleep(200);
                }

                // get game setup
                Dictionary<string, string> gameSetup = this.ControllerServerService.GetGameSetup(this.GameKey);
                
                // read game mode
                GameMode gameMode = GameMode.Undefined;
                if (gameSetup["GameMode"].Equals("FreeForAll")) gameMode = GameMode.FreeForAll;
                if (gameSetup["GameMode"].Equals("TeamDeathMatch")) gameMode = GameMode.TeamDeathMatch;

                // read map info 
                string mapName = gameSetup["MapName"];
                string mapFileName = gameSetup["MapFileName"];
                int mapWidth = int.Parse(gameSetup["MapWidth"]);
                int mapHeight = int.Parse(gameSetup["MapHeight"]);
                MapInfo mapInfo;
                if(string.IsNullOrEmpty(mapFileName))
                {
                    mapInfo = new MapInfo(mapName, mapWidth, mapHeight);
                }
                else
                {
                    mapInfo = new MapInfo(mapName, mapFileName);
                }
                
                // read players info
                int numberOfSlots = int.Parse(gameSetup["Slots"]);
                int numberOfPlayers = int.Parse(gameSetup["Players"]);
                int numberOfTeams = int.Parse(gameSetup["Teams"]);
                this.PlayersInfo = new ServerPlayersInfo(numberOfSlots, numberOfPlayers, numberOfTeams);
                for (int slot = 0; slot < numberOfSlots; slot++)
                {
                    this.PlayersInfo.PlayersIdentities.Add(new Identity(gameSetup["PlayerId" + slot]));
                    this.PlayersInfo.PlayersNames[slot] = gameSetup["PlayerName" + slot];
                    this.PlayersInfo.PlayersKeys[slot] = gameSetup["PlayerKey" + slot];
                    this.PlayersInfo.PlayersTypes[slot] = PlayerType.Undefined;
                    if (gameSetup["PlayerType" + slot] == "Human") this.PlayersInfo.PlayersTypes[slot] = PlayerType.Human;
                    if (gameSetup["PlayerType" + slot] == "Bot") this.PlayersInfo.PlayersTypes[slot] = PlayerType.Bot;
                    this.PlayersInfo.PlayersColors[slot] = ColorExtensions.Parse(gameSetup["PlayerColor" + slot]);
                    this.PlayersInfo.TeamsIdentities.Add(new Identity(gameSetup["TeamId" + slot]));
                    this.PlayersInfo.TeamsNames[slot] = gameSetup["TeamName" + slot];
                    this.PlayersInfo.TeamsColors[slot] = ColorExtensions.Parse(gameSetup["TeamColor" + slot]);
                }

                // wait for all players to connect
                for (int i = 0; i < this.PlayersInfo.NumberOfPlayers; i++)
                {
                    this.PlayersInfo.PlayersWaitHandles[i] = new ManualResetEvent(this.PlayersInfo.PlayersTypes[i] != PlayerType.Human);
                }

                for (int i = 0; i < this.PlayersInfo.NumberOfPlayers; i++)
                {
                    this.PlayersInfo.PlayersWaitHandles[i].WaitOne();
                }
                
                // create game
                lock (this.syncRoot)
                {
                    this.Game = new ServerGame(gameMode, mapInfo, this.PlayersInfo);
                    this.Game.CreateInitialState();
                }

                // run game until completed
                while (this.Game.Result == GameResult.Running)
                {
                    lock (this.syncRoot)
                    {
                        Thread.Sleep(100);
                        var gameEvents = new List<Event>();
                        
                        // add players events
                        lock (syncRoot)
                        {
                            gameEvents.AddRange(this.GameEvents);
                            this.GameEvents.Clear();
                        }

                        // add bots events
                        BotFactory factory = new BotFactory();
                        for (int i = 0; i < this.PlayersInfo.NumberOfPlayers; i++)
                        {
                            if(this.PlayersInfo.PlayersTypes[i] == PlayerType.Bot)
                            {
                                var bot = factory.GetBot(this.PlayersInfo.PlayersNames[i]);
                                gameEvents.AddRange(bot.GetPlayerEvents(this.PlayersInfo.PlayersIdentities[i], this.Game.CurrentState, this.Game.Map));
                            }
                        }

                        this.Game.CreateNextState(gameEvents);
                        this.Game.CheckEndConditions();
                    }
                }

                this.Game = null;

                // notify game complete
                this.ControllerServerService.GameHasEnded(this.GameKey);
                this.GameKey = null;

                // unregister server
                this.ControllerServerService.UnregisterServer();
            }

            this.ControllerServerService.Disconnect();
        }

        /// <summary>
        /// Client connections accepting thread
        /// </summary>
        public void ClientServingThread()
        {
            this.Server.Connect();
            while (this.Server.IsConnected)
            {
                try
                {
                    Client client = this.Server.AcceptIncomingConnection();
                    Thread clientThread = new Thread(ClientThread);
                    clientThread.Start(client);
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    break;
                }
            }

            Console.WriteLine("Shutting down incomming connection acceptance");
            this.Server.Disconnect();       
        }

        /// <summary>
        /// Client handling thread
        /// </summary>
        /// <param name="arg">client passed as an argument</param>
        public void ClientThread(object arg)
        {
            Client client = (Client) arg;
            client.Connect();
            // get messages from client and process
            while (client.IsConnected)
            {
                try
                {
                    MessageCollection messages = client.Receive();
                    this.HandleMessage(client, messages.First<JoinGame>());
                    this.HandleMessage(client, messages.First<PlayerAction>());
                    this.HandleMessage(client, messages.First<SpectateGame>());
                    this.HandleMessage(client, messages.First<GetGameDetails>());
                    this.HandleMessage(client, messages.First<GetGameStatus>());
                    this.HandleMessage(client, messages.First<GetGameData>());
                    this.HandleMessage(client, messages.First<GetGameEndResult>());
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    break;
                }
            }

            Console.WriteLine("Shutting down client connection");
            client.Disconnect();
        }
        
        /// <summary>
        /// Handle game joining, restricted to players only
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="message">game message</param>
        protected void HandleMessage(Client client, JoinGame message)
        {
            if (message == null) return;
            if (!ValidateGameKey(client, message.GameKey)) return;
            if (!ValidatePlayerKey(client, message.PlayerKey)) return;
            
            int index = this.PlayersInfo.PlayerIndexByKey(message.PlayerKey);
            this.PlayersInfo.PlayersConnections[index] = client;
            ((ManualResetEvent)this.PlayersInfo.PlayersWaitHandles[index]).Set();
            client.Send(new Joined() { GameKey = this.GameKey, PlayerKey = message.PlayerKey });
        }

        /// <summary>
        /// Handle player action, restricted to players only
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="message">game message</param>
        protected void HandleMessage(Client client, PlayerAction message)
        {
            if (message == null) return;
            if (!ValidateGameKey(client, message.GameKey)) return;
            if (!ValidatePlayerKey(client, message.PlayerKey)) return;

            lock (syncRoot)
            {
                int index = this.PlayersInfo.PlayerIndexByKey(message.PlayerKey);
                this.GameEvents.Add(new PlayerMoveEvent(this.PlayersInfo.PlayersIdentities[index], message.MoveDirection));
                client.Send(new Moved());
            }
        }

        /// <summary>
        /// Handle game spectate request
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="message">game message</param>
        protected void HandleMessage(Client client, SpectateGame message)
        {
            if (message == null) return;

            string spectatorKey = Guid.NewGuid().ToString();
            this.Spectators.Add(spectatorKey);
            client.Send(new Joined() { GameKey = this.GameKey, PlayerKey = spectatorKey });
        }

        /// <summary>
        /// Handle game details request, restricted to players and spectators
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="message">game message</param>
        protected void HandleMessage(Client client, GetGameDetails message)
        {
            if (message == null) return;
            if (!ValidateGameKey(client, message.GameKey)) return;
            if (!ValidatePlayerKey(client, message.PlayerKey) && this.Spectators.Contains(message.PlayerKey)) return;

            int index = this.PlayersInfo.PlayerIndexByKey(message.PlayerKey);
            client.Send(new GameDetails()
                {
                    MyPlayerId = this.PlayersInfo.PlayersIdentities[index].Identifier,
                    MyTeamId = this.PlayersInfo.TeamsIdentities[index].Identifier,
                    MapName = this.Game.MapInfo.MapName,
                    MapWidth = this.Game.MapInfo.MapWidth,
                    MapHeight = this.Game.MapInfo.MapHeight,
                    MapData = new TextMapWriter().Write(this.Game.Map),
                    GameMode = this.Game.GameMode,
                    GameSlots = this.PlayersInfo.NumberOfPlayers,
                    PlayerId = this.PlayersInfo.PlayersIdentities.AsCharArray,
                    PlayerName = this.PlayersInfo.PlayersNames,
                    PlayerColor = this.PlayersInfo.PlayersColors,
                    TeamId = this.PlayersInfo.TeamsIdentities.AsCharArray,
                    TeamName = this.PlayersInfo.TeamsNames,
                    TeamColor = this.PlayersInfo.TeamsColors,
                });
        }

        /// <summary>
        /// Handle game status request, restricted to players and spectators
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="message">game message</param>
        protected void HandleMessage(Client client, GetGameStatus message)
        {
            if (message == null) return;
            if (!ValidateGameKey(client, message.GameKey)) return;
            if (!ValidatePlayerKey(client, message.PlayerKey) && this.Spectators.Contains(message.PlayerKey)) return;

            GameState gameState;
            switch (this.Game.Result)
            {
                case GameResult.Running:
                    gameState = GameState.Running;
                    break;
                case GameResult.FinshedWithWinner:
                case GameResult.FinshedWithWinners:
                case GameResult.FinishedWithoutWinner:
                case GameResult.Terminated:
                    gameState = GameState.Finished;
                    break;
                default:
                    gameState = GameState.Starting;
                    break;
            }

            int index = this.PlayersInfo.PlayerIndexByKey(message.PlayerKey);
            client.Send(new GameStatus()
                {
                    AmIAlive = this.Game.CurrentState.Objects.Players.Contains(this.PlayersInfo.PlayersIdentities[index]),
                    State = gameState,
                    TurnNumber = this.Game.CurrentState.Turn
                });
        }

        /// <summary>
        /// Handle game data request, restricted to players and spectators
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="message">game message</param>
        protected void HandleMessage(Client client, GetGameData message)
        {
            if (message == null) return;
            if (!ValidateGameKey(client, message.GameKey)) return;
            if (!ValidatePlayerKey(client, message.PlayerKey) && this.Spectators.Contains(message.PlayerKey)) return;

            client.Send(new GameData()
                {
                    Data = new TextGameStateWriter().Write(this.Game.Map, this.Game.CurrentState)
                });
        }

        /// <summary>
        /// Handle game end message request, restricted to players and spectators
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="message">game message</param>
        protected void HandleMessage(Client client, GetGameEndResult message)
        {
            if (message == null) return;
            if (!ValidateGameKey(client, message.GameKey)) return;
            if (!ValidatePlayerKey(client, message.PlayerKey) && this.Spectators.Contains(message.PlayerKey)) return;

            client.Send(new GameEndResult()
                {
                    GameResult = this.Game.Result,
                    Winner = this.Game.Winner.Identifier,
                    WinnerName = this.PlayersInfo.PlayersNames[this.PlayersInfo.PlayerIndexByIdentity(this.Game.Winner.Identifier)],
                    WinningTeam = this.Game.WinningTeam.Identifier,
                    WinningTeamName = this.PlayersInfo.TeamsNames[this.PlayersInfo.TeamIndexByIdentity(this.Game.WinningTeam.Identifier)],
                });
        }
        
        /// <summary>
        /// Validate if game key is correct
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="gameKey">game key</param>
        /// <returns>whether key is valid</returns>
        protected bool ValidateGameKey(Client client, string gameKey)
        {
            if (this.GameKey.Equals(gameKey))
            {
                return true;
            }
            
            client.Send(new InvalidGameKey());
            return false;
        }

        /// <summary>
        /// Validate if player key is acceptable
        /// </summary>
        /// <param name="client">networkig client</param>
        /// <param name="playerKey">player key</param>
        /// <returns>whether key is valid</returns>
        protected bool ValidatePlayerKey(Client client, string playerKey)
        {
            for (int i = 0; i < this.PlayersInfo.NumberOfPlayers; i++)
            {
                if (this.PlayersInfo.PlayersKeys[i].Equals(playerKey))
                {
                    return true;
                }
            }

            client.Send(new InvalidPlayerKey());
            return false;
        }

        /*
        /// <summary>
        /// Process remote control message and create reponse
        /// </summary>
        /// <param name="remoteCommandIn">remote control message</param>
        /// <param name="client">endPoint sending request</param>
        protected override void ProcessRemoteMessage(RemoteControlMessage remoteCommandIn, Client<RemoteControlMessage> client)
        {
            Console.WriteLine(client.Tag + ": " + remoteCommandIn);
            if (remoteCommandIn.Identifier == RequestIdentifier.RemoteAddBot)
            {
                PlayerInfo botInfo = this.gameRunner.BotFactory.CreatePlayerInfo();
                this.gameRunner.Lobby.Add(botInfo);
                this.gameRunner.Game.Pool.AssignToFirstFreeSlot(botInfo);
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteKickPlayer)
            {
                PlayerInfo playerInfo = this.gameRunner.Lobby.GetPlayer(remoteCommandIn.Data);
                GameSlot slot = this.gameRunner.Game.Pool.GetSlotAssigned(playerInfo);
                this.gameRunner.Game.Pool.ClearSlot(slot);
                this.gameRunner.Lobby.Remove(playerInfo);
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteLobbyMoveUp)
            {
                this.gameRunner.Lobby.MoveUp(remoteCommandIn.Data);
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteLobbyMoveDown)
            {
                this.gameRunner.Lobby.MoveDown(remoteCommandIn.Data);
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteAdjustDelayOnStart)
            {
                this.gameRunner.Parameters.DelayOnStart = int.Parse(remoteCommandIn.Data);
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteAdjustDelayOnEnd)
            {
                this.gameRunner.Parameters.DelayOnEnd = int.Parse(remoteCommandIn.Data);
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteAdjustTurnTime)
            {
                this.gameRunner.Parameters.TurnTime = int.Parse(remoteCommandIn.Data);
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteCycleGameInfo)
            {
                this.gameRunner.GamesCycle.Cycle();
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteNewGameInfo)
            {
                string[] parameters = remoteCommandIn.Data.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (parameters.Length == 4)
                {
                    GameMapInfo gameInfo = new GameMapInfo(
                        int.Parse(parameters[0]),
                        int.Parse(parameters[1]),
                        (GameMode)int.Parse(parameters[2]),
                        parameters[3]);
                    this.gameRunner.GamesCycle.Add(gameInfo);
                }
                else if (parameters.Length == 5)
                {
                    GameMapInfo gameInfo = new GameMapInfo(
                        int.Parse(parameters[0]),
                        int.Parse(parameters[1]),
                        (GameMode)int.Parse(parameters[2]),
                        int.Parse(parameters[3]),
                        int.Parse(parameters[4]));
                    this.gameRunner.GamesCycle.Add(gameInfo);
                }
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteRemoveGameInfo)
            {
                this.gameRunner.GamesCycle.TryRemoveAt(int.Parse(remoteCommandIn.Data));
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteTerminateGame)
            {
                this.gameRunner.TerminateGame();
            }
            else if (remoteCommandIn.Identifier == RequestIdentifier.RemoteStartGame)
            {
                this.gameRunner.RemoteGameStart();
            }
        }

        /// <summary>
        /// Process client command message and create reponse
        /// </summary>
        /// <param name="commandIn">client command message</param>
        /// <param name="client">endPoint sending request</param>
        protected override void ProcessCommandMessage(RequestBase commandIn, Client<RequestBase> client)
        {
            Console.WriteLine(client.Tag + ": " + commandIn);
            
            PlayerInfo playerInfo = this.gameRunner.Lobby.GetPlayer(client.Tag);
            GameSlot slot = this.gameRunner.Game.Pool.GetSlotAssigned(playerInfo);

            if (slot != null)
            {
                if (commandIn.Identifier == RequestIdentifier.CommandTurnLeft)
                {
                    this.gameRunner.GameEvents.Add(new PlayerMoveEvent(slot.Player, RelativeDirection.Left));
                }
                else if (commandIn.Identifier == RequestIdentifier.CommandTurnRight)
                {
                    this.gameRunner.GameEvents.Add(new PlayerMoveEvent(slot.Player, RelativeDirection.Right));
                }
                else if (commandIn.Identifier == RequestIdentifier.CommandGoStraightAhead)
                {
                    this.gameRunner.GameEvents.Add(new PlayerMoveEvent(slot.Player, RelativeDirection.StraightAhead));
                }
            }
            else
            {
                if (commandIn.Identifier == RequestIdentifier.CommandIWantToPlay)
                {
                    if (!this.gameRunner.Lobby.HasPlayer(client.Tag))
                    {
                        this.gameRunner.Lobby.Add(new PlayerInfo(client.Tag, commandIn.Data, PlayerType.Human));
                    }

                    if (!this.gameRunner.Game.Pool.HasSlotAssigned(playerInfo))
                    {
                        this.gameRunner.Game.Pool.AssignToFirstFreeSlot(playerInfo);
                    }
                }
            }
        }

        /// <summary>
        /// Create message that will be broadcasted
        /// </summary>
        /// <param name="client">endPoint which will get the message</param>
        /// <returns>message to broadcast</returns>
        protected override GameStateMessage CreateBroadcastMessage(Client<GameStateMessage> client)
        {
            GameStateMessage messageOut = new GameStateMessage();
            if (gameRunner.Phase == GameRunnerPhase.Lobby)
            {
                messageOut.Identifier = RequestIdentifier.BroadcastGamePlayerGathering;
            }
            else if(gameRunner.Phase == GameRunnerPhase.StartPending)
            {
                messageOut.Identifier = RequestIdentifier.BroadcastGameStartPending;
                messageOut.StartCounter = this.gameRunner.Delay.SecondsToStart;
            }
            else if(gameRunner.Phase == GameRunnerPhase.Starting)
            {
                messageOut.Identifier = RequestIdentifier.BroadcastGameStart;
            }
            else if(gameRunner.Phase == GameRunnerPhase.Playing)
            {
                messageOut.Identifier = RequestIdentifier.BroadcastGameTurn;
            }
            else if(gameRunner.Phase == GameRunnerPhase.End)
            {
                messageOut.Identifier = RequestIdentifier.BroadcastGameEnd;
            }

            PlayerInfo playerInfo = this.gameRunner.Lobby.GetPlayer(client.Tag);
            GameSlot slot = this.gameRunner.Game.Pool.GetSlotAssigned(playerInfo);
            messageOut.YourPlayerId = slot != null ? slot.Player.Identifier : '?';
            messageOut.YourTeamId = slot != null ? slot.Team.Identifier : '?';

            GameMapInfo gameInfo = this.gameRunner.GamesCycle[0];
            messageOut.GameMode = gameInfo.GameMode.ToString();
            messageOut.MapWidth = gameInfo.MapWidth;
            messageOut.MapHeight = gameInfo.MapHeight;

            if (this.gameRunner.Game != null)
            {
                // init game data
                messageOut.Players = this.gameRunner.Game.Pool.PlayersNames.Keys.ToArray();
                messageOut.PlayersNames = this.gameRunner.Game.Pool.PlayersNames.Values.ToArray();
                messageOut.Teams = this.gameRunner.Game.Pool.TeamsNames.Keys.ToArray();
                messageOut.TeamNames = this.gameRunner.Game.Pool.TeamsNames.Values.ToArray();
                messageOut.PlayersTeams = this.gameRunner.Game.Pool.Slots.SlotsTeams.Identifiers.ToArray();

                // game state
                messageOut.PlayersAlive = this.gameRunner.Game.CurrentState.Objects.Teams.Identifiers.ToArray();
                messageOut.TeamsAlive = this.gameRunner.Game.CurrentState.Objects.Players.Identifiers.ToArray();
                messageOut.TurnNumber = this.gameRunner.Game.CurrentState.Turn;

                // end game state
                messageOut.GameResult = this.gameRunner.Game.Result.ToString();
                messageOut.Winner = this.gameRunner.Game.Winner != null ? this.gameRunner.Game.Winner.Identifier : '?';
                messageOut.WinningTeam = this.gameRunner.Game.WinningTeam != null ? this.gameRunner.Game.WinningTeam.Identifier : '?';

                // game state
                messageOut.PlayersAlive = this.gameRunner.Game.CurrentState.Objects.Teams.Identifiers.ToArray();
                messageOut.TeamsAlive = this.gameRunner.Game.CurrentState.Objects.Players.Identifiers.ToArray();
                messageOut.TurnNumber = this.gameRunner.Game.CurrentState.Turn;

                StringBuilder builder = new StringBuilder();
                TextWriter writer = new StringWriter(builder);
                TextGameStateWriter stateWriter = new TextGameStateWriter(writer);
                stateWriter.Write(this.gameRunner.Game.Map, this.gameRunner.Game.CurrentState);
                writer.Close();

                messageOut.GameState = builder.ToString();
            }

            return messageOut;
        }*/
    }
}
