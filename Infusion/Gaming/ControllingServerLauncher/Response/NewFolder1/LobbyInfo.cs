namespace Infusion.Networking.ControllingServer.Response.NewFolder1
{
    /// <summary>
    /// Message with lobby info
    /// </summary>
    public class LobbyInfo : ResponseBase<LobbyInfo>
    {
        /// <summary>
        /// Gets or sets game id
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets game mode
        /// </summary>
        //public GameMode GameMode { get; set; }

        /// <summary>
        /// Gets or sets number of game slots
        /// </summary>
        public int GameSlots { get; set; }

        /// <summary>
        /// Gets or sets number of game free slots
        /// </summary>
        public int GameFreeSlots { get; set; }

        /// <summary>
        /// Gets or sets players ids of each slot
        /// </summary>
        public char[] SlotPlayerId { get; set; }

        /// <summary>
        /// Gets or sets players name of each slot
        /// </summary>
        public string[] SlotPlayerName { get; set; }

        /// <summary>
        /// Gets or sets team ids of each slot
        /// </summary>
        public char[] SlotTeamId { get; set; }

        /// <summary>
        /// Gets or sets team name of each slot
        /// </summary>
        public string[] SlotTeamName { get; set; }
    }
}
