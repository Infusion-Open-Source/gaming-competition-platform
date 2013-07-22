using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Response
{
    /// <summary>
    /// Game status info reponse message
    /// </summary>
    public class GameStatus : ResponseBase<GameStatus>
    {
        /// <summary>
        /// Gets or sets current state of the game
        /// </summary>
        public GameState State { get; set; }
        
        /// <summary>
        /// Gets or sets game turn number if it is running
        /// </summary>
        public int TurnNumber { get; set; }

        /// <summary>
        /// Gets or sets whether player is alive
        /// </summary>
        public bool AmIAlive { get; set; }
    }
}
