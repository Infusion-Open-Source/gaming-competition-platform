using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    public class GameStateBroadcaster : IGameStateSink
    {
        public void Flush(IGameState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("gameState");
            }

            var writer = new StringWriter();
            var serializer = new StringGameStateSerializer(writer);
            serializer.Write(state);

            var snapshot = writer.GetStringBuilder().ToString();


            var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);
            var server = new UdpClient();
            server.ExclusiveAddressUse = false;
            server.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            server.Client.Bind(endpoint);
            var bytes = Encoding.ASCII.GetBytes(snapshot);
            server.Send(bytes, bytes.Length, endpoint);

        }
    }
}
