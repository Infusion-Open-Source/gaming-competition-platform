namespace Infusion.Gaming.LightCycles
{
    using System.Collections.Generic;

    /// <summary>
    /// Game info collection, allows to cycle through game info list
    /// </summary>
    public class GameInfoCollection : List<GameInfo>
    {
        /// <summary>
        /// Current game info index
        /// </summary>
        private int currentGameInfoIndex;

        /// <summary>
        /// Cycle through game info collection
        /// </summary>
        /// <returns>next game info</returns>
        public GameInfo Cycle()
        {
            if (this.Count == 0)
            {
                return null;
            }

            if (this.currentGameInfoIndex + 1 >= this.Count)
            {
                this.currentGameInfoIndex = 0;
            }

            return this[this.currentGameInfoIndex++];
        }
    }
}
