using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Infusion.Networking
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// TCP Connection
    /// </summary>
    public abstract class TcpConnection : IDisposable
    {
        /// <summary>
        /// Internal sync signal
        /// </summary>
        private readonly ManualResetEvent syncSignal = new ManualResetEvent(false);

        /// <summary>
        /// Internal thread
        /// </summary>
        protected Thread connectionThread;
        
        /// <summary>
        /// Gets or sets TCP configuartion
        /// </summary>
        public TcpConfiguartion TcpConfiguartion { get; protected set; }

        /// <summary>
        /// Gets client Tag
        /// </summary>
        public string Tag
        {
            get
            {
                return this.TcpConfiguartion.Tag + " (" + this.LocalEndPointName + " - " + this.RemoteEndPoint + ")";
            }
        }

        /// <summary>
        /// Gets name of local endpoint
        /// </summary>
        public abstract string LocalEndPointName { get; }

        /// <summary>
        /// Gets name of remote endpoint
        /// </summary>
        public abstract string RemoteEndPoint { get; }

        /// <summary>
        /// Gets a value indicating whether is connected
        /// </summary>
        public abstract bool IsConnected { get; }

        /// <summary>
        /// Creates new instance of a session
        /// </summary>
        /// <param name="tcpConfiguartion">tcp configuration</param>
        protected TcpConnection(TcpConfiguartion tcpConfiguartion)
        {
            if (tcpConfiguartion == null)
            {
                throw new ArgumentNullException("tcpConfiguartion");
            }

            this.TcpConfiguartion = tcpConfiguartion;
        }

        /// <summary>
        /// Start session
        /// </summary>
        public void Start()
        {
            Console.WriteLine(this.TcpConfiguartion.Tag + " starting");
            this.connectionThread = new Thread(SessionThread);
            this.connectionThread.Start();
            syncSignal.WaitOne();
        }

        /// <summary>
        /// Stops session
        /// </summary>
        public void Stop()
        {
            Console.WriteLine(this.TcpConfiguartion.Tag + " stopping");
            if (this.IsConnected)
            {
                this.Disconnect();
                this.syncSignal.WaitOne();
            }
        }
        
        /// <summary>
        /// Session thread
        /// </summary>
        protected void SessionThread()
        {
            try
            {
                this.Initialize();

                // release sync wait on start 
                syncSignal.Set();
                while (this.IsConnected)
                {
                    if(!this.Process())
                    {
                        break;
                    }

                    Thread.Sleep(0);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(this.TcpConfiguartion.Tag + " error: " + e.Message);
            }
            finally
            {
                this.CleanUp();

                // release sync wait on stop
                this.syncSignal.Set();
            }
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        protected abstract void Disconnect();

        /// <summary>
        /// Scote initialization
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Scote initialization
        /// </summary>
        protected abstract bool Process();

        /// <summary>
        /// Release the socket.
        /// </summary>
        protected abstract void CleanUp();

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.syncSignal.Dispose();
        }
    }
}
