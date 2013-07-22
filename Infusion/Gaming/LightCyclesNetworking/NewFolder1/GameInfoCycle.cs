using System.Collections.Generic;
using Infusion.Gaming.LightCycles;

namespace Infusion.Gaming.LightCyclesNetworking.NewFolder1
{
    /// <summary>
    /// Game info collection, allows to cycle through game info list
    /// </summary>
    public class GameInfoCycle : List<MapInfo>
    {
        /// <summary>
        /// Game info index
        /// </summary>
        private int gameInfoIndex;

        /// <summary>
        /// Gets current game from cycle
        /// </summary>
        public MapInfo Current 
        { 
            get
            {
                return this[gameInfoIndex];
            }
        }

        /// <summary>
        /// Cycle through game info collection
        /// </summary>
        /// <returns>next game info</returns>
        public void Cycle()
        {
            this.gameInfoIndex++;
            if (this.gameInfoIndex >= this.Count)
            {
                this.gameInfoIndex = 0;
            }
        }

        /// <summary>
        /// Try to remove at given index w/o throwing out of range exceptions
        /// </summary>
        /// <param name="index">index of element to remove</param>
        public bool TryRemoveAt(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                this.RemoveAt(index);
                return true;
            }

            return false;
        }

        
    }
}
