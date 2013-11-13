namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// Player controller class
    /// </summary>
    public class PlayerController : IDisposable
    {
        /// <summary>
        /// Is object disposed
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// Synchronization root object
        /// </summary>
        private object syncRoot = new object();

        /// <summary>
        /// Players processes
        /// </summary>
        private Dictionary<Identity, Process> playerProcesses = new Dictionary<Identity, Process>();

        /// <summary>
        /// Players inputs buffers
        /// </summary>
        private Dictionary<Identity, Queue<string>> playerInputs = new Dictionary<Identity, Queue<string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerController" /> class.
        /// </summary>
        /// <param name="playerIdentities">players identity</param>
        /// <param name="playerExeFiles">teams identity</param>
        public PlayerController(List<Identity> playerIdentities, Dictionary<Identity, string> playerExeFiles)
        {
            foreach (Identity identity in playerIdentities)
            {
                string exeFile = playerExeFiles[identity];
                Process process = new Process();
                process.StartInfo.FileName = exeFile;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                process.Start();

                this.playerProcesses.Add(identity, process);
                this.playerInputs.Add(identity, new Queue<string>());

                Thread thread = new Thread(this.ProcessReader);
                thread.Start(new object[] { identity, process });
            }
        }

        /// <summary>
        /// Gets process for given player
        /// </summary>
        /// <param name="playerId">player identity</param>
        /// <returns>players process</returns>
        public Process GetProcess(Identity playerId)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("StreamsController");
            }
            
            return this.playerProcesses[playerId];
        }
        
        /// <summary>
        /// Clears all players buffers
        /// </summary>
        public void Clear()
        {
            lock (this.syncRoot)
            {
                // do quick clean up, minimize time under lock
                foreach (Queue<string> queue in this.playerInputs.Values)
                {
                    queue.Clear();
                }
            }
        }

        /// <summary>
        /// Reads in all messages from players buffers at once, minimizes locks
        /// </summary>
        /// <param name="playerIdentities">players identities for which to do the read</param>
        /// <returns>read data</returns>
        public Dictionary<Identity, Queue<string>> ReadAllMessages(IEnumerable<Identity> playerIdentities)
        {
            Dictionary<Identity, Queue<string>> newPlayerInputs = new Dictionary<Identity, Queue<string>>();
            foreach (Identity identity in playerIdentities)
            {
                newPlayerInputs.Add(identity, new Queue<string>());
            }

            Dictionary<Identity, Queue<string>> oldPlayerInputs;
            lock (this.syncRoot)
            {
                // swap the buffers, minimize time under lock
                oldPlayerInputs = this.playerInputs;
                this.playerInputs = newPlayerInputs;
            }

            return oldPlayerInputs;
        }
        
        /// <summary>
        /// Close all processes
        /// </summary>
        public void Close()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("StreamsController");
            }

            foreach (Process process in this.playerProcesses.Values)
            {
                try
                {
                    process.Kill();
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Do the dispose
        /// </summary>
        public void Dispose()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("StreamsController");
            }

            foreach (Process process in this.playerProcesses.Values)
            {
                process.Dispose();
            }

            this.isDisposed = true;
        }
        
        /// <summary>
        /// Process reading routine
        /// </summary>
        /// <param name="arg">routine arguments</param>
        private void ProcessReader(object arg)
        {
            object[] args = (object[])arg;
            Identity identity = (Identity)args[0];
            Process process = (Process)args[1];
            try
            {
                while (true)
                {
                    string line = process.StandardOutput.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        lock (this.syncRoot)
                        {
                            this.playerInputs[identity].Enqueue(line);
                        }
                    }

                    Thread.Sleep(0);
                }
            }
            catch (InvalidOperationException)
            {
                // Console.WriteLine("Process " + identity + " is down.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Process " + identity + " has failed: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
