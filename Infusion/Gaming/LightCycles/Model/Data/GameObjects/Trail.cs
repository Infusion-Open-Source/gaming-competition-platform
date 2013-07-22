using System;
using System.Drawing;

namespace Infusion.Gaming.LightCycles.Model.Data.GameObjects
{
    /// <summary>
    /// Represents players light trail on players data map
    /// </summary>
    public class Trail : PlayerGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trail"/> class.
        /// </summary>
        /// <param name="location">location of an object</param>
        /// <param name="player">player owning light trail</param>
        /// <param name="age">trails age in turns</param>
        public Trail(Point location, Identity player, int age)
            : base(location, player)
        {
            if (age < 0)
            {
                throw new ArgumentOutOfRangeException("age");
            }

            this.Age = age;
        }

        /// <summary>
        /// Gets or sets age of light trail
        /// </summary>
        public int Age { get; protected set; }

        /// <summary>
        /// Clones game object
        /// </summary>
        /// <returns>Cloned game object</returns>
        public override GameObject Clone()
        {
            return new Trail(this.Location, this.Player, this.Age + 1);
        }
    }
}
