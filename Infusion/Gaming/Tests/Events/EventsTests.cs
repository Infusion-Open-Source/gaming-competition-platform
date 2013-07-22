using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCyclesCommon.Definitions;

namespace Infusion.Gaming.LightCycles.Tests.Events
{
    using System;

    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model.Data;
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
            var player = new Identity('A');
            Assert.AreEqual(player, new PlayerCollisionEvent(player).Player);
        }

        /// <summary>
        /// The player move event checks.
        /// </summary>
        [Test]
        public void PlayerMoveEventChecks()
        {
            var player = new Identity('A');
            Assert.AreEqual(player, new PlayerMoveEvent(player, RelativeDirection.Left).Player);
            Assert.AreEqual(RelativeDirection.Left, new PlayerMoveEvent(player, RelativeDirection.Left).Direction);
            Assert.AreEqual(RelativeDirection.Right, new PlayerMoveEvent(player, RelativeDirection.Right).Direction);
            Assert.AreEqual(RelativeDirection.StraightAhead, new PlayerMoveEvent(player, RelativeDirection.StraightAhead).Direction);
            Assert.AreEqual(RelativeDirection.Undefined, new PlayerMoveEvent(player, RelativeDirection.Undefined).Direction);
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