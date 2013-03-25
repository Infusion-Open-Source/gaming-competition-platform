namespace Infusion.Gaming.LightCycles.Tests.Events
{
    using System;

    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    using NUnit.Framework;

    /// <summary>
    /// The events tests.
    /// </summary>
    [TestFixture]
    public class EventsTests
    {
        /// <summary>
        /// The player collision event checks.
        /// </summary>
        [Test]
        public void PlayerCollisionEventChecks()
        {
            var player = new Player('A');
            Assert.AreEqual(player, new PlayerCollisionEvent(player).Player);
        }

        /// <summary>
        /// The player move event checks.
        /// </summary>
        [Test]
        public void PlayerMoveEventChecks()
        {
            var player = new Player('A');
            Assert.AreEqual(player, new PlayerMoveEvent(player, RelativeDirectionEnum.Left).Player);
            Assert.AreEqual(RelativeDirectionEnum.Left, new PlayerMoveEvent(player, RelativeDirectionEnum.Left).Direction);
            Assert.AreEqual(RelativeDirectionEnum.Right, new PlayerMoveEvent(player, RelativeDirectionEnum.Right).Direction);
            Assert.AreEqual(RelativeDirectionEnum.StraightForward, new PlayerMoveEvent(player, RelativeDirectionEnum.StraightForward).Direction);
            Assert.AreEqual(RelativeDirectionEnum.Undefined, new PlayerMoveEvent(player, RelativeDirectionEnum.Undefined).Direction);
        }

        /// <summary>
        /// The tick event checks.
        /// </summary>
        [Test]
        public void TickEventChecks()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TickEvent(-1));
            Assert.AreEqual(0, new TickEvent(0).Turn);
            Assert.AreEqual(3, new TickEvent(3).Turn);
            Assert.AreEqual(6, new TickEvent(6).Turn);
            Assert.AreEqual(12, new TickEvent(12).Turn);
        }
    }
}