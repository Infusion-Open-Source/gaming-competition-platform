namespace Infusion.Gaming.LightCycles.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension of generic lists.
    /// </summary>
    internal static class ListExtensions
    {
        /// <summary>
        /// Get collection being intersection with given collection.
        /// </summary>
        /// <typeparam name="T">type of item to remove</typeparam>
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
                if (listA.Contains(item))
                {
                    results.Add(item);
                }
            }

            return results;
        }

        /// <summary>
        /// Removes from collection players that at given collection.        
        /// </summary>
        /// <typeparam name="T">type of item to remove</typeparam>
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
    }
}
