namespace Infusion.Networking.ControllingServer.Request
{
    /// <summary>
    /// Player search for game request
    /// </summary>
    public class FindGame : RequestBase<FindGame>
    {
        /// <summary>
        /// Gets or sets player key
        /// </summary>
        public string PlayerKey { get; set; }

        /// <summary>
        /// Gets or sets game mode which player wants to join
        /// </summary>
        //public GameMode GameMode { get; set; }
    }
}
