// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerCollisionEvent.cs" company="Infusion">
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
//   The player collision event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Events
{
    using System.Text;

    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    ///     The player collision event.
    /// </summary>
    public class PlayerCollisionEvent : PlayerEvent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCollisionEvent"/> class.
        /// </summary>
        /// <param name="player">
        /// The player which collides.
        /// </param>
        public PlayerCollisionEvent(Player player)
            : base(player)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Get string.
        /// </summary>
        /// <returns>
        ///     The string representation of an object.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}: collides", this.Player);
            return builder.ToString();
        }

        #endregion
    }
}