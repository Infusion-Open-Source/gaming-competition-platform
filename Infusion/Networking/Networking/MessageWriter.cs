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
    public class MessageWriter : IMessageWriter
    {
        /// <summary>
        /// Gets or sets tcp client to use
        /// </summary>
        public TcpClient TcpClient { get; protected set; }
        
        /// <summary>
        /// Gets or sets message serializer
        /// </summary>
        public IMessageSerializer MessageSerializer { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageWriter" /> class.
        /// </summary>
        /// <param name="tcpClient">tcp client to use</param>
        /// <param name="messageSerializer">message serializer</param>
        public MessageWriter(TcpClient tcpClient, IMessageSerializer messageSerializer)
        {
            if (tcpClient == null)
            {
                throw new ArgumentNullException("tcpClient");
            }

            if (messageSerializer == null)
            {
                throw new ArgumentNullException("messageSerializer");
            }

            this.TcpClient = tcpClient;
            this.MessageSerializer = messageSerializer;
        }

        /// <summary>
        /// Write message
        /// </summary>
        /// <param name="messageOut">message out</param>
        public void Write(IMessage messageOut)
        {
            if (messageOut != null)
            {
                messageOut.SendTime = DateTime.Now.Ticks;
                Type type = messageOut.GetType();
                string messageOutString = this.MessageSerializer.Serialize(messageOut, type) + MessageBase.EndOfMessage;
                byte[] messageOutBytes = Encoding.ASCII.GetBytes(messageOutString);
                NetworkStream ns = this.TcpClient.GetStream();
                ns.Write(messageOutBytes, 0, messageOutBytes.Length);
            }
        }

        /// <summary>
        /// Writes set of messages
        /// </summary>
        /// <param name="messages">messages to be send</param>
        public void Write(IEnumerable<IMessage> messages)
        {
            if (messages != null)
            {
                foreach (IMessage message in messages)
                {
                    this.Write(message);
                }
            }
        }
    }
}
