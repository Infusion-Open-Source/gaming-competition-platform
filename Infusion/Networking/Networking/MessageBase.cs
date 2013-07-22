using System;

namespace Infusion.Networking
{
    /// <summary>
    /// Message base class
    /// </summary>
    public class MessageBase : IMessage
    {
        /// <summary>
        /// EndOfMessage mark
        /// </summary>
        public const string EndOfMessage = "<!--EOF-->";

        /// <summary>
        /// Gets or sets timestamp when message was send
        /// </summary>
        public long SendTime { get; set; }

        /// <summary>
        /// Gets or sets timestamp when message was received
        /// </summary>
        public long ReceiveTime { get; set; }

        /// <summary>
        /// Gets or sets message type
        /// </summary>
        public string MessageType { get; set; }
    }
}
