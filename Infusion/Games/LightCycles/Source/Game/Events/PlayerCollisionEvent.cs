namespace Infusion.Gaming.LightCycles.Events
{
    using System.Text;
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// The player collision event.
    /// </summary>
    public class PlayerCollisionEvent : PlayerEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCollisionEvent"/> class.
        /// </summary>
        /// <param name="player">
        /// The player which collides.
        /// </param>
        public PlayerCollisionEvent(Identity player)
            : base(player)
        {
        }

        /// <summary>
        /// Get string.
        /// </summary>
        /// <returns>
        /// The string representation of an object.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}: collides", this.Player);
            return builder.ToString();
        }
    }
}