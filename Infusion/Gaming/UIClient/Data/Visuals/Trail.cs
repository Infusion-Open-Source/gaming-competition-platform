namespace UIClient.Data.Visuals
{
    /// <summary>
    /// Trail visual
    /// </summary>
    public class Trail : IVisual
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trail"/> class.
        /// </summary>
        /// <param name="playerId">owning player id</param>
        /// <param name="teamId">owning team id</param>
        /// <param name="age">trails age</param>
        public Trail(char playerId, char teamId, int age)
        {
            this.PlayerId = playerId;
            this.TeamId = teamId;
            this.Age = age;
        }

        /// <summary>
        /// Gets or sets player id
        /// </summary>
        public char PlayerId { get; protected set; }

        /// <summary>
        /// Gets or sets team id
        /// </summary>
        public char TeamId { get; protected set; }

        /// <summary>
        /// Gets or sets trail age
        /// </summary>
        public int Age { get; protected set; }
    }
}
