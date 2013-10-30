
namespace Infusion.Gaming.LightCycles.Model.Data
{
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The location on the map.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Is location passable
        /// </summary>
        public bool IsPassable
        {
            get
            {
                return this.LocationType == LocationTypeEnum.Space 
                    || this.LocationType == LocationTypeEnum.PlayersStartingLocation;
            }
        }

        /// <summary>
        ///     Gets or sets the type of the location.
        /// </summary>
        public LocationTypeEnum LocationType { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="locationType">
        /// The type of the location.
        /// </param>
        public Location(LocationTypeEnum locationType)
        {
            this.LocationType = locationType;
        }
    }
}