namespace Infusion.Gaming.LightCyclesCommon.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension of generic Queue.
    /// </summary>
    public static class QueueExtensions
    {
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
    }
}
