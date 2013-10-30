namespace Infusion.Gaming.LightCycles.Events
{
    /// <summary>
    /// Player score source enumerator
    /// </summary>
    public enum PlayerScoreSource
    {
        /// <summary>
        /// Score is from clean move, w/o collision
        /// </summary>
        CleanMoveScore,

        /// <summary>
        /// Score is from enemy hitting players trial
        /// </summary>
        TrailHitScore,

        /// <summary>
        /// Score is from player reaching end of game
        /// </summary>
        LastManStandScore
    }
}
