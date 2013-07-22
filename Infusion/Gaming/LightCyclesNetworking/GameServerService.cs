using System;
using System.Collections.Generic;
using System.Net;
using Infusion.Gaming.LightCyclesNetworking.Error;
using Infusion.Gaming.LightCyclesNetworking.Request;
using Infusion.Gaming.LightCyclesNetworking.Response;
using Infusion.Networking;
using Infusion.Networking.ControllingServer.Exceptions;

namespace Infusion.Gaming.LightCyclesNetworking
{
    /// <summary>
    /// Base game client class
    /// Consumes following services:
    /// - one way game state broadcasting server
    /// - two way sockets for communication with multiple clients
    /// </summary>
    public class GameServerService : IGameServerService
    {
        /// <summary>
        /// Creates new instance of a GameServerClient
        /// </summary>
        /// <param name="gameServerEndPoint">game server endpoint</param>
        public GameServerService(IPEndPoint gameServerEndPoint)
        {
            if (gameServerEndPoint == null)
            {
                throw new ArgumentNullException("gameServerEndPoint");
            }

            this.Client = new Client(gameServerEndPoint);
        }

        /// <summary>
        /// Gets or sets network networking client
        /// </summary>
        public Client Client { get; protected set; }

        /// <summary>
        /// Connects to controller service
        /// </summary>
        public void Connect()
        {
            this.Client.Connect();
        }

        /// <summary>
        /// Dicsonnects from controller service
        /// </summary>
        public void Disconnect()
        {
            this.Client.Disconnect();
        }
        
        /// <summary>
        /// Join new game
        /// </summary>
        /// <param name="playerKey">joining player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>whether player joined the game</returns>
        public bool JoinGame(string playerKey, string gameKey)
        {
            MessageCollection messages = this.Client.SendAndReceive(new JoinGame() { PlayerKey = playerKey, GameKey = gameKey });
            this.HandleErrorMessages(messages);
            return messages.Contains<Joined>();
        }

        /// <summary>
        /// Join game as a spectator
        /// </summary>
        /// <returns>spectatory key</returns>
        public string SpectateGame()
        {
            MessageCollection messages = this.Client.SendAndReceive(new SpectateGame());
            this.HandleErrorMessages(messages);
            if (messages.Contains<Joined>())
            {
                return messages.First<Joined>().PlayerKey;
            }

            return null;
        }

        /// <summary>
        /// Get game details
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>game details</returns>
        public GameDetails GetGameDetails(string playerKey, string gameKey)
        {
            MessageCollection messages = this.Client.SendAndReceive(new GetGameDetails() { PlayerKey = playerKey, GameKey = gameKey });
            this.HandleErrorMessages(messages);
            return messages.First<GameDetails>();
        }

        /// <summary>
        /// Get game status
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>game status</returns>
        public GameStatus GetGameStatus(string playerKey, string gameKey)
        {
            MessageCollection messages = this.Client.SendAndReceive(new GetGameStatus() { PlayerKey = playerKey, GameKey = gameKey });
            this.HandleErrorMessages(messages);
            return messages.First<GameStatus>();
        }

        /// <summary>
        /// Get game data
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>game data</returns>
        public GameData GetGameData(string playerKey, string gameKey)
        {
            MessageCollection messages = this.Client.SendAndReceive(new GetGameData() { PlayerKey = playerKey, GameKey = gameKey });
            this.HandleErrorMessages(messages); 
            return messages.First<GameData>();
        }

        /// <summary>
        /// Sends player action
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <param name="moveDirection">player move direction</param>
        /// <returns>whether action has been delivered</returns>
        public bool SendPlayerAction(string playerKey, string gameKey, Infusion.Gaming.LightCycles.Definitions.RelativeDirection moveDirection)
        {
            MessageCollection messages = this.Client.SendAndReceive(new PlayerAction() { PlayerKey = playerKey, GameKey = gameKey, MoveDirection = moveDirection });
            this.HandleErrorMessages(messages);
            return messages.Contains<Moved>();
        }

        /// <summary>
        /// Get game end result
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>game end result</returns>
        public GameEndResult GetGameEndResult(string playerKey, string gameKey)
        {
            MessageCollection messages = this.Client.SendAndReceive(new GetGameEndResult() { PlayerKey = playerKey, GameKey = gameKey });
            this.HandleErrorMessages(messages);
            return messages.First<GameEndResult>();
        }

        /// <summary>
        /// Handle common error messages
        /// </summary>
        /// <param name="messages">messages to check</param>
        protected void HandleErrorMessages(IEnumerable<IMessage> messages)
        {
            foreach (IMessage message in messages)
            {
                HandleErrorMessages(message);
            }
        }

        /// <summary>
        /// Handle common error messages
        /// </summary>
        /// <param name="message">message to check</param>
        protected void HandleErrorMessages(IMessage message)
        {
            InvalidGameKey invalidGameKey = message as InvalidGameKey;
            if (invalidGameKey != null)
            {
                throw new InvalidGameKeyException(invalidGameKey.Message);
            }

            InvalidPlayerKey invalidPlayerKey = message as InvalidPlayerKey;
            if (invalidPlayerKey != null)
            {
                throw new InvalidGameKeyException(invalidPlayerKey.Message);
            }
        }
    }
}
