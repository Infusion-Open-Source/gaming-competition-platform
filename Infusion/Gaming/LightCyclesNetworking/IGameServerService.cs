using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infusion.Gaming.LightCyclesNetworking.Error;
using Infusion.Gaming.LightCyclesNetworking.Request;
using Infusion.Gaming.LightCyclesNetworking.Response;
using Infusion.Networking;
using Infusion.Networking.ControllingServer.Exceptions;

namespace Infusion.Gaming.LightCyclesNetworking
{
    public interface IGameServerService
    {
        /// <summary>
        /// Connects to controller service
        /// </summary>
        void Connect();

        /// <summary>
        /// Dicsonnects from controller service
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Join new game
        /// </summary>
        /// <param name="playerKey">joining player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>whether player joined the game</returns>
        bool JoinGame(string playerKey, string gameKey);

        /// <summary>
        /// Join game as a spectator
        /// </summary>
        /// <returns>spectatory key</returns>
        string SpectateGame();

        /// <summary>
        /// Get game details
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>game details</returns>
        GameDetails GetGameDetails(string playerKey, string gameKey);

        /// <summary>
        /// Get game status
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>game status</returns>
        GameStatus GetGameStatus(string playerKey, string gameKey);

        /// <summary>
        /// Get game data
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>game data</returns>
        GameData GetGameData(string playerKey, string gameKey);

        /// <summary>
        /// Sends player action
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <param name="moveDirection">player move direction</param>
        /// <returns>whether action has been delivered</returns>
        bool SendPlayerAction(string playerKey, string gameKey, Infusion.Gaming.LightCycles.Definitions.RelativeDirection moveDirection);

        /// <summary>
        /// Get game end result
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <param name="gameKey">game key</param>
        /// <returns>game end result</returns>
        GameEndResult GetGameEndResult(string playerKey, string gameKey);
    }
}
