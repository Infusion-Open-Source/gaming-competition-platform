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
