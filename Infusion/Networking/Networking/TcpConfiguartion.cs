namespace Infusion.Networking
{
    using System.Net.Sockets;

    /// <summary>
    /// Networking socket configuration
    /// </summary>
    public class TcpConfiguartion
    {
        public TcpConfiguartion(string serverName, int port, bool read, bool write)
        {
            this.ServerName = serverName;
            this.Port = port;
            this.CanRead = read;
            this.CanWrite = write;
            this.MaxPacketSize = 250000;
        }

        /// <summary>
        /// Server host name    
        /// </summary>
        public string ServerName { get; protected set; }

        /// <summary>
        /// Broadcasting port number
        /// </summary>
        public int Port { get; protected set; }
        
        /// <summary>
        /// Can read from socket
        /// </summary>
        public bool CanRead { get; protected set; }

        /// <summary>
        /// Can write to socket
        /// </summary>
        public bool CanWrite { get; protected set; }

        /// <summary>
        /// Gets or set connection tag
        /// </summary>
        public string Tag { get; protected set; }

        /// <summary>
        /// Max packet size
        /// </summary>
        public int MaxPacketSize { get; protected set; }
    }
}
