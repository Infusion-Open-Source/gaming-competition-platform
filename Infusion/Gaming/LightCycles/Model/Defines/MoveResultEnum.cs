namespace Infusion.Gaming.LightCycles.Model.Defines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Different types of move action result
    /// </summary>
    public enum MoveResultEnum
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
