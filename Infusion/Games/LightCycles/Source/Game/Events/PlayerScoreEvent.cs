namespace Infusion.Gaming.LightCycles.Events
{
    using System.Text;
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// Player has earned points event
    /// </summary>
    public class PlayerScoreEvent : PlayerEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerScoreEvent"/> class.
        /// </summary>
        /// <param name="player">
        /// The player related to the event.
        /// </param>
        /// <param name="scoreSource">
        /// Source of the score
        /// </param>
        public PlayerScoreEvent(Identity player, PlayerScoreSource scoreSource)
            : base(player)
        {
            this.ScoreSource = scoreSource;
        }

        /// <summary>
        /// Gets or sets the number of points scored.
        /// </summary>
        public PlayerScoreSource ScoreSource { get; protected set; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>
        /// The string representation of an object.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}: {1}", this.Player, this.ScoreSource);
            return builder.ToString();
        }
    }
}
