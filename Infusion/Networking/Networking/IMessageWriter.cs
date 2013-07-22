using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Infusion.Networking
{
    /// <summary>
    /// Writes messages to tcp client
    /// </summary>
    public interface IMessageWriter
    {
        /// <summary>
        /// Write message
        /// </summary>
        /// <param name="messageOut">message out</param>
        void Write(IMessage messageOut);

        /// <summary>
        /// Writes set of messages
        /// </summary>
        /// <param name="messages">messages to be send</param>
        void Write(IEnumerable<IMessage> messages);
    }
}
