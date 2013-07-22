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
        /// Game is still running.
        /// </summary>
        Running, 

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
        FinishedWithoutWinner,

        /// <summary>
        /// Geme was forced to terminate
        /// </summary>
        Terminated
    }
}
