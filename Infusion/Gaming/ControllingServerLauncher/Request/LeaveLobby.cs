namespace Infusion.Networking.ControllingServer.Request
{
    /// <summary>
    /// Player leave lobby request, the nice way of leaving the lobby
    /// </summary>
    public class LeaveLobby : RequestBase<LeaveLobby>
    {
        /// <summary>
        /// Gets or sets player key
        /// </summary>
        public string PlayerKey { get; set; }
    }
}
