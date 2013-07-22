using System;

namespace Infusion.Networking
{
    /// <summary>
    /// Message with data for player command
    /// </summary>
    /// <typeparam name="T">Message type</typeparam>
    public class RequestBase<T> : MessageBase
        where T : class, IMessage
    {
        /// <summary>
        /// Creates new instance of ResponseBase
        /// </summary>
        public RequestBase()
        {
            this.MessageType = typeof(T).AssemblyQualifiedName;
        }
    }
}
