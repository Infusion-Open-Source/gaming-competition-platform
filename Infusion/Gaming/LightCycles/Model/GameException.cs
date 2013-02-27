// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameException.cs" company="Infusion">
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
//   The game exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Infusion.Gaming.LightCycles.Model
{
    /// <summary>
    ///     The game exception.
    /// </summary>
    [Serializable]
    public class GameException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameException" /> class.
        /// </summary>
        public GameException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        public GameException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        /// <param name="internalExcpetion">
        /// The internal exception.
        /// </param>
        public GameException(string message, Exception internalExcpetion)
            : base(message, internalExcpetion)
        {
        }

        #endregion
    }
}