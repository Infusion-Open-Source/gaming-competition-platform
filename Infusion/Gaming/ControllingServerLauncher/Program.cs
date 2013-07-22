using System;
using System.Threading;

namespace Infusion.Networking.ControllingServer
{
    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Program main method.
        /// </summary>
        /// <param name="args">
        /// Program arguments.
        /// </param>
        private static void Main(string[] args)
        {
            Console.WriteLine("LightCycles - Copyright (C) 2013 Paweł Drozdowski");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");

            try
            {/*
                ControllingServer server = new ControllingServer();
                server.Start();
                while (server.IsProcessing)
                {
                    Thread.Sleep(0);
                }

                server.Stop();*/
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
