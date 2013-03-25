namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System;

    /// <summary>
    /// Represents players light trail on players data map
    /// </summary>
    public class Trail : PlayerGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trail"/> class.
        /// </summary>
        /// <param name="player">player owning light trail</param>
        /// <param name="age">trails age in turns</param>
        public Trail(Player player, int age)
            : base(player)
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
            return new Trail(this.Player, this.Age + 1);
        }
    }
}
