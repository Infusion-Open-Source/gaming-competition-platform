using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Request
{
    /// <summary>
    /// Request for game end result
    /// </summary>
    public class GetGameEndResult : RequestBase<GetGameEndResult>
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
