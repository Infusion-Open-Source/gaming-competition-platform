using System;
using System.Drawing;

namespace Infusion.Gaming.LightCycles.Model.Data.GameObjects
{
    /// <summary>
    /// The game object on player data map owned by specific player.
    /// </summary>
    public abstract class PlayerGameObject : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerGameObject"/> class.
        /// </summary>
        /// <param name="location">location of an object</param>
        /// <param name="player">player owning an object</param>
        protected PlayerGameObject(Point location, Identity player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            this.Player = player;
            this.Location = location;
        }

        /// <summary>
        /// Gets or sets the related player.
        /// </summary>
        public Identity Player { get; protected set; }
    }
}