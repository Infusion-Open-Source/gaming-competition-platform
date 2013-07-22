using System.Collections.Generic;
using Infusion.Networking.ControllingServer.Response;

namespace Infusion.Networking.ControllingServer
{
    public class ControllerServerServiceStub : IControllerServerService
    {
        public const string SecretGameKey = "SECRET_GAME_KEY";

        /// <summary>
        /// Conneects to controller service
        /// </summary>
        public void Connect()
        {
        }

        /// <summary>
        /// Dicsonnects from controller service
        /// </summary>
        public void Disconnect()
        {
        }

        /// <summary>
        /// Register current connection as a server able to host a game
        /// </summary>
        /// <returns>whether server has been registered</returns>
        public bool RegisterServer()
        {
            return true;
        }

        /// <summary>
        /// Unregister server from server pool on controlling service
        /// </summary>
        public void UnregisterServer()
        {
        }

        /// <summary>
        /// Fetches game key to be hosted
        /// </summary>
        /// <returns>key of game to host</returns>
        public string GetGameToHost()
        {
            return SecretGameKey;
        }

        /// <summary>
        /// Fetches game setup data
        /// </summary>
        /// <returns>game setup data</returns>
        public Dictionary<string, string> GetGameSetup(string gameKey)
        {
            if (gameKey != null && gameKey.Equals(SecretGameKey))
            {
                Dictionary<string, string> gameSetup = new Dictionary<string, string>();
                gameSetup.Add("GameMode", "FreeForAll");
                gameSetup.Add("MapName", "Some Generated Map");
                gameSetup.Add("MapFileName", "");
                gameSetup.Add("MapWidth", "50");
                gameSetup.Add("MapHeight", "75");

                gameSetup.Add("Slots", "8");
                gameSetup.Add("Players", "2");
                gameSetup.Add("Teams", "2");

                gameSetup.Add("PlayerId0", "A");
                gameSetup.Add("PlayerName0", "Player A");
                gameSetup.Add("PlayerKey0", "Player A");
                gameSetup.Add("PlayerType0", "Human");
                gameSetup.Add("PlayerColor0", "255,0,0");
                gameSetup.Add("TeamId0", "A");
                gameSetup.Add("TeamName0", "Team A");
                gameSetup.Add("TeamColor0", "255,0,0");

                gameSetup.Add("PlayerId1", "B");
                gameSetup.Add("PlayerName1", "Player B");
                gameSetup.Add("PlayerKey1", "Player B");
                gameSetup.Add("PlayerType1", "Human");
                gameSetup.Add("PlayerColor1", "0,255,0");
                gameSetup.Add("TeamId1", "B");
                gameSetup.Add("TeamName1", "Team B");
                gameSetup.Add("TeamColor1", "255,0,0");

                for (int i = 2; i < 8; i++)
                {
                    string id = ((char)('A' + i)).ToString();
                    gameSetup.Add("PlayerId" + i, id);
                    gameSetup.Add("PlayerName" + i, "Player " + id);
                    gameSetup.Add("PlayerKey" + i, "");
                    gameSetup.Add("PlayerType" + i, "");
                    gameSetup.Add("PlayerColor" + i, "0,0,0");
                    gameSetup.Add("TeamId" + i, id);
                    gameSetup.Add("TeamName" + i, "Team " + id);
                    gameSetup.Add("TeamColor" + i, "0,0,0");
                }

                return gameSetup;
            }

            return null;
        }

        /// <summary>
        /// Notifies controller service that game has ended
        /// </summary>
        /// <returns>game key</returns>
        public void GameHasEnded(string gameKey)
        {
        }

        /// <summary>
        /// Submits stat to record on controller service
        /// </summary>
        /// <param name="name">stat name</param>
        /// <param name="value">stat value</param>
        public void SubmitStat(string name, int value)
        {
        }

        /// <summary>
        /// Submits stat to record on controller service
        /// </summary>
        /// <param name="name">stat name</param>
        /// <param name="valueMod">stat value modifier</param>
        public void ModifyStat(string name, int valueMod)
        {
        }

        /// <summary>
        /// Player joins lobby
        /// </summary>
        /// <param name="playerName">player name</param>
        /// <returns>unique player key</returns>
        public string JoinLobby(string playerName)
        {
            return playerName;
        }

        /// <summary>
        /// Player leaves lobby
        /// </summary>
        /// <param name="playerKey">player key</param>
        public void LeaveLobby(string playerKey)
        {
            
        }

        /// <summary>
        /// Find game to play
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <returns>game serve where game can be played</returns>
        public NewGame FindGame(string playerKey)
        {
            return new NewGame()
                {
                    GameKey = SecretGameKey,
                    ServerAddress = "localhost",
                    ServerPort = 10001
                };
        }
    }
}
