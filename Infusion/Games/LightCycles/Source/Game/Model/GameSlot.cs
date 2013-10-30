namespace Infusion.Gaming.LightCycles.Model
{
    using System.Drawing;

    /// <summary>
    /// Game slot
    /// </summary>
    public class GameSlot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameSlot" /> class.
        /// </summary>
        /// <param name="slotNumber">game slot number</param>
        /// <param name="player">player identity</param>
        /// <param name="team">team identity</param>
        /// <param name="startLocation">starting location</param>
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
