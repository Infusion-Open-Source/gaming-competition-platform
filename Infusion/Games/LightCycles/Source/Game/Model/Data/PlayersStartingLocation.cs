using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infusion.Gaming.LightCycles.Model.Defines;

namespace Infusion.Gaming.LightCycles.Model.Data
{
    public class PlayersStartingLocation : Location
    {
        /// <summary>
        ///     Gets or sets the player which should start in this location.
        /// </summary>
        public Player Player { get; protected set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersStartingLocation"/> class.
        /// </summary>
        /// <param name="player">
        /// The player which should start in this location.
        /// </param>
        public PlayersStartingLocation(Player player)
            : base(LocationTypeEnum.PlayersStartingLocation)
        {
            if(player == null)
            {
                throw new ArgumentNullException("player");
            }

            this.Player = player;
        }
    }
}
