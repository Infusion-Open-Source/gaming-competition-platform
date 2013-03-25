namespace Infusion.Gaming.LightCycles.Model.MapData
{
    public class Obstacle : Location
    {
        /// <summary>
        /// Is location passable
        /// </summary>
        public override bool IsPassable { get { return false; } }
    }
}
