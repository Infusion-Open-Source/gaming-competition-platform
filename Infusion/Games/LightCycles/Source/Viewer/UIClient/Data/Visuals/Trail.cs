namespace Infusion.Gaming.LightCycles.UIClient.Data.Visuals
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
        /// <param name="age">trails age</param>
        public Trail(GameIdentity playerId, int age)
        {
            this.PlayerId = playerId;
            this.Age = age;
        }

        /// <summary>
        /// Gets or sets player id
        /// </summary>
        public GameIdentity PlayerId { get; protected set; }
        
        /// <summary>
        /// Gets or sets trail age
        /// </summary>
        public int Age { get; protected set; }
    }
}
