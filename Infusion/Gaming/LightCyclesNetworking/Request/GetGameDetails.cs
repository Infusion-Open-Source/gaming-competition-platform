using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Request
{
    /// <summary>
    /// Request for game details
    /// </summary>
    public class GetGameDetails : RequestBase<GetGameDetails>
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
