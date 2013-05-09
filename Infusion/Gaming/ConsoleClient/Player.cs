namespace ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCyclesCommon.Networking;

    /// <summary>
    /// Player playing the game
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        /// <param name="listener">broadcasts listener to use</param>
        public Player(BroadcastListener listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException("listener");
            }

            this.Listener = listener;
        }

        /// <summary>
        /// Gets or sets broadcast listener to use
        /// </summary>
        public BroadcastListener Listener { get; protected set; }
        
        /// <summary>
        /// Play the game
        /// </summary>
        public void Play()
        {
            List<string> gameState = new List<string>();
            var messageParser = new MessageParser();
            string gameMode;
            int mapWidth;
            int mapHeight;
            int numberOfPlayers;
            int numberOfTeams;
            int turn;
            int ocupiedSlots;
            int totalSlots;
            string addedPlayerId;
            string addedPlayerTeamId;
            int startsInCounter;
            string winningTeam;
            string winningPlayer;

            while (true)
            {
                string message = this.Listener.Receive();
                if (string.IsNullOrEmpty(message))
                {
                    continue;
                }

                if (messageParser.TryGetString(message, "[Initialize][Mode]", out gameMode))
                {
                    Console.WriteLine("GameMode:" + gameMode);
                }
                else if (messageParser.TryGetInt(message, "[Initialize][MapWidth]", out mapWidth))
                {
                    Console.WriteLine("MapWidth:" + mapWidth);
                }
                else if (messageParser.TryGetInt(message, "[Initialize][MapHeight]", out mapHeight))
                {
                    Console.WriteLine("MapHeight:" + mapHeight);
                }
                else if (messageParser.TryGetInt(message, "[Initialize][NumberOfPlayers]", out numberOfPlayers))
                {
                    Console.WriteLine("NumberOfPlayers:" + numberOfPlayers);
                }
                else if (messageParser.TryGetInt(message, "[Initialize][NumberOfTeams]", out numberOfTeams))
                {
                    Console.WriteLine("NumberOfTeams:" + numberOfTeams);
                }
                else if (messageParser.TryGetIntInt(message, "[Waiting for players][Slots]", "/", out ocupiedSlots, out totalSlots))
                {
                    // game is waiting for players, if ocupied players less than total slots then send request that I want to play
                    Console.WriteLine("Slots available: " + ocupiedSlots + "/" + totalSlots);
                }
                else if (messageParser.TryGetStringString(message, "[Waiting for players][AddPlayer][Bot]", ",", out addedPlayerId, out addedPlayerTeamId))
                {
                    Console.WriteLine("Player added: " + addedPlayerId + " in team: " + addedPlayerTeamId);
                }
                else if (messageParser.TryGetInt(message, "[GameStart][In]", out startsInCounter))
                {
                    Console.WriteLine("Game starts in: " + startsInCounter);
                }
                else if (messageParser.TryGet(message, "[GameStart][Now]"))
                {
                    // game starts from this moment 
                    Console.WriteLine("Game starts");
                }
                else if (messageParser.TryGetInt(message, "[TurnStart]", out turn))
                {
                    // turn started, game state is on next messages until [TurnEnd] received
                    Console.WriteLine("Turn " + turn);
                    gameState = new List<string>();
                }
                else if (messageParser.TryGet(message, "[TurnEnd]"))
                {
                    // end of game turn, state data ready
                    int totalBytes = 0;
                    foreach (string s in gameState)
                    {
                        totalBytes += s.Length;
                    }

                    Console.WriteLine("State bytes received: " + totalBytes + ". DO THE STATE PROCESING HERE AND SEND PLAYER COMMAND");
                }
                else if (messageParser.TryGetString(message, "[GameEnd][Team wins]", out winningTeam))
                {
                    Console.WriteLine("Team " + winningTeam + " has won");
                    break;
                }
                else if (messageParser.TryGetString(message, "[GameEnd][Player wins]", out winningPlayer))
                {
                    Console.WriteLine("Player " + winningPlayer + " has won");
                    break;
                }
                else if (messageParser.TryGet(message, "[GameEnd][No winner]"))
                {
                    Console.WriteLine("No winner on this game");
                    break;
                }
                else if (messageParser.TryGet(message, "[GameEnd][Terminated]"))
                {
                    Console.WriteLine("Game terminated");
                    break;
                }
                else
                {
                    gameState.Add(message);
                }
            }
        }
    }
}
