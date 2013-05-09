namespace ConsoleClient
{
    using System;
    using System.Net;
    using Infusion.Gaming.LightCyclesCommon.Networking;
    
    /// <summary>
    /// Console client example application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">program arguments</param>
        public static void Main(string[] args)
        {
            var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);
            var listener = new BroadcastListener(endpoint);
            Console.WriteLine("Listening on " + endpoint + ".");

            // play 3 games and end execution
            for (int i = 0; i < 3; i++)
            {
                var player = new Player(listener);
                player.Play();
            }

            Console.WriteLine("Cleaning up.");
            listener.Close();
        }
    }
}
