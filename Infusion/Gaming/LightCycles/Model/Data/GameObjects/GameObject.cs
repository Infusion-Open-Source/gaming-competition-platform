using System.Drawing;

namespace Infusion.Gaming.LightCycles.Model.Data.GameObjects
{
    /// <summary>
    /// The game object on player data map.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Gets or sets object location
        /// </summary>
        public Point Location { get; protected set; }

        /// <summary>
        /// Clones game object
        /// </summary>
        /// <returns>Cloned game object</returns>
        public abstract GameObject Clone();
    }
}