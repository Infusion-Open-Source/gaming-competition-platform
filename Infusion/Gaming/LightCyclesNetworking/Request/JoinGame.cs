using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Request
{
    /// <summary>
    /// Request for game join
    /// </summary>
    public class JoinGame : RequestBase<JoinGame>
    {
        /// <summary>
        /// Gets or sets player key
        /// </summary>
        public string PlayerKey { get; set; }

        /// <summary>
        /// Gets or sets game key
        /// </summary>
        public string GameKey { get; set; }
    }
}
