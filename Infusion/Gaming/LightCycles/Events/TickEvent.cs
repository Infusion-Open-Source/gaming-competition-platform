// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TickEvent.cs" company="Infusion">
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
//   The tick event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Events
{
    using System;
    using System.Text;

    /// <summary>
    ///     The tick event.
    /// </summary>
    public class TickEvent : Event
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TickEvent"/> class.
        /// </summary>
        /// <param name="turn">
        /// Number of game turn.
        /// </param>
        public TickEvent(int turn)
        {
            if (turn < 0)
            {
                throw new ArgumentOutOfRangeException("turn");
            }

            this.Turn = turn;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets game turn.
        /// </summary>
        public int Turn { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     To string.
        /// </summary>
        /// <returns>
        ///     String representation of an object.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Turn {0} starts", this.Turn);
            return builder.ToString();
        }

        #endregion
    }
}