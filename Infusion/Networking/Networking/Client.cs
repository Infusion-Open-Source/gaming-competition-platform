using System.Collections.Generic;
using System.Net;

namespace Infusion.Networking
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Networking client
    /// </summary>
    public class Client
    {
        /// <summary>
        /// TCP client to be used
        /// </summary>
        public TcpClient TcpClient { get; protected set; }
        
        /// <summary>
        /// Gets name of local endpoint
        /// </summary>
        public string LocalEndPointName
        {
            get
            {
                try
                {
                    return this.TcpClient.Client.LocalEndPoint.ToString();
                }
                catch (Exception)
                {
                    return "?";
                }
            }
        }

        /// <summary>
        /// Gets name of remote endpoint
        /// </summary>
        public string RemoteEndPointName
        {
            get
            {
                try
                {
                    return this.TcpClient.Client.RemoteEndPoint.ToString();
                }
                catch (Exception)
                {
                    return "?";
                }
            }
        }

        /// <summary>
        /// Gets or sets erver endp oint
        /// </summary>
        public IPEndPoint ServerEndPoint { get; protected set; }

        /// <summary>
        /// Gets or set message serializer
        /// </summary>
        public IMessageSerializer MessageSerializer { get; protected set; }

        /// <summary>
        /// Gets or set message reader
        /// </summary>
        public IMessageReader MessageReader { get; protected set; }

        /// <summary>
        /// Gets or set message writer
        /// </summary>
        public IMessageWriter MessageWriter { get; protected set; }

        /// <summary>
        /// Creates new instance of a session
        /// </summary>
        /// <param name="serverEndPoint">server to connect to</param>
        public Client(IPEndPoint serverEndPoint)
        {
            if (serverEndPoint == null)
            {
                throw new ArgumentNullException("serverEndPoint");
            }

            this.TcpClient = null;
            this.ServerEndPoint = serverEndPoint;
        }

        /// <summary>
        /// Creates new instance of a session
        /// </summary>
        /// <param name="tcpClient">tcp client ot be used</param>
        public Client(TcpClient tcpClient)
        {
            if (tcpClient == null)
            {
                throw new ArgumentNullException("tcpClient");
            }
            
            this.TcpClient = tcpClient;
        }

        /// <summary>
        /// Sends message and receives response
        /// </summary>
        /// <param name="message">message to be send</param>
        /// <returns>received messages</returns>
        public MessageCollection SendAndReceive(IMessage message)
        {
            this.Send(message);
            return this.Receive();
        }

        /// <summary>
        /// Sends message
        /// </summary>
        public void Send(IMessage message)
        {
            if(message != null)
            {
                this.MessageWriter.Write(message);
            }
        }

        /// <summary>
        /// Waits to receive incoming message
        /// </summary>
        /// <returns>incomming messages</returns>
        public MessageCollection Receive()
        {
            return this.MessageReader.Read();
        }

        /// <summary>
        /// Gets a value indicating whether is connected
        /// </summary>
        public bool IsConnected
        {
            get
            {
                try
                {
                    return this.TcpClient.Client.IsBound;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Connect
        /// </summary>
        public void Connect()
        {
            if (this.TcpClient == null)
            {
                this.TcpClient = new TcpClient();
                this.TcpClient.Connect(this.ServerEndPoint);
            }
            
            this.MessageSerializer = new MessageXmlSerializer();
            this.MessageReader = new MessageReader(this.TcpClient, this.MessageSerializer);
            this.MessageWriter = new MessageWriter(this.TcpClient, this.MessageSerializer);
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if(this.TcpClient != null)
            {
                this.TcpClient.Close();
            }
            
            this.TcpClient = null;
        }
    }
}
