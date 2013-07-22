using System;
using System.Collections.Generic;
using System.Net;
using Infusion.Networking.ControllingServer.Error;
using Infusion.Networking.ControllingServer.Exceptions;
using Infusion.Networking.ControllingServer.Request;
using Infusion.Networking.ControllingServer.Response;

namespace Infusion.Networking.ControllingServer
{
    /// <summary>
    /// ControllingServerClient client class
    /// It is players client of controlling server, allows to connect to server lobby and get scheduled for games
    /// </summary>
    public class ControllerServerService
    {
        /// <summary>
        /// Creates new instance of a GameServerClient
        /// </summary>
        /// <param name="controllingServerEndPoint">game server endpoint</param>
        public ControllerServerService(IPEndPoint controllingServerEndPoint)
        {
            if (controllingServerEndPoint == null)
            {
                throw new ArgumentNullException("controllingServerEndPoint");
            }

            this.Client = new Client(controllingServerEndPoint);
        }

        /// <summary>
        /// Gets or sets network networking client
        /// </summary>
        public Client Client { get; protected set; }
        
        /// <summary>
        /// Run game server client
        /// </summary>
        public void Run()
        {
            this.Client.Connect();
            
            this.Client.Disconnect();
        }
        
        /// <summary>
        /// Join lobby
        /// </summary>
        /// <param name="playerName">player name</param>
        /// <returns>player key</returns>
        public string JoinLobby(string playerName)
        {
            MessageCollection messages = this.Client.SendAndReceive(new JoinLobby { PlayerName = playerName });
            foreach (IMessage msg in messages)
            {
                this.HandleErrorMessages(msg);

                LobbyJoined notification = msg as LobbyJoined;
                if (notification != null)
                {
                    return notification.PlayerKey;
                }
            }

            return null;
        }

        /// <summary>
        /// Leave lobby
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <returns>Whether has left the lobby</returns>
        public bool LeaveLobby(string playerKey)
        {
            MessageCollection messages = this.Client.SendAndReceive(new LeaveLobby() { PlayerKey = playerKey });
            foreach (IMessage msg in messages)
            {
                this.HandleErrorMessages(msg);

                LobbyLeft notification = msg as LobbyLeft;
                if (notification != null)
                {
                    return true;
                }
            }

            return false;
        }
        /*
        /// <summary>
        /// Request searching for a game
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameMode">game mode which would like to play</param>
        /// <returns>new game connection details</returns>
        public NewGame FindGame(string playerKey, GameMode gameMode)
        {
            MessageCollection messages = this.Client.SendAndReceive(new FindGame { PlayerKey = playerKey, GameMode = gameMode });
            foreach (IMessage msg in messages)
            {
                this.HandleErrorMessages(msg);

                GameNotFound gameNotFound = msg as GameNotFound;
                if (gameNotFound != null)
                {
                    return null;
                }
                
                NewGame newGame = msg as NewGame;
                if (newGame != null)
                {
                    return newGame;
                }
            }

            return null;
        }*/

        /// <summary>
        /// Handle common error messages
        /// </summary>
        /// <param name="message">message to check</param>
        protected void HandleErrorMessages(IMessage message)
        {
            Kicked kicked = message as Kicked;
            if (kicked != null)
            {
                throw new PlayerKickedException(kicked.Message);
            }

            InssuficientPriviledges inssuficientPriviledges = message as InssuficientPriviledges;
            if (inssuficientPriviledges != null)
            {
                throw new InssuficientPriviledgesException(inssuficientPriviledges.Message);
            }
        }
    }
}
