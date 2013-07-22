using System;
using System.Collections.Generic;
using System.Text;

namespace Infusion.Networking
{
    /// <summary>
    /// General network message interface
    /// </summary>
    public class MessageCollection : List<IMessage>
    {
        /// <summary>
        /// Get first instance of message of given type
        /// </summary>
        /// <typeparam name="T">type of message</typeparam>
        /// <returns>Message</returns>
        public T First<T>()
            where T : class, IMessage 
        {
            foreach (IMessage message in this)
            {
                if(message is T)
                {
                    return (T)message;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks wheter list contains message of specified type
        /// </summary>
        /// <typeparam name="T">type of message</typeparam>
        /// <returns>vlaue indicating wheter list contains message of specified type</returns>
        public bool Contains<T>()
            where T : class, IMessage
        {
            foreach (IMessage message in this)
            {
                if (message is T)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
