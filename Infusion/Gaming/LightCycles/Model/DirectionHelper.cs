﻿namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The direction helper.
    /// </summary>
    public class DirectionHelper
    {
        #region Static Fields

        /// <summary>
        /// The mappings.
        /// </summary>
        private static readonly List<DirectionHelper> Mappings = new List<DirectionHelper>
                                                           {
                                                               new DirectionHelper(
                                                                   DirectionEnum.Left, 
                                                                   RelativeDirectionEnum.Left, 
                                                                   DirectionEnum.Down), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Left, 
                                                                   RelativeDirectionEnum.Right, 
                                                                   DirectionEnum.Up), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Left, 
                                                                   RelativeDirectionEnum.StraightForward, 
                                                                   DirectionEnum.Left), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Left, 
                                                                   RelativeDirectionEnum.Undefined, 
                                                                   DirectionEnum.Left), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Right, 
                                                                   RelativeDirectionEnum.Left, 
                                                                   DirectionEnum.Up), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Right, 
                                                                   RelativeDirectionEnum.Right, 
                                                                   DirectionEnum.Down), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Right, 
                                                                   RelativeDirectionEnum.StraightForward, 
                                                                   DirectionEnum.Right), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Right, 
                                                                   RelativeDirectionEnum.Undefined, 
                                                                   DirectionEnum.Right), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Up, 
                                                                   RelativeDirectionEnum.Left, 
                                                                   DirectionEnum.Left), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Up, 
                                                                   RelativeDirectionEnum.Right, 
                                                                   DirectionEnum.Right), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Up, 
                                                                   RelativeDirectionEnum.StraightForward, 
                                                                   DirectionEnum.Up), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Up, 
                                                                   RelativeDirectionEnum.Undefined, 
                                                                   DirectionEnum.Up), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Down, 
                                                                   RelativeDirectionEnum.Left, 
                                                                   DirectionEnum.Right), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Down, 
                                                                   RelativeDirectionEnum.Right, 
                                                                   DirectionEnum.Left), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Down, 
                                                                   RelativeDirectionEnum.StraightForward, 
                                                                   DirectionEnum.Down), 
                                                               new DirectionHelper(
                                                                   DirectionEnum.Down, 
                                                                   RelativeDirectionEnum.Undefined, 
                                                                   DirectionEnum.Down), 
                                                           };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionHelper"/> class.
        /// </summary>
        /// <param name="direction">
        /// The current direction.
        /// </param>
        /// <param name="relativeDirection">
        /// The relative direction to apply.
        /// </param>
        /// <param name="newDirection">
        /// The resulting new direction.
        /// </param>
        public DirectionHelper(
            DirectionEnum direction, RelativeDirectionEnum relativeDirection, DirectionEnum newDirection)
        {
            this.Direction = direction;
            this.RelativeDirection = relativeDirection;
            this.NewDirection = newDirection;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the current direction.
        /// </summary>
        public DirectionEnum Direction { get; protected set; }

        /// <summary>
        /// Gets or sets the new direction.
        /// </summary>
        public DirectionEnum NewDirection { get; protected set; }

        /// <summary>
        /// Gets or sets the relative direction to apply.
        /// </summary>
        public RelativeDirectionEnum RelativeDirection { get; protected set; }

        /// <summary>
        /// Gets or sets the shift on x.
        /// </summary>
        public int ShiftOnX { get; protected set; }

        /// <summary>
        /// Gets or sets the shift on y.
        /// </summary>
        public int ShiftOnY { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Check the direction by analyzing current and previous location.
        /// </summary>
        /// <param name="currentX">
        /// The current x.
        /// </param>
        /// <param name="currentY">
        /// The current y.
        /// </param>
        /// <param name="prevX">
        /// The previous x.
        /// </param>
        /// <param name="prevY">
        /// The previous y.
        /// </param>
        /// <returns>
        /// The direction.
        /// </returns>
        public static DirectionEnum CheckDirection(int currentX, int currentY, int prevX, int prevY)
        {
            if (currentX > prevX && currentY == prevY)
            {
                return DirectionEnum.Right;
            }

            if (currentX < prevX && currentY == prevY)
            {
                return DirectionEnum.Left;
            }

            if (currentX == prevX && currentY > prevY)
            {
                return DirectionEnum.Down;
            }

            if (currentX == prevX && currentY < prevY)
            {
                return DirectionEnum.Up;
            }

            return DirectionEnum.Undefined;
        }

        /// <summary>
        /// Check the direction by analyzing current and previous location.
        /// </summary>
        /// <param name="currentPoint">
        /// The current point.
        /// </param>
        /// <param name="prevPoint">
        /// The previous point.
        /// </param>
        /// <returns>
        /// The direction.
        /// </returns>
        public static DirectionEnum CheckDirection(Point currentPoint, Point prevPoint)
        {
            return CheckDirection(currentPoint.X, currentPoint.Y, prevPoint.X, prevPoint.Y);
        }

        /// <summary>
        /// Changes current direction.
        /// </summary>
        /// <param name="direction">
        /// The current direction.
        /// </param>
        /// <param name="relativeDirection">
        /// The relative direction to apply.
        /// </param>
        /// <returns>
        /// The resulting direction
        /// </returns>
        public static DirectionEnum ChangeDirection(DirectionEnum direction, RelativeDirectionEnum relativeDirection)
        {
            foreach (DirectionHelper mapping in Mappings)
            {
                if (mapping.Direction == direction && mapping.RelativeDirection == relativeDirection)
                {
                    return mapping.NewDirection;
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        #endregion
    }
}