namespace Infusion.Gaming.LightCycles.Util
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Definitions;

    /// <summary>
    /// The direction helper.
    /// </summary>
    public class DirectionHelper
    {
        /// <summary>
        /// Internal random number generator
        /// </summary>
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// The mappings.
        /// </summary>
        private static readonly List<DirectionHelper> Mappings = new List<DirectionHelper>
                                                                     {
                                                                         new DirectionHelper(Direction.Left, RelativeDirection.Left, Direction.Down), 
                                                                         new DirectionHelper(Direction.Left, RelativeDirection.Right, Direction.Up), 
                                                                         new DirectionHelper(Direction.Left, RelativeDirection.StraightAhead, Direction.Left), 
                                                                         new DirectionHelper(Direction.Left, RelativeDirection.Undefined, Direction.Left), 
                                                                         new DirectionHelper(Direction.Right, RelativeDirection.Left, Direction.Up), 
                                                                         new DirectionHelper(Direction.Right, RelativeDirection.Right, Direction.Down), 
                                                                         new DirectionHelper(Direction.Right, RelativeDirection.StraightAhead, Direction.Right), 
                                                                         new DirectionHelper(Direction.Right, RelativeDirection.Undefined, Direction.Right), 
                                                                         new DirectionHelper(Direction.Up, RelativeDirection.Left, Direction.Left), 
                                                                         new DirectionHelper(Direction.Up, RelativeDirection.Right, Direction.Right), 
                                                                         new DirectionHelper(Direction.Up, RelativeDirection.StraightAhead, Direction.Up), 
                                                                         new DirectionHelper(Direction.Up, RelativeDirection.Undefined, Direction.Up), 
                                                                         new DirectionHelper(Direction.Down, RelativeDirection.Left, Direction.Right), 
                                                                         new DirectionHelper(Direction.Down, RelativeDirection.Right, Direction.Left), 
                                                                         new DirectionHelper(Direction.Down, RelativeDirection.StraightAhead, Direction.Down), 
                                                                         new DirectionHelper(Direction.Down, RelativeDirection.Undefined, Direction.Down) 
                                                                     };

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
            Direction direction, RelativeDirection relativeDirection, Direction newDirection)
        {
            this.Direction = direction;
            this.RelativeDirection = relativeDirection;
            this.NewDirection = newDirection;
        }

        /// <summary>
        /// Gets or sets the current direction.
        /// </summary>
        public Direction Direction { get; protected set; }

        /// <summary>
        /// Gets or sets the new direction.
        /// </summary>
        public Direction NewDirection { get; protected set; }

        /// <summary>
        /// Gets or sets the relative direction to apply.
        /// </summary>
        public RelativeDirection RelativeDirection { get; protected set; }

        /// <summary>
        /// Gets or sets the shift on x.
        /// </summary>
        public int ShiftOnX { get; protected set; }

        /// <summary>
        /// Gets or sets the shift on y.
        /// </summary>
        public int ShiftOnY { get; protected set; }

        /// <summary>
        /// Gets random direction
        /// </summary>
        /// <returns>Random direction.</returns>
        public static Direction RandomDirection()
        {
            switch (Random.Next(4))
            {
                case 0: 
                    return Direction.Up;
                case 1:
                    return Direction.Down;
                case 2:
                    return Direction.Left;
                case 3:
                    return Direction.Right;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        public static Direction ChangeDirection(Direction direction, RelativeDirection relativeDirection)
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
        public static Direction CheckDirection(int currentX, int currentY, int prevX, int prevY)
        {
            if (currentX > prevX && currentY == prevY)
            {
                return Direction.Right;
            }

            if (currentX < prevX && currentY == prevY)
            {
                return Direction.Left;
            }

            if (currentX == prevX && currentY > prevY)
            {
                return Direction.Down;
            }

            if (currentX == prevX && currentY < prevY)
            {
                return Direction.Up;
            }

            return Direction.Undefined;
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
        public static Direction CheckDirection(Point currentPoint, Point prevPoint)
        {
            return CheckDirection(currentPoint.X, currentPoint.Y, prevPoint.X, prevPoint.Y);
        }

        /// <summary>
        /// Gets coordinates of next location after applying relative move
        /// </summary>
        /// <param name="location">
        /// current location
        /// </param>
        /// <param name="direction">
        /// current global direction of move
        /// </param>
        /// <returns>
        /// new location
        /// </returns>
        public static Point NextLocation(Point location, Direction direction)
        {
            var newLocation = new Point(location.X, location.Y);
            switch (direction)
            {
                case Direction.Up:
                    newLocation.Y--;
                    break;
                case Direction.Down:
                    newLocation.Y++;
                    break;
                case Direction.Left:
                    newLocation.X--;
                    break;
                case Direction.Right:
                    newLocation.X++;
                    break;
            }

            return newLocation;
        }
    }
}