using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infusion.Networking
{
    /// <summary>
    /// Collection of networking sessions
    /// </summary>
    public class ClientCollection : IEnumerable<Client>
    {
        protected object syncRoot = new object();
        protected List<Client> sessions = new List<Client>();

        public ClientCollection()
        {
        }

        public ClientCollection(ClientCollection collection)
        {
            this.AddRange(collection);
        }

        public int Count
        {
            get
            {
                int result;
                lock (this.syncRoot)
                {
                    result = this.sessions.Count;
                }

                return result;
            }
        }

        /// <summary>
        /// Stop all sessions
        /// </summary>
        public void DisconnectAll()
        {
            foreach (Client session in this.sessions)
            {
                session.Disconnect();
            }
        }

        public void Add(Client session)
        {
            lock (this.syncRoot)
            {
                this.sessions.Add(session);
            }
        }

        public void AddRange(ClientCollection collection)
        {
            lock(this.syncRoot)
            {
                lock (collection.syncRoot)
                {
                    foreach (Client session in collection.sessions)
                    {
                        this.sessions.Add(session);
                    }
                }
            }
        }

        public ClientCollection Clone()
        {
            ClientCollection result;
            lock (this.syncRoot)
            {
                result = new ClientCollection(this);
            }

            return result;
        }

        public IEnumerator<Client> GetEnumerator()
        {
            return this.sessions.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.sessions.GetEnumerator();
        }

        public void CleanUp()
        {
            lock (this.syncRoot)
            {
                for(int i=0; i<this.sessions.Count; i++)
                {
                    if (sessions[i] == null)
                    {
                        sessions.RemoveAt(i--);
                        continue;
                    }

                    if (!sessions[i].IsConnected)
                    {
                        sessions[i].Disconnect();
                        sessions.RemoveAt(i--);
                    }
                }
            }
        }
    }
}
