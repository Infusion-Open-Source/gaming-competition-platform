
namespace Infusion.Gaming.LightCycles.Events
{
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    ///     The base class for player event.
    /// </summary>
    public abstract class PlayerEvent : Event
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerEvent"/> class.
        /// </summary>
        /// <param name="player">
        /// The player related to the event.
        /// </param>
        protected PlayerEvent(Player player)
        {
            this.Player = player;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the player.
        /// </summary>
        public Player Player { get; protected set; }

        #endregion
    }
}