using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infusion.Networking
{
    /// <summary>
    /// Thread safe generic message buffer
    /// </summary>
    public class MessageBuffer
    {
        /// <summary>
        /// internal message buffer
        /// </summary>
        protected Queue<IMessage> messageBuffer = new Queue<IMessage>();

        /// <summary>
        /// Sync root object
        /// </summary>
        public object syncRoot = new object();
        
        /// <summary>
        /// Gets buffer count
        /// </summary>
        public int Count
        {
            get
            {
                lock (syncRoot)
                {
                    return this.messageBuffer.Count;
                }
            }
        }

        /// <summary>
        /// Enqueue message on a buffer
        /// </summary>
        /// <param name="message">message to enqueue</param>
        public void Enqueue(IMessage message)
        {
            lock (syncRoot)
            {
                this.messageBuffer.Enqueue(message);
            }
        }

        /// <summary>
        /// Dequeue message from the buffer
        /// </summary>
        /// <returns>Dequeueed message</returns>
        public IMessage Dequeue()
        {
            IMessage message = null;
            lock (syncRoot)
            {
                if (this.messageBuffer.Count > 0)
                {
                    message = this.messageBuffer.Dequeue();
                }
            }

            return message;
        }

        /// <summary>
        /// Clear the buffer
        /// </summary>
        public void Clear()
        {
            lock (syncRoot)
            {
                this.messageBuffer.Clear();
                this.messageBuffer.TrimExcess();
            }
        }
    }
}
