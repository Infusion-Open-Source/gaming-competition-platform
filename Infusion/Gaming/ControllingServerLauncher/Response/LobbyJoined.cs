namespace Infusion.Networking.ControllingServer.Response
{
    /// <summary>
    /// Message telling player that has joined the lobby
    /// </summary>
    public class LobbyJoined : ResponseBase<LobbyJoined>
    {
        /// <summary>
        /// Gets or sets player key
        /// </summary>
        public string PlayerKey { get; set; }
    }
}
