using System;

namespace Infusion.Networking
{
    /// <summary>
    /// Message with data for player command
    /// </summary>
    /// <typeparam name="T">Message type</typeparam>
    public class ResponseBase<T> : MessageBase
        where T : class, IMessage
    {
        /// <summary>
        /// Creates new instance of ResponseBase
        /// </summary>
        public ResponseBase()
        {
            this.MessageType = typeof(T).AssemblyQualifiedName;
        }

        /// <summary>
        /// Gets or sets status of response
        /// </summary>
        public ResponseStatus Status { get; set; }

        /// <summary>
        /// Gets or sets details on response status
        /// </summary>
        public string StatusMessage { get; set; }
    }
}
