using System;
using System.Threading;

namespace Infusion.Gaming.LightCyclesNetworking.NewFolder1
{
    /// <summary>
    /// Abstraction of message processing host
    /// </summary>
    public abstract class HostBase : IDisposable
    {
        /// <summary>
        /// Internal sync root
        /// </summary>
        private readonly object syncRoot = new object();

        /// <summary>
        /// Internal sync signal
        /// </summary>
        private readonly ManualResetEvent syncSignal = new ManualResetEvent(false);

        /// <summary>
        /// Is host in a middle of processing
        /// </summary>
        private bool isProcessing;
        
        /// <summary>
        /// Internal thread
        /// </summary>
        private Thread processingThread;
        
        /// <summary>
        /// Gets or sets a value indicating whether processor is processing
        /// </summary>
        public bool IsProcessing
        {
            get
            {
                return this.isProcessing;
            }

            set
            {
                lock (this.syncRoot)
                {
                    this.isProcessing = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether processor is connected 
        /// </summary>
        public abstract bool IsConnected { get; }

        /// <summary>
        /// Starts up all game server services
        /// </summary>
        public void Start()
        {
            if (!this.isProcessing)
            {
                this.processingThread = new Thread(this.MessageProcessingThread);
                this.processingThread.Start();
                this.syncSignal.WaitOne();
            }
        }

        /// <summary>
        /// Shuts down all server services
        /// </summary>
        public void Stop()
        {
            if (this.isProcessing)
            {
                this.processingThread.Join();
                this.syncSignal.WaitOne();
            }
        }

        /// <summary>
        /// Dispose resources
        /// </summary>
        public void Dispose()
        {
            this.syncSignal.Dispose();
        }

        /// <summary>
        /// Message processing thread
        /// </summary>
        protected void MessageProcessingThread()
        {
            try
            {
                this.Initialize();
                this.IsProcessing = true;
                this.syncSignal.Set();
                while (this.IsConnected)
                {
                    this.Process();
                    Thread.Sleep(0);
                }
            }
            finally
            {
                this.CleanUp();
                this.IsProcessing = false;
                this.syncSignal.Set();
            }
        }
        
        /// <summary>
        /// Initialize processing
        /// </summary>
        protected abstract void Initialize();
        
        /// <summary>
        /// Do the processing
        /// </summary>
        protected abstract void Process();

        /// <summary>
        /// Clean up processing
        /// </summary>
        protected abstract void CleanUp();
    }
}
