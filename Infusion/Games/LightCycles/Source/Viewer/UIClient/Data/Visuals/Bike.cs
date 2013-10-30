namespace Infusion.Gaming.LightCycles.UIClient.Data.Visuals
{
    /// <summary>
    /// Bike visual
    /// </summary>
    public class Bike : IVisual
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bike"/> class.
        /// </summary>
        /// <param name="playerId">owning player id</param>
        public Bike(GameIdentity playerId)
        {
            this.PlayerId = playerId;
        }

        /// <summary>
        /// Gets or sets player id
        /// </summary>
        public GameIdentity PlayerId { get; protected set; }
    }
}
