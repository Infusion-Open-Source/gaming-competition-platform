using System;

namespace Infusion.Gaming.LightCycles.Networking.Game
{/*
    /// <summary>
    /// Base game server class
    /// Hosts following services:
    /// - one way game state broadcasting server
    /// - two way sockets for communication with multiple clients
    /// - two way socket for communication with remote controlling app
    /// </summary>
    public abstract class GameServerBase : HostBase
    {
        /// <summary>
        /// Gets or sets networking server
        /// </summary>
        public Server NetServer { get; protected set; }
        
        /// <summary>
        /// Gets a value indicating whether processor is connected 
        /// </summary>
        public override bool IsConnected
        {
            get
            {
                return this.NetServer.IsConnected;
            }
        }
        
        /// <summary>
        /// Initialize processing
        /// </summary>
        protected override void Initialize()
        {
            var factory = new TcpConfiguartionFactory();
            this.NetServer = new Server(factory.CreateConfiguration());
            this.NetServer.Start();
        }

        /// <summary>
        /// Do the processing
        /// </summary>
        protected override void Process()
        {
            this.NetServer.Sessions.CleanUp();
            ClientCollection clientConnections = this.NetServer.Sessions.Clone();

            // process remoting commands
            foreach (Client client in clientConnections)
            {
                if (client.Input.Count > 0)
                {
                    IMessage output = this.ProcessMessage(client.Input.Dequeue(), client);
                    if(output != null)
                    {
                        if (client.Output.Count < 1000) // drop message if too many waiting in buffer
                        {
                            client.Output.Enqueue(output);
                        }
                        else
                        {
                            Console.WriteLine("Droping message");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clean up processing
        /// </summary>
        protected override void CleanUp()
        {
            this.NetServer.Stop();
        }

        /// <summary>
        /// Process incomming message and create response
        /// </summary>
        /// <param name="messageIn">remote control message</param>
        /// <param name="netClient">endPoint sending request</param>
        protected abstract IMessage ProcessMessage(IMessage messageIn, Client netClient);
    }*/
}
