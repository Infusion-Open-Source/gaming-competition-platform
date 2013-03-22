using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infusion.Gaming.LightCycles.Model.Data
{
    public class LightCycleBike : PlayerGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightCycleBike"/> class.
        /// </summary>
        public LightCycleBike(Player player)
            : base(player)
        {
        }

        /// <summary>
        /// Clones game object
        /// </summary>
        /// <returns>Clonned game object</returns>
        public override GameObject Clone()
        {
            return new LightCycleBike(this.Player);
        }
    }
}
