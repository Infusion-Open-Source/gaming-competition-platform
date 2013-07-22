namespace Infusion.Networking.ControllingServer.Response.NewFolder1
{
    /// <summary>
    /// Message telling that player has been elevated
    /// </summary>
    public class Elevated : ResponseBase<Elevated>
    {
        /// <summary>
        /// Gets or sets elevated player key
        /// </summary>
        public string PlayerKey { get; set; }
    }
}
