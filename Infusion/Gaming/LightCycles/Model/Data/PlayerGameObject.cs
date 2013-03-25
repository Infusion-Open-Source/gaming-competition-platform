namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System;

    /// <summary>
    /// The game object on player data map owned by specific player.
    /// </summary>
    public abstract class PlayerGameObject : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerGameObject"/> class.
        /// </summary>
        /// <param name="player">player owning an object</param>
        protected PlayerGameObject(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            this.Player = player;
        }

        /// <summary>
        /// Gets or sets the related player.
        /// </summary>
        public Player Player { get; protected set; }
    }
}