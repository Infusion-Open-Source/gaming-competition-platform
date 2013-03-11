
namespace Infusion.Gaming.LightCycles.Tests.Model.Data
{
    using System.Drawing;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Defines;

    using NUnit.Framework;

    /// <summary>
    ///     The direction helper tests.
    /// </summary>
    [TestFixture]
    public class DirectionHelperTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Change direction checks.
        /// </summary>
        [Test]
        public void ChangeDirectionChecks()
        {
            Assert.AreEqual(
                DirectionEnum.Left, 
                DirectionHelper.ChangeDirection(DirectionEnum.Left, RelativeDirectionEnum.StraightForward));
            Assert.AreEqual(
                DirectionEnum.Right, 
                DirectionHelper.ChangeDirection(DirectionEnum.Right, RelativeDirectionEnum.StraightForward));
            Assert.AreEqual(
                DirectionEnum.Up, 
                DirectionHelper.ChangeDirection(DirectionEnum.Up, RelativeDirectionEnum.StraightForward));
            Assert.AreEqual(
                DirectionEnum.Down, 
                DirectionHelper.ChangeDirection(DirectionEnum.Down, RelativeDirectionEnum.StraightForward));

            Assert.AreEqual(
                DirectionEnum.Left, DirectionHelper.ChangeDirection(DirectionEnum.Left, RelativeDirectionEnum.Undefined));
            Assert.AreEqual(
                DirectionEnum.Right, 
                DirectionHelper.ChangeDirection(DirectionEnum.Right, RelativeDirectionEnum.Undefined));
            Assert.AreEqual(
                DirectionEnum.Up, DirectionHelper.ChangeDirection(DirectionEnum.Up, RelativeDirectionEnum.Undefined));
            Assert.AreEqual(
                DirectionEnum.Down, DirectionHelper.ChangeDirection(DirectionEnum.Down, RelativeDirectionEnum.Undefined));

            Assert.AreEqual(
                DirectionEnum.Down, DirectionHelper.ChangeDirection(DirectionEnum.Left, RelativeDirectionEnum.Left));
            Assert.AreEqual(
                DirectionEnum.Up, DirectionHelper.ChangeDirection(DirectionEnum.Right, RelativeDirectionEnum.Left));
            Assert.AreEqual(
                DirectionEnum.Left, DirectionHelper.ChangeDirection(DirectionEnum.Up, RelativeDirectionEnum.Left));
            Assert.AreEqual(
                DirectionEnum.Right, DirectionHelper.ChangeDirection(DirectionEnum.Down, RelativeDirectionEnum.Left));

            Assert.AreEqual(
                DirectionEnum.Up, DirectionHelper.ChangeDirection(DirectionEnum.Left, RelativeDirectionEnum.Right));
            Assert.AreEqual(
                DirectionEnum.Down, DirectionHelper.ChangeDirection(DirectionEnum.Right, RelativeDirectionEnum.Right));
            Assert.AreEqual(
                DirectionEnum.Right, DirectionHelper.ChangeDirection(DirectionEnum.Up, RelativeDirectionEnum.Right));
            Assert.AreEqual(
                DirectionEnum.Left, DirectionHelper.ChangeDirection(DirectionEnum.Down, RelativeDirectionEnum.Right));
        }

        /// <summary>
        ///     The direction checking checks.
        /// </summary>
        [Test]
        public void DirectionCheckingChecks()
        {
            Assert.AreEqual(DirectionEnum.Left, DirectionHelper.CheckDirection(-1, 0, 0, 0));
            Assert.AreEqual(DirectionEnum.Right, DirectionHelper.CheckDirection(1, 0, 0, 0));
            Assert.AreEqual(DirectionEnum.Up, DirectionHelper.CheckDirection(0, -1, 0, 0));
            Assert.AreEqual(DirectionEnum.Down, DirectionHelper.CheckDirection(0, 1, 0, 0));
            Assert.AreEqual(DirectionEnum.Undefined, DirectionHelper.CheckDirection(0, 0, 0, 0));

            Assert.AreEqual(DirectionEnum.Left, DirectionHelper.CheckDirection(new Point(-1, 0), new Point(0, 0)));
            Assert.AreEqual(DirectionEnum.Right, DirectionHelper.CheckDirection(new Point(1, 0), new Point(0, 0)));
            Assert.AreEqual(DirectionEnum.Up, DirectionHelper.CheckDirection(new Point(0, -1), new Point(0, 0)));
            Assert.AreEqual(DirectionEnum.Down, DirectionHelper.CheckDirection(new Point(0, 1), new Point(0, 0)));
            Assert.AreEqual(DirectionEnum.Undefined, DirectionHelper.CheckDirection(new Point(0, 0), new Point(0, 0)));
        }

        #endregion
    }
}