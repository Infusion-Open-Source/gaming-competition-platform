namespace Infusion.Networking.ControllingServer
{
    /// <summary>
    /// Player playing the game
    /// </summary>
    public class ControllingServer 
    {
        /*
        private readonly object syncRoot = new object();
        
        /// <summary>
        /// Maps root path
        /// </summary>
        public const string MapsRootPath = @"..\..\..\Maps";

        /// <summary>
        /// Gets or sets game lobby
        /// </summary>
        public GameLobby GameLobby { get; protected set; }

        /// <summary>
        /// Game info cycle
        /// </summary>
        public GameInfoCycle GameInfoCycle { get; protected set; }

        /// <summary>
        /// Game slots pool
        /// </summary>
        public GameSlotsPool GameSlotsPool { get; protected set; }

        /// <summary>
        /// Game server pool
        /// </summary>
        public GameSetupPool GameSetupPool { get; protected set; }

        /// <summary>
        /// Game server pool
        /// </summary>
        public GameServerPool GameServerPool { get; protected set; }

        /// <summary>
        /// Creates new instance of a GameServer
        /// </summary>
        /// <param name="localEndPoint">local server endpoint</param>
        public ControllingServer(IPEndPoint localEndPoint)
        {
            if (localEndPoint == null)
            {
                throw new ArgumentNullException("localEndPoint");
            }

            this.Server = new Server(localEndPoint);
            this.GameLobby = new GameLobby();
            this.GameInfoCycle = new GameInfoCycle();

            // add games to cycle
            this.GameInfoCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, 50, 22));
            this.GameInfoCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\infusion_logo.png")));
            this.GameInfoCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\pac_man.png")));
            this.GameInfoCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\pac_man2.png")));
            this.GameInfoCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\spiral.png")));
            this.GameInfoCycle.Add(new GameInfo(8, 8, GameMode.FreeForAll, Path.Combine(MapsRootPath, @"FreeForAll\world.png")));

            this.GameInfoCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, 50, 22));
            this.GameInfoCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\infusion_logo.png")));
            this.GameInfoCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\pac_man.png")));
            this.GameInfoCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\pac_man2.png")));
            this.GameInfoCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\spiral.png")));
            this.GameInfoCycle.Add(new GameInfo(8, 2, GameMode.TeamDeathMatch, Path.Combine(MapsRootPath, @"TeamDeathmatch\world.png")));
        }
        
        /// <summary>
        /// Gets or sets networking server
        /// </summary>
        public Server Server { get; protected set; }

        /// <summary>
        /// Initialize processing
        /// </summary>
        public void Run()
        {
            while(this.Server.IsConnected)
            {
                Client client = this.Server.AcceptIncomingConnection();
                Thread clientThread = new Thread(ClientThread);
                clientThread.Start(client);
            }
        }

        /// <summary>
        /// Run client routine
        /// </summary>
        /// <param name="arg">client passed by argument</param>
        public void ClientThread(object arg)
        {
            Client client = (Client)arg;
            while (client.IsConnected)
            {
                MessageCollection messages = client.Receive();
                // process messages from all clients synchornously one by one
                lock (syncRoot)
                {
                    IMessage messageOut = null;
                    foreach (IMessage messageIn in messages)
                    {
                        JoinLobby joinLobby = messageIn as JoinLobby;
                        if (joinLobby != null)
                        {
                            messageOut = this.JoinLobby(joinLobby);
                        }


                        if (messageOut != null)
                        {
                            client.Send(messageOut);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Join lobby
        /// </summary>
        /// <param name="message">message to process</param>
        /// <returns>response message</returns>
        public IMessage JoinLobby(JoinLobby message)
        {
            string playerKey = Guid.NewGuid().ToString();
            PlayerInfo info = new PlayerInfo(playerKey, message.PlayerName, PlayerType.Human);
            this.GameLobby.Add(info);
            return new LobbyJoined { PlayerKey = playerKey };
        }

        /// <summary>
        /// Find game
        /// </summary>
        /// <param name="message">message to process</param>
        /// <returns>response message</returns>
        public IMessage FindGame(FindGame message)
        {
            if (!this.GameLobby.HasPlayer(message.PlayerKey))
            {
                return new InvalidPlayerKey();
            }
            
            return new GameNotFound();
        }


        public IMessage AddGame(AddGame message)
        {
            if (!string.IsNullOrEmpty(message.MapName))
            {
                this.GameInfoCycle.Add(new GameInfo(message.NumberOfPlayers, message.NumberOfTeams, message.GameMode, Path.Combine(MapsRootPath, message.MapName)));    
            }
            else
            {
                this.GameInfoCycle.Add(new GameInfo(message.NumberOfPlayers, message.NumberOfTeams, message.GameMode, message.MapWidth, message.MapHeight));    
            }

            return new GameAdded();
        }

        public IMessage RemoveGame(RemoveGame message)
        {
            if (this.GameInfoCycle.Count > 0)
            {
                this.GameInfoCycle.Remove(this.GameInfoCycle.Current);
            }

            return new GameRemoved();
        }

        public IMessage CycleGame(CycleGame message)
        {
            this.GameInfoCycle.Cycle();
            return new GameCycled();
        }

        public IMessage AddServer(AddServer message, Client client)
        {
            if (!this.GameServerPool.HasServer(client.RemoteEndPointName))
            {
                this.GameServerPool.Add(client.RemoteEndPointName);
            }

            return new ServerAdded();
        }

        public IMessage RemoveServer(RemoveServer message, Client client)
        {
            if (this.GameServerPool.HasServer(client.RemoteEndPointName))
            {
                this.GameServerPool.Remove(client.RemoteEndPointName);
            }

            return new ServerRemoved();
        }

        public IMessage SetupGame(SetupGame message)
        {
            this.GameSetupPool.Add(new GameSetup(this.GameInfoCycle.Current, this.GameSlotsPool));
            // game is added to pool by clicking start in remote
            // create game setup and put into games pool

        }

        public IMessage HostGame(HostGame message, Client client)
        {
            // when game server asks to host game it will take game from the pool if available

            if (this.GameServerPool.HasServer(client.RemoteEndPointName) && this.GameSetupPool.Count > 0)
            {
                GameSetup setup = this.GameSetupPool[0];
                return new NewGameToHost()
                    {
                        GameKey = setup.GameKey,
                        GameMode = setup.GameMode,
                        MapName = setup.MapName,
                        GameSlots = setup.GameSlots,
                        SlotPlayerId = setup.SlotPlayerId,
                        SlotPlayerName = setup.SlotPlayerName,
                        SlotPlayerKey = setup.SlotPlayerKey,
                        SlotTeamId = setup.SlotTeamId,
                        SlotTeamName = setup.SlotTeamName,
                        MapWidth = setup.MapWidth,
                        MapHeight = setup.MapHeight,
                        MapData = setup.MapData,
                    };
            }

            return new ServerNotRegistered();
        }*/
    }
}
