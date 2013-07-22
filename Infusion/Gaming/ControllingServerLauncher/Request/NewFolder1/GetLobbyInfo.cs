namespace Infusion.Networking.ControllingServer.Request.NewFolder1
{
    /// <summary>
    /// Requests lobby info
    /// </summary>
    public class GetLobbyInfo : RequestBase<GetLobbyInfo>
    {
        /// <summary>
        /// Gets or sets game id
        /// </summary>
        public int GameId { get; set; }
    }
}
