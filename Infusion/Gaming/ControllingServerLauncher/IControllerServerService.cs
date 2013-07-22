using System.Collections.Generic;
using Infusion.Networking.ControllingServer.Response;

namespace Infusion.Networking.ControllingServer
{
    public interface IControllerServerService
    {
        /// <summary>
        /// Conneects to controller service
        /// </summary>
        void Connect();

        /// <summary>
        /// Dicsonnects from controller service
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Register current connection as a server able to host a game
        /// </summary>
        /// <returns>whether server has been registered</returns>
        bool RegisterServer();

        /// <summary>
        /// Unregister server from server pool on controlling service
        /// </summary>
        void UnregisterServer();

        /// <summary>
        /// Fetches game key to be hosted
        /// </summary>
        /// <returns>key of game to host</returns>
        string GetGameToHost();

        /// <summary>
        /// Fetches game setup data
        /// </summary>
        /// <returns>game setup data</returns>
        Dictionary<string, string> GetGameSetup(string gameKey);

        /// <summary>
        /// Notifies controller service that game has ended
        /// </summary>
        /// <returns>game key</returns>
        void GameHasEnded(string gameKey);

        /// <summary>
        /// Submits stat to record on controller service
        /// </summary>
        /// <param name="name">stat name</param>
        /// <param name="value">stat value</param>
        void SubmitStat(string name, int value);

        /// <summary>
        /// Submits stat to record on controller service
        /// </summary>
        /// <param name="name">stat name</param>
        /// <param name="valueMod">stat value modifier</param>
        void ModifyStat(string name, int valueMod);

        /// <summary>
        /// Player joins lobby
        /// </summary>
        /// <param name="playerName">player name</param>
        /// <returns>unique player key</returns>
        string JoinLobby(string playerName);

        /// <summary>
        /// Player leaves lobby
        /// </summary>
        /// <param name="playerKey">player key</param>
        void LeaveLobby(string playerKey);

        /// <summary>
        /// Find game to play
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <returns>game serve where game can be played</returns>
        NewGame FindGame(string playerKey);
    }
}
