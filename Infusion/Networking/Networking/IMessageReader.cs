using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Infusion.Networking
{
    /// <summary>
    /// Reads messages from tcp client
    /// </summary>
    public interface IMessageReader
    {
        /// <summary>
        /// Reads in incoming messages
        /// </summary>
        /// <returns>set of incoming messages</returns>
        MessageCollection Read();
    }
}
