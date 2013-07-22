using Infusion.Gaming.LightCyclesCommon.Definitions;

namespace Infusion.Gaming.LightCycles.Tests.Model.Data
{
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Model;
    using NUnit.Framework;

    /// <summary>
    /// The direction helper tests.
    /// </summary>
    [TestFixture]
    public class DirectionHelperTests
    {
        /// <summary>
        /// Change direction checks.
        /// </summary>
        [Test]
        public void ChangeDirectionChecks()
        {
            Assert.AreEqual(Direction.Left, DirectionHelper.ChangeDirection(Direction.Left, RelativeDirection.StraightAhead));
            Assert.AreEqual(Direction.Right, DirectionHelper.ChangeDirection(Direction.Right, RelativeDirection.StraightAhead));
            Assert.AreEqual(Direction.Up, DirectionHelper.ChangeDirection(Direction.Up, RelativeDirection.StraightAhead));
            Assert.AreEqual(Direction.Down, DirectionHelper.ChangeDirection(Direction.Down, RelativeDirection.StraightAhead));

            Assert.AreEqual(Direction.Left, DirectionHelper.ChangeDirection(Direction.Left, RelativeDirection.Undefined));
            Assert.AreEqual(Direction.Right, DirectionHelper.ChangeDirection(Direction.Right, RelativeDirection.Undefined));
            Assert.AreEqual(Direction.Up, DirectionHelper.ChangeDirection(Direction.Up, RelativeDirection.Undefined));
            Assert.AreEqual(Direction.Down, DirectionHelper.ChangeDirection(Direction.Down, RelativeDirection.Undefined));

            Assert.AreEqual(Direction.Down, DirectionHelper.ChangeDirection(Direction.Left, RelativeDirection.Left));
            Assert.AreEqual(Direction.Up, DirectionHelper.ChangeDirection(Direction.Right, RelativeDirection.Left));
            Assert.AreEqual(Direction.Left, DirectionHelper.ChangeDirection(Direction.Up, RelativeDirection.Left));
            Assert.AreEqual(Direction.Right, DirectionHelper.ChangeDirection(Direction.Down, RelativeDirection.Left));

            Assert.AreEqual(Direction.Up, DirectionHelper.ChangeDirection(Direction.Left, RelativeDirection.Right));
            Assert.AreEqual(Direction.Down, DirectionHelper.ChangeDirection(Direction.Right, RelativeDirection.Right));
            Assert.AreEqual(Direction.Right, DirectionHelper.ChangeDirection(Direction.Up, RelativeDirection.Right));
            Assert.AreEqual(Direction.Left, DirectionHelper.ChangeDirection(Direction.Down, RelativeDirection.Right));
        }

        /// <summary>
        /// The direction checking checks.
        /// </summary>
        [Test]
        public void DirectionCheckingChecks()
        {
            Assert.AreEqual(Direction.Left, DirectionHelper.CheckDirection(-1, 0, 0, 0));
            Assert.AreEqual(Direction.Right, DirectionHelper.CheckDirection(1, 0, 0, 0));
            Assert.AreEqual(Direction.Up, DirectionHelper.CheckDirection(0, -1, 0, 0));
            Assert.AreEqual(Direction.Down, DirectionHelper.CheckDirection(0, 1, 0, 0));
            Assert.AreEqual(Direction.Undefined, DirectionHelper.CheckDirection(0, 0, 0, 0));

            Assert.AreEqual(Direction.Left, DirectionHelper.CheckDirection(new Point(-1, 0), new Point(0, 0)));
            Assert.AreEqual(Direction.Right, DirectionHelper.CheckDirection(new Point(1, 0), new Point(0, 0)));
            Assert.AreEqual(Direction.Up, DirectionHelper.CheckDirection(new Point(0, -1), new Point(0, 0)));
            Assert.AreEqual(Direction.Down, DirectionHelper.CheckDirection(new Point(0, 1), new Point(0, 0)));
            Assert.AreEqual(Direction.Undefined, DirectionHelper.CheckDirection(new Point(0, 0), new Point(0, 0)));
        }
    }
}