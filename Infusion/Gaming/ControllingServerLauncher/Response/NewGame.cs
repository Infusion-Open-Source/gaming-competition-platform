namespace Infusion.Networking.ControllingServer.Response
{
    /// <summary>
    /// Message telling player where to connect to play new game
    /// </summary>
    public class NewGame : ResponseBase<NewGame>
    {
        /// <summary>
        /// Gets or sets game key
        /// </summary>
        public string GameKey { get; set; }

        /// <summary>
        /// Gets or sets game server address
        /// </summary>
        public string ServerAddress { get; set; }

        /// <summary>
        /// Gets or sets game server port
        /// </summary>
        public int ServerPort { get; set; }
    }
}
