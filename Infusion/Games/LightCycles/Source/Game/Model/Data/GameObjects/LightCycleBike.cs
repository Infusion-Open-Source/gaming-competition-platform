namespace Infusion.Gaming.LightCycles.Model.Data.GameObjects
{
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Definitions;

    /// <summary>
    /// Class represents light cycle bike on players object map
    /// </summary>
    public class LightCycleBike : PlayerGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightCycleBike"/> class.
        /// </summary>
        /// <param name="location">location of an object</param>
        /// <param name="player">player owning light cycle bike</param>
        /// <param name="direction">direction in which bike is oriented</param>
        public LightCycleBike(Point location, Identity player, Direction direction)
            : base(location, player)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// Gets or sets bike move direction
        /// </summary>
        public Direction Direction { get; protected set; }

        /// <summary>
        /// Clones game object
        /// </summary>
        /// <returns>Cloned game object</returns>
        public override GameObject Clone()
        {
            return new LightCycleBike(this.Location, this.Player, this.Direction);
        }
    }
}
