namespace Infusion.Gaming.LightCycles.Model.MapData
{
    using System;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// Represents player starting location on a map
    /// </summary>
    public class PlayersStartingLocation : Location
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersStartingLocation"/> class.
        /// </summary>
        /// <param name="player">
        /// The player which should start in this location.
        /// </param>
        public PlayersStartingLocation(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            this.Player = player;
        }

        /// <summary>
        /// Gets or sets the player which should start in this location.
        /// </summary>
        public Player Player { get; protected set; }

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
