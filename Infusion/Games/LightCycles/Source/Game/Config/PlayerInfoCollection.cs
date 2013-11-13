namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Collection of players information
    /// </summary>
    public class PlayerInfoCollection : List<PlayerInfo>
    {
        /// <summary>
        /// Find entry by name
        /// </summary>
        /// <param name="name">name to look for</param>
        /// <returns>found entry</returns>
        public PlayerInfo FindByName(string name)
        {
            foreach (PlayerInfo entry in this)
            {
                if (entry.Name.Equals(name))
                {
                    return entry;
                }
            }

            return null;
        }
    }
}
