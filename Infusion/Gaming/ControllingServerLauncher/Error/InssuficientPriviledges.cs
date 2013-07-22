namespace Infusion.Networking.ControllingServer.Error
{
    /// <summary>
    /// Message telling player that has insufficient priviledges to perform action
    /// </summary>
    public class InssuficientPriviledges : ResponseBase<InssuficientPriviledges>
    {
        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }
    }
}
