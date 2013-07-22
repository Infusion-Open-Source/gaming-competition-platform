using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Response
{
    /// <summary>
    /// Message with game end data
    /// </summary>
    public class GameEndResult : ResponseBase<GameEndResult>
    {
        /// <summary>
        /// Gets or sets game result
        /// </summary>
        public GameResult GameResult { get; set; }

        /// <summary>
        /// Gets or sets game winner
        /// </summary>
        public char Winner { get; set; }

        /// <summary>
        /// Gets or sets game winner name
        /// </summary>
        public string WinnerName { get; set; }

        /// <summary>
        /// Gets or sets game winning team
        /// </summary>
        public char WinningTeam { get; set; }

        /// <summary>
        /// Gets or sets game winning team name
        /// </summary>
        public string WinningTeamName { get; set; }
    }
}
