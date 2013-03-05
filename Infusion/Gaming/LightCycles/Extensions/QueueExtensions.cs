// -----------------------------------------------------------------------
// <copyright file="QueueExtensions.cs" company="Infusion">
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

namespace Infusion.Gaming.LightCycles.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension of generic Queue.
    /// </summary>
    internal static class QueueExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Enqueues collection of items
        /// </summary>
        /// <typeparam name="T">Type of item to enqueue</typeparam>
        /// <param name="queue">
        /// Queue to extend
        /// </param>
        /// <param name="list">
        /// Collection of items to enqueue
        /// </param>
        /// <returns>
        /// The queue having list of items enqueued.
        /// </returns>
        public static Queue<T> Enqueue<T>(this Queue<T> queue, IEnumerable<T> list)
        {
            foreach (T item in list)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        #endregion
    }
}
