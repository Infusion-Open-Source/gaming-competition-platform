using System.Drawing;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Response
{
    /// <summary>
    /// Response with game parameters
    /// </summary>
    public class GameDetails : ResponseBase<GameDetails>
    {
        /// <summary>
        /// Gets or sets your player id
        /// </summary>
        public char MyPlayerId { get; set; }

        /// <summary>
        /// Gets or sets you team id
        /// </summary>
        public char MyTeamId { get; set; }

        /// <summary>
        /// Gets or sets game map name
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets game map width
        /// </summary>
        public int MapWidth { get; set; }

        /// <summary>
        /// Gets or sets game map height
        /// </summary>
        public int MapHeight { get; set; }

        /// <summary>
        /// Gets or sets game data
        /// </summary>
        public string MapData { get; set; }

        /// <summary>
        /// Gets or sets game mode
        /// </summary>
        public GameMode GameMode { get; set; }

        /// <summary>
        /// Gets or sets number of game slots
        /// </summary>
        public int GameSlots { get; set; }
        
        /// <summary>
        /// Gets or sets players ids of each slot
        /// </summary>
        public char[] PlayerId { get; set; }

        /// <summary>
        /// Gets or sets players name of each slot
        /// </summary>
        public string[] PlayerName { get; set; }

        /// <summary>
        /// Gets or sets players name of each slot
        /// </summary>
        public Color[] PlayerColor { get; set; }

        /// <summary>
        /// Gets or sets team ids of each slot
        /// </summary>
        public char[] TeamId { get; set; }

        /// <summary>
        /// Gets or sets team name of each slot
        /// </summary>
        public string[] TeamName { get; set; }

        /// <summary>
        /// Gets or sets team name of each slot
        /// </summary>
        public Color[] TeamColor { get; set; }
    }
}
