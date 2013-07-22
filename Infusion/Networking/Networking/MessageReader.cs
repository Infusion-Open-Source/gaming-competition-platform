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
    public class MessageReader : IMessageReader
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
        /// Initializes a new instance of the <see cref="MessageReader" /> class.
        /// </summary>
        /// <param name="tcpClient">tcp client to use</param>
        /// <param name="messageSerializer">message serializer</param>
        public MessageReader(TcpClient tcpClient, IMessageSerializer messageSerializer)
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
        /// Reads in incoming messages
        /// </summary>
        /// <returns>set of incoming messages</returns>
        public MessageCollection Read()
        {
            MessageCollection dataIn = new MessageCollection();
            NetworkStream ns = this.TcpClient.GetStream();
            byte[] messageInBytes = new byte[100000];
            int count = ns.Read(messageInBytes, 0, 100000);
            string messagesInString = Encoding.ASCII.GetString(messageInBytes, 0, count);
            foreach (string message in messagesInString.Split(new [] { MessageBase.EndOfMessage }, StringSplitOptions.RemoveEmptyEntries))
            {
                string startTag = "<MessageType>";
                string endTag = "</MessageType>";
                int messageTypeStart = message.IndexOf(startTag, System.StringComparison.Ordinal);
                int messageTypeEnd = message.IndexOf(endTag, System.StringComparison.Ordinal);
                string messageType = message.Substring(messageTypeStart + startTag.Length, messageTypeEnd - (messageTypeStart + startTag.Length));
                Type type = Type.GetType(messageType);
                    
                var messageIn = this.MessageSerializer.Deserialize(message, type);
                messageIn.ReceiveTime = DateTime.Now.Ticks;
                dataIn.Add(messageIn);
            }

            return dataIn;
        }
    }
}
