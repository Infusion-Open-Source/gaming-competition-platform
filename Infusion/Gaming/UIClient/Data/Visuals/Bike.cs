namespace UIClient.Data.Visuals
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
        /// <param name="teamId">owning team id</param>
        public Bike(char playerId, char teamId)
        {
            this.PlayerId = playerId;
            this.TeamId = teamId;
        }

        /// <summary>
        /// Gets or sets player id
        /// </summary>
        public char PlayerId { get; protected set; }

        /// <summary>
        /// Gets or sets team id
        /// </summary>
        public char TeamId { get; protected set; }
    }
}
