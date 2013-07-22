using System;

namespace Infusion.Gaming.LightCycles.Networking.Game
{/*
    /// <summary>
    /// Game listener class
    /// </summary>
    public abstract class GameServerListener : HostBase
    {
        /// <summary>
        /// Gets or sets network listening client
        /// </summary>
        public Client NetListener { get; protected set; }
        
        /// <summary>
        /// Gets a value indicating whether is connected 
        /// </summary>
        public override bool IsConnected
        {
            get
            {
                return this.NetListener.IsConnected;
            }
        }
        
        /// <summary>
        /// Initialize processing
        /// </summary>
        protected override void Initialize()
        {
            var factory = new TcpConfiguartionFactory();
            this.NetListener = new Client(factory.CreateConfiguration());
            this.NetListener.Start();
        }

        /// <summary>
        /// Do the processing
        /// </summary>
        protected override void Process()
        {
            if (this.NetListener.Input.Count > 0)
            {
                IMessage messageIn = this.NetListener.Input.Dequeue();
                IMessage messageOut = this.Process(messageIn);
                if (messageOut != null)
                {
                    if (this.NetListener.Output.Count < 1000) // drop message if too many waiting in buffer
                    {
                        this.NetListener.Output.Enqueue(messageOut);
                    }
                    else
                    {
                        Console.WriteLine("Droping message");
                    }
                }
            }


        }

        /// <summary>
        /// Clean up processing
        /// </summary>
        protected override void CleanUp()
        {
            this.NetListener.Stop();
        }

        /// <summary>
        /// Process broadcast message and create command message
        /// </summary>
        /// <param name="messageIn">incoming broadcast message</param>
        /// <returns>resulting command message</returns>
        protected abstract IMessage Process(IMessage messageIn);
    }*/
}
