using System;
using System.Text;

namespace Infusion.Networking
{
    /// <summary>
    /// General network message interface
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// When message was send, ticks
        /// </summary>
        long SendTime { get; set; }

        /// <summary>
        /// When message we received, ticks
        /// </summary>
        long ReceiveTime { get; set; }

        /// <summary>
        /// Gets or sets message type
        /// </summary>
        string MessageType { get; set; }
    }
}
