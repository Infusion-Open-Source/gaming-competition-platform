namespace Infusion.Gaming.LightCycles.Model.MapData
{
    public class Space : Location
    {
        /// <summary>
        /// Is location passable
        /// </summary>
        public override bool IsPassable { get { return true; } }
    }
}
