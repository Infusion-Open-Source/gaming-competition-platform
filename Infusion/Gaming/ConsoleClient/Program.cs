using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);
            var client = new UdpClient();
            client.ExclusiveAddressUse = false;
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.Client.Bind(endpoint);

            IPEndPoint inEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Listening on " + endpoint + ".");

            while (true)
            {
                byte[] buffer = client.Receive(ref inEndPoint);
                Console.Clear();
                Console.Write(Encoding.ASCII.GetString(buffer));
            }
        }
    }
}
