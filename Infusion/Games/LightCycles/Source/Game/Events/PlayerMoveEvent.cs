namespace Infusion.Gaming.LightCycles.Events
{
    using System.Text;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// The player move event.
    /// </summary>
    public class PlayerMoveEvent : PlayerEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMoveEvent"/> class.
        /// </summary>
        /// <param name="player">
        /// The player related to the event.
        /// </param>
        /// <param name="direction">
        /// The direction on which player wants to move.
        /// </param>
        public PlayerMoveEvent(Identity player, RelativeDirection direction)
            : base(player)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public RelativeDirection Direction { get; protected set; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>
        /// The string representation of an object.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}: {1}", this.Player, this.Direction);
            return builder.ToString();
        }
    }
}