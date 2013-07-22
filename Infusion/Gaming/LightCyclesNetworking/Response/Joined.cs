using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Response
{
    /// <summary>
    /// Message telling player that has joined the game
    /// </summary>
    public class Joined : ResponseBase<Joined>
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
