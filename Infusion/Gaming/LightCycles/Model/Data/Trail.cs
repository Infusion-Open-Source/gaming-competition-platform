using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infusion.Gaming.LightCycles.Model.Data
{
    public class Trail : PlayerGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trail"/> class.
        /// </summary>
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
        /// Clones game object
        /// </summary>
        /// <returns>Clonned game object</returns>
        public override GameObject Clone()
        {
            return new Trail(this.Player, this.Age + 1);
        }

        /// <summary>
        /// Get or sets age of light trail
        /// </summary>
        public int Age { get; protected set; }
    }
}
