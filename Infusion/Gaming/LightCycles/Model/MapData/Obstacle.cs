namespace Infusion.Gaming.LightCycles.Model.MapData
{
    /// <summary>
    /// Class represents impassable obstacle on a map
    /// </summary>
    public class Obstacle : Location
    {
        /// <summary>
        /// Gets a value indicating whether location is passable
        /// </summary>
        public override bool IsPassable
        {
            get
            {
                return false;
            }
        }
    }
}
