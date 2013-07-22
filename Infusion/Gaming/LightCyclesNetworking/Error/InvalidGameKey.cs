using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Error
{
    /// <summary>
    /// Message telling player that provided game key is invalid
    /// </summary>
    public class InvalidGameKey : ResponseBase<InvalidGameKey>
    {
        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }
    }
}
