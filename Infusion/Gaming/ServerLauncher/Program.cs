using System.Net;
using Infusion.Gaming.LightCyclesNetworking;
using System;
using Infusion.Networking.ControllingServer;

namespace Infusion.Gaming.LightCycles.ServerLauncher
{
    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">program arguments</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("LightCycles - Copyright (C) 2013 Paweł Drozdowski");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");

            //try
            //{
                //if (args.Length == 1)
                //{
                    IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
                    GameServer server = new GameServer(new IPEndPoint(ipAddress, 10001), new ControllerServerServiceStub());
                    server.RunGame();
                //}
                //else
                //{
                //    Console.WriteLine("Invalid number of arguments, expected command line in following format:");
                //    Console.WriteLine("ServerLauncher.exe ControllingServerAddress");
                //    Console.WriteLine("example 1: ServerLauncher.exe 192.168.1.1:10010");
                //    Console.WriteLine("example 2: ServerLauncher.exe myServer.com");
                //}
            //}
            //catch(Exception e)
            //{
            //    Console.Write(e);
            //}
        }
    }
}