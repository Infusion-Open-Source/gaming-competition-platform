namespace Infusion.Gaming.LightCycles.Definitions
{
    /// <summary>
    /// Different types of move action result
    /// </summary>
    public enum MoveResult
    {
        /// <summary>
        /// Result id undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Move was successful
        /// </summary>
        Successful,

        /// <summary>
        /// Move ended with collision with obstacle
        /// </summary>
        CollisionWithObstacle,

        /// <summary>
        /// Move ended with collision with light trail
        /// </summary>
        CollisionWithTrail,

        /// <summary>
        /// Move ended with collision with other player
        /// </summary>
        CollisionWithPlayer,
    }
}
