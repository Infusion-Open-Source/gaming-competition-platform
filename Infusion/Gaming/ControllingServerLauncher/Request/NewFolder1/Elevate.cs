namespace Infusion.Networking.ControllingServer.Request.NewFolder1
{
    /// <summary>
    /// Request evelating players priviledges
    /// </summary>
    public class Elevate : RequestBase<Elevate>
    {
        /// <summary>
        /// Gets or sets player key
        /// </summary>
        public string PlayerKey { get; set; }

        /// <summary>
        /// Gets or sets player to elevate key
        /// </summary>
        public string PlayerToElevateKey { get; set; }
    }
}
