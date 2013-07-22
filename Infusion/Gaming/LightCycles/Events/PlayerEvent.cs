using Infusion.Gaming.LightCycles.Model;

namespace Infusion.Gaming.LightCycles.Events
{
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The base class for player event.
    /// </summary>
    public abstract class PlayerEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerEvent"/> class.
        /// </summary>
        /// <param name="player">
        /// The player related to the event.
        /// </param>
        protected PlayerEvent(Identity player)
        {
            this.Player = player;
        }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public Identity Player { get; protected set; }
    }
}