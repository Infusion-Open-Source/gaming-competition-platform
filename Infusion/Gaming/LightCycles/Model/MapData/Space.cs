namespace Infusion.Gaming.LightCycles.Model.MapData
{
    /// <summary>
    /// Class representing passable map grid
    /// </summary>
    public class Space : Location
    {
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
