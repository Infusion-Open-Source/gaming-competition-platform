namespace Infusion.Gaming.LightCyclesCommon.Networking
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// Listener of UDP broadcasts
    /// </summary>
    public class BroadcastListener
    {
        /// <summary>
        /// Remote endpoint member
        /// </summary>
        private IPEndPoint remoteEndPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastListener" /> class.
        /// </summary>
        /// <param name="remoteEndPoint">remote endpoint to use</param>
        public BroadcastListener(IPEndPoint remoteEndPoint)
        {
            if (remoteEndPoint == null)
            {
                throw new ArgumentNullException("remoteEndPoint");
            }

            this.remoteEndPoint = remoteEndPoint;
            this.Client = new UdpClient();
            this.Client.ExclusiveAddressUse = false;
            this.Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.Client.Client.Bind(this.remoteEndPoint);
        }
        
        /// <summary>
        /// Gets or sets endpoint
        /// </summary>
        public UdpClient Client { get; protected set; }

        /// <summary>
        /// Receive UDP message
        /// </summary>
        /// <returns>message received</returns>
        public string Receive()
        {
            byte[] buffer = this.Client.Receive(ref this.remoteEndPoint);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Close connection
        /// </summary>
        public void Close()
        {
            this.Client.Close();
        }
    }
}
