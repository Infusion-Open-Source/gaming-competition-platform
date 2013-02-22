// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Location.cs" company="Infusion">
//    Copyright (C) 2013 Paweł Drozdowski
//
//    This file is part of LightCycles Game Engine.
//
//    LightCycles Game Engine is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    LightCycles Game Engine is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with LightCycles Game Engine.  If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
//   The location on the map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Model.Data
{
    /// <summary>
    ///     The location on the map.
    /// </summary>
    public class Location
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="locationType">
        /// The type of the location.
        /// </param>
        /// <param name="player">
        /// The player related to the location.
        /// </param>
        public Location(LocationTypeEnum locationType, Player player)
        {
            this.LocationType = locationType;
            this.Player = player;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="locationType">
        /// The type of the location.
        /// </param>
        public Location(LocationTypeEnum locationType)
            : this(locationType, null)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the type of the location.
        /// </summary>
        public LocationTypeEnum LocationType { get; protected set; }

        /// <summary>
        ///     Gets or sets the related player.
        /// </summary>
        public Player Player { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Check if equals.
        /// </summary>
        /// <param name="obj">
        /// The object to compare to.
        /// </param>
        /// <returns>
        /// The result of comparison.
        /// </returns>
        public override bool Equals(object obj)
        {
            var objLocation = obj as Location;
            if (objLocation != null)
            {
                if (obj.GetHashCode() != this.GetHashCode())
                {
                    return false;
                }

                if (this.LocationType != objLocation.LocationType)
                {
                    return false;
                }

                if (this.Player == null && objLocation.Player == null)
                {
                    return true;
                }

                if (this.Player != null && objLocation.Player != null && this.Player.Id == objLocation.Player.Id)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        ///     Gets the hash code of the object.
        /// </summary>
        /// <returns>
        ///     The hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return (int)this.LocationType * (this.Player != null ? this.Player.Id : 1);
        }

        #endregion
    }
}