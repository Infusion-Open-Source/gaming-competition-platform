// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerMoveEvent.cs" company="Infusion">
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
//   The player move event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Events
{
    using System.Text;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The player move event.
    /// </summary>
    public class PlayerMoveEvent : PlayerEvent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMoveEvent"/> class.
        /// </summary>
        /// <param name="player">
        /// The player related to the event.
        /// </param>
        /// <param name="direction">
        /// The direction on which player wants to move.
        /// </param>
        public PlayerMoveEvent(Player player, RelativeDirectionEnum direction)
            : base(player)
        {
            this.Direction = direction;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        public RelativeDirectionEnum Direction { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     To string.
        /// </summary>
        /// <returns>
        ///     The string representation of an object.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}: {1}", this.Player, this.Direction);
            return builder.ToString();
        }

        #endregion
    }
}