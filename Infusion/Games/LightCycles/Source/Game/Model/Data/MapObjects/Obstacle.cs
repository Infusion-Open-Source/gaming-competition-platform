﻿namespace Infusion.Gaming.LightCycles.Model.Data.MapObjects
{
    using System.Drawing;

    /// <summary>
    /// Class represents impassable obstacle on a map
    /// </summary>
    public class Obstacle : MapLocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Obstacle"/> class.
        /// </summary>
        /// <param name="location">obstacle location</param>
        public Obstacle(Point location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Gets a value indicating whether location is passable
        /// </summary>
        public override bool IsPassable
        {
            get
            {
                return false;
            }
        }
    }
}
