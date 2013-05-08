namespace Infusion.Gaming.LightCycles
{
    using System.Collections.Generic;

    /// <summary>
    /// Game info collection, allows to cycle through game info list
    /// </summary>
    public class GameInfoCollection : List<GameInfo>
    {
        /// <summary>
        /// Next game info index
        /// </summary>
        private int nextGameInfoIndex;

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

            GameInfo result = this[this.nextGameInfoIndex++];
            if (this.nextGameInfoIndex >= this.Count)
            {
                this.nextGameInfoIndex = 0;
            }

            return result;
        }
    }
}
