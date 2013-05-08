namespace Infusion.Gaming.LightCycles.Messaging
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// Game state broadcaster via UDP
    /// </summary>
    public class GameStateBroadcaster : IMessageSink
    {
        /// <summary>
        /// Flush message into a sink
        /// </summary>
        /// <param name="message">message to be flushed</param>
        public void Flush(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var server = new UdpClient();
                var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);
                server.ExclusiveAddressUse = false;
                server.DontFragment = true;
                server.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                server.Client.Bind(endpoint);
                var bytes = Encoding.ASCII.GetBytes(message);
                server.Send(bytes, bytes.Length, endpoint);
            }
        }
    }
}
