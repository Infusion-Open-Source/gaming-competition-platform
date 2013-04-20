namespace Infusion.Gaming.LightCycles.Model.Data
{
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// Class represents light cycle bike on players object map
    /// </summary>
    public class LightCycleBike : PlayerGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightCycleBike"/> class.
        /// </summary>
        /// <param name="player">player owning light cycle bike</param>
        /// <param name="direction">direction in which bike is oriented</param>
        public LightCycleBike(Player player, DirectionEnum direction)
            : base(player)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// Gets or sets bike move direction
        /// </summary>
        public DirectionEnum Direction { get; protected set; }

        /// <summary>
        /// Clones game object
        /// </summary>
        /// <returns>Cloned game object</returns>
        public override GameObject Clone()
        {
            return new LightCycleBike(this.Player, this.Direction);
        }
    }
}
