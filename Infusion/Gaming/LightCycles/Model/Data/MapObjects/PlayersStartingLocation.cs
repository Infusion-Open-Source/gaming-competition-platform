using System;
using System.Drawing;

namespace Infusion.Gaming.LightCycles.Model.Data.MapObjects
{
    /// <summary>
    /// Represents player starting location on a map
    /// </summary>
    public class PlayersStartingLocation : MapLocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersStartingLocation"/> class.
        /// </summary>
        /// <param name="location"> location coordinates. </param>
        /// <param name="player"> The player which should start in this location. </param>
        /// <param name="team"> The team which should start in this location. </param>
        public PlayersStartingLocation(Point location, Identity player, Identity team)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            if (team == null)
            {
                throw new ArgumentNullException("team");
            }

            this.Location = location;
            this.Player = player;
            this.Team = team;
        }

        /// <summary>
        /// Gets or sets the player which should start in this location.
        /// </summary>
        public Identity Player { get; protected set; }

        /// <summary>
        /// Gets or sets the team which should start in this location.
        /// </summary>
        public Identity Team { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether location is passable
        /// </summary>
        public override bool IsPassable
        {
            get
            {
                return true;
            }
        }
    }
}
