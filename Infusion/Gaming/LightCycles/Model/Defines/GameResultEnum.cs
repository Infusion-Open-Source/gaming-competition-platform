namespace Infusion.Gaming.LightCycles.Model.Defines
{
    /// <summary>
    /// The game result enumeration.
    /// </summary>
    public enum GameResultEnum
    {
        /// <summary>
        /// Undefined result.
        /// </summary>
        Undefined = 0, 

        /// <summary>
        /// Game is still running.
        /// </summary>
        StillRunning, 

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
        /// Game has been terminated.
        /// </summary>
        Terminated
    }
}