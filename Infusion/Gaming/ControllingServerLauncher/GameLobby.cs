using System;
using System.Collections.Generic;

namespace Infusion.Gaming.LightCycles.Networking.Controlling
{/*
    /// <summary>
    /// Manages players in and outside game
    /// </summary>
    public class GameLobby : List<PlayerInfo>
    {
        /// <summary>
        /// Remove element by unique id
        /// </summary>
        /// <param name="uniqueId">unique id used for match up</param>
        public void RemoveByUniqueId(string uniqueId)
        {
            int index = this.Find(uniqueId);
            if (index != -1)
            {
                this.RemoveAt(index);
            }
        }

        /// <summary>
        /// Move up in lobby queue
        /// </summary>
        /// <param name="uniqueId">unique id of player</param>
        public void MoveUp(string uniqueId)
        {
            int index = this.Find(uniqueId);
            if (index > 0)
            {
                var tmp = this[index];
                this[index] = this[index - 1];
                this[index - 1] = tmp;
            }
        }

        /// <summary>
        /// Move down in lobby queu
        /// </summary>
        /// <param name="uniqueId">unique id of player</param>
        public void MoveDown(string uniqueId)
        {
            int index = this.Find(uniqueId);
            if (index < this.Count - 2)
            {
                var tmp = this[index];
                this[index] = this[index + 1];
                this[index + 1] = tmp;
            }
        }

        /// <summary>
        /// Find index by unique id
        /// </summary>
        /// <param name="uniqueId">unique id of player</param>
        /// <returns>player index or -1</returns>
        public int Find(string uniqueId)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (string.Equals(this[i].Key, uniqueId, StringComparison.InvariantCulture))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Checks whether looby has player
        /// </summary>
        /// <param name="uniqueId">unique id of player</param>
        /// <returns>whether looby has player</returns>
        public bool HasPlayer(string uniqueId)
        {
            return this.Find(uniqueId) != -1;
        }

        /// <summary>
        /// Gets player info structuser using unique id
        /// </summary>
        /// <param name="uniqueId">unique id of player</param>
        /// <returns>Player info matching unique id</returns>
        public PlayerInfo GetPlayer(string uniqueId)
        {
            if (this.HasPlayer(uniqueId))
            {
                return this[Find(uniqueId)];
            }

            return null;
        }
    }*/
}
