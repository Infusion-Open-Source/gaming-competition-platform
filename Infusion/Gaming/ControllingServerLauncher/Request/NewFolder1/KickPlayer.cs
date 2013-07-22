namespace Infusion.Networking.ControllingServer.Request.NewFolder1
{
    /// <summary>
    /// Request kicking player off the lobby
    /// </summary>
    public class KickPlayer : RequestBase<KickPlayer>
    {
        /// <summary>
        /// Gets or sets player key
        /// </summary>
        public string PlayerKey { get; set; }
    }
}
