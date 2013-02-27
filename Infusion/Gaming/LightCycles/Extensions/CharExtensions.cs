// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharExtensions.cs" company="Infusion">
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
//   The char type extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Extensions
{
    /// <summary>
    ///     The char type extensions.
    /// </summary>
    public static class CharExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Change character to lower case.
        /// </summary>
        /// <param name="c">
        /// The character to change.
        /// </param>
        /// <returns>
        /// The changed character.
        /// </returns>
        public static char ToLower(this char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return (char)(c + ('a' - 'A'));
            }

            return c;
        }

        /// <summary>
        /// Change character to upper case.
        /// </summary>
        /// <param name="c">
        /// The character to change.
        /// </param>
        /// <returns>
        /// The changed character.
        /// </returns>
        public static char ToUpper(this char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return (char)(c + ('A' - 'a'));
            }

            return c;
        }

        #endregion
    }
}