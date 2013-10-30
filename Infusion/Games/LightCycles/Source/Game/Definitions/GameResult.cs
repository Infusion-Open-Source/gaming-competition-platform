namespace Infusion.Gaming.LightCycles.Definitions
{
    /// <summary>
    /// Game result enumerable
    /// </summary>
    public enum GameResult
    {
        /// <summary>
        /// Undefined result.
        /// </summary>
        Undefined = 0, 
        
        /// <summary>
        /// Game finished with a winner.
        /// </summary>
        FinshedWithWinner,

        /// <summary>
        /// Game finished with one or more winners (for team play).
        /// </summary>
        FinshedWithWinners, 

        /// <summary>
        /// Game finished without a winner.
        /// </summary>
        FinishedWithoutWinner
    }
}
