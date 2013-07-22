using System.Drawing;

namespace Infusion.Gaming.LightCycles.Model
{
    /// <summary>
    /// Game slot
    /// </summary>
    public class GameSlot
    {
        /// <summary>
        /// Creates new instance of game slot
        /// </summary>
        /// <param name="slotNumber"></param>
        /// <param name="player"></param>
        /// <param name="team"></param>
        /// <param name="startLocation"></param>
        public GameSlot(int slotNumber, Identity player, Identity team, Point startLocation)
        {
            this.SlotNumber = slotNumber;
            this.Player = player;
            this.Team = team;
            this.StartLocation = startLocation;
        }

        /// <summary>
        /// Gets or sets slot player
        /// </summary>
        public int SlotNumber { get; protected set; }

        /// <summary>
        /// Gets or sets slot player
        /// </summary>
        public Identity Player { get; protected set; }
        
        /// <summary>
        /// Gets or sets slot team
        /// </summary>
        public Identity Team { get; protected set; }
        
        /// <summary>
        /// Gets or sets slot start location
        /// </summary>
        public Point StartLocation { get; protected set; }
    }
}
