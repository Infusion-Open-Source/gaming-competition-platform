namespace Infusion.Networking.ControllingServer.Request.NewFolder1
{
    /// <summary>
    /// Fetch all games
    /// </summary>
    public class GetGames : RequestBase<GetGames>
    {
        /// <summary>
        /// Gets or sets player key
        /// </summary>
        public string PlayerKey { get; set; }
    }
}
