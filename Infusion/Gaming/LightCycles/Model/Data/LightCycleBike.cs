using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infusion.Gaming.LightCycles.Model.Defines;

namespace Infusion.Gaming.LightCycles.Model.Data
{
    public class LightCycleBike : PlayerGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightCycleBike"/> class.
        /// </summary>
        public LightCycleBike(Player player, DirectionEnum direction)
            : base(player)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// Clones game object
        /// </summary>
        /// <returns>Clonned game object</returns>
        public override GameObject Clone()
        {
            return new LightCycleBike(this.Player, this.Direction);
        }

        /// <summary>
        /// Gets or sets bike move direction
        /// </summary>
        public DirectionEnum Direction { get; protected set; }
    }
}
