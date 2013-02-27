// -----------------------------------------------------------------------
// <copyright file="ListExtensions.cs" company="Infusion">
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
//   The players collection.
// </summary>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Infusion.Gaming.LightCycles.Extensions
{
    /// <summary>
    /// Extension of generic lists.
    /// </summary>
    internal static class ListExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Get collection being intersection with given collection.
        /// </summary>
        /// <param name="listA">
        /// First collection to intersect.
        /// </param>
        /// <param name="listB">
        /// Second collection to intersect.
        /// </param>
        /// <returns>
        /// The collection having items that are in both collections.
        /// </returns>
        public static IList<T> Intersect<T>(this IList<T> listA, IList<T> listB)
        {
            var results = new List<T>();
            foreach (T item in listB)
            {
                if (!listA.Contains(item))
                {
                    listA.Add(item);
                }
            }

            return results;
        }

        /// <summary>
        /// Removes from collection players that at given collection.        
        /// </summary>
        /// <param name="listA">
        /// The list from which items should be removed.
        /// </param>
        /// <param name="listB">
        /// The players to be removed.
        /// </param>
        /// <returns>
        /// The collection with removed items.
        /// </returns>
        public static IList<T> Remove<T>(this IList<T> listA, IList<T> listB)
        {
            var results = new List<T>();
            foreach (T item in listB)
            {
                if (listA.Contains(item))
                {
                    listA.Remove(item);
                }
            }

            return results;
        }
        #endregion
    }
}
