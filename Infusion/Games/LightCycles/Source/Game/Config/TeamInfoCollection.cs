namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Collection of teams information
    /// </summary>
    public class TeamInfoCollection : List<TeamInfo>
    {
        /// <summary>
        /// Find entry by name
        /// </summary>
        /// <param name="name">name to look for</param>
        /// <returns>found entry</returns>
        public TeamInfo FindByName(string name)
        {
            foreach (TeamInfo entry in this)
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
