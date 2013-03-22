
using System;

namespace Infusion.Gaming.LightCycles.Model.Data
{
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The location data on the player data map.
    /// </summary>
    public class LocationData
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationData"/> class.
        /// </summary>
        public LocationData()
            : this(null, PlayerDataTypeEnum.Undefined)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationData"/> class.
        /// </summary>
        public LocationData(Player player, PlayerDataTypeEnum playerDataType)
        {
            this.Player = player;
            this.PlayerDataType = playerDataType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationData"/> class.
        /// </summary>
        /// <param name="locationData">
        /// Players data map to be clonned
        /// </param>
        public LocationData(LocationData locationData)
        {
            if (locationData == null)
            {
                throw new ArgumentNullException("locationData");
            }

            this.Player = locationData.Player;
            this.PlayerDataType = locationData.PlayerDataType;
        }
        
        #endregion

        #region Public Properties
        /// <summary>
        /// Is location passable
        /// </summary>
        public bool IsPassable
        {
            get
            {
                return this.PlayerDataType == PlayerDataTypeEnum.Undefined;
            }
        }

        /// <summary>
        ///     Gets or sets the related player.
        /// </summary>
        public Player Player { get; protected set; }

        /// <summary>
        ///     Gets or sets the player data type.
        /// </summary>
        public PlayerDataTypeEnum PlayerDataType { get; protected set; }

        #endregion
    }
}