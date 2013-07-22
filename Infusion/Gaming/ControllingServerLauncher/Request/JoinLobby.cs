namespace Infusion.Networking.ControllingServer.Request
{
    /// <summary>
    /// Player lobby join request, if account is not ready then will be automatically created
    /// </summary>
    public class JoinLobby : RequestBase<JoinLobby>
    {
        /// <summary>
        /// Gets or sets player name
        /// </summary>
        public string PlayerName { get; set; }
    }
}
