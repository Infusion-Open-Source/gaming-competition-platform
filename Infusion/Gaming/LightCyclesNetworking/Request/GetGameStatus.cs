using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Request
{
    /// <summary>
    /// Game status query
    /// </summary>
    public class GetGameStatus : RequestBase<GetGameStatus>
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
