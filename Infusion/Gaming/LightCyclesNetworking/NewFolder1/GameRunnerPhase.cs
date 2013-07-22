namespace Infusion.Gaming.LightCyclesNetworking.NewFolder1
{
    /// <summary>
    /// Phases of game runner
    /// </summary>
    public enum GameRunnerPhase
    {
        /// <summary>
        /// Bot identified value
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Lobby for player gathering
        /// </summary>
        Lobby,

        /// <summary>
        /// Game is pending to start
        /// </summary>
        StartPending,

        /// <summary>
        /// Game is starting
        /// </summary>
        Starting,

        /// <summary>
        /// Game is running
        /// </summary>
        Playing,

        /// <summary>
        /// Game has ended
        /// </summary>
        End
    }
}
