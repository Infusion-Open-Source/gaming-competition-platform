namespace Infusion.Networking.ControllingServer.Response.NewFolder1
{
    /// <summary>
    /// Message with player info update
    /// </summary>
    public class PlayerInfoUpdate : ResponseBase<PlayerInfoUpdate>
    {
        /// <summary>
        /// Gets or sets game id
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets your player id
        /// </summary>
        public char PlayerId { get; set; }

        /// <summary>
        /// Gets or sets you team id
        /// </summary>
        public char TeamId { get; set; }
    }
}
