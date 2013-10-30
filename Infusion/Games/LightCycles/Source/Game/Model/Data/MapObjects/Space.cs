namespace Infusion.Gaming.LightCycles.Model.Data.MapObjects
{
    using System.Drawing;

    /// <summary>
    /// Class representing passable map grid
    /// </summary>
    public class Space : MapLocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Space"/> class.
        /// </summary>
        /// <param name="location">space location</param>
        public Space(Point location)
        {
            this.Location = location;
        }

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
