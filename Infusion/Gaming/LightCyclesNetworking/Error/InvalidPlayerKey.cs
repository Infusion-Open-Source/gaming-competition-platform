using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Error
{
    /// <summary>
    /// Message telling player that provided player key is invalid
    /// </summary>
    public class InvalidPlayerKey : ResponseBase<InvalidPlayerKey>
    {
        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }
    }
}
