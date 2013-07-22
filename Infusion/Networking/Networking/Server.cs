namespace Infusion.Networking
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// multi-client server basic abstraction class
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Connected client sessions
        /// </summary>
        public ClientCollection Sessions { get; protected set; }

        /// <summary>
        /// TCP Listener
        /// </summary>
        public TcpListener TcpListener { get; protected set; }

        /// <summary>
        /// Gets or sets local endpoint
        /// </summary>
        public IPEndPoint LocalEndPoint { get; protected set; }

        /// <summary>
        /// Gets name of local endpoint
        /// </summary>
        public string LocalEndPointName
        {
            get
            {
                try
                {
                    return this.TcpListener.Server.LocalEndPoint.ToString();
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
                    return this.TcpListener.Server.RemoteEndPoint.ToString();
                }
                catch (Exception)
                {
                    return "?";
                }
            }
        }

        /// <summary>
        /// Creates new instance of server
        /// </summary>
        /// <param name="localEndPoint">server local end point</param>
        public Server(IPEndPoint localEndPoint)
        {
            if (localEndPoint == null)
            {
                throw new ArgumentNullException("localEndPoint");
            }

            this.LocalEndPoint = localEndPoint;
            this.Sessions = new ClientCollection();
        }
        
        /// <summary>
        /// Listen and accept incomming connections
        /// </summary>
        public Client AcceptIncomingConnection()
        {
            TcpClient tcpClient = this.TcpListener.AcceptTcpClient();
            Client session = new Client(tcpClient);
            this.Sessions.Add(session);
            if (!session.IsConnected)
            {
                session.Connect();
            }
            
            Console.WriteLine("new client session from " + session.RemoteEndPointName);
            return session;
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
                    return this.TcpListener.Server.IsBound;
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
            if (this.TcpListener == null)
            {
                this.TcpListener = new TcpListener(this.LocalEndPoint);
            }

            this.TcpListener.Start();
            Console.WriteLine("accepting connections");
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (this.TcpListener != null)
            {
                this.TcpListener.Stop();
            }

            this.TcpListener = null;
        }
    }
}
