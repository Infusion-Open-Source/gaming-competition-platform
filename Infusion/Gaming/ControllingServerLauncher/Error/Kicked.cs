namespace Infusion.Networking.ControllingServer.Error
{
    /// <summary>
    /// Message telling player that was kicked
    /// </summary>
    public class Kicked : ResponseBase<Kicked>
    {
        /// <summary>
        /// Gets or sets message with reason of kick
        /// </summary>
        public string Message { get; set; }
    }
}
