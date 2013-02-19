namespace Infusion.Gaming.LightCycles.Tests.Events
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    using NUnit.Framework;

    /// <summary>
    /// The events arbiter tests.
    /// </summary>
    [TestFixture]
    public class EventsArbiterTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Arbitrage checks.
        /// </summary>
        [Test]
        public void ArbitrageChecks()
        {
            var playersInGame = new List<Player> { new Player('A'), new Player('B') };
            var events = new EventsCollection
                             {
                                 new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Left), 
                                 new PlayerMoveEvent(new Player('B'), RelativeDirectionEnum.Left), 
                                 new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Right), 
                                 new PlayerMoveEvent(new Player('B'), RelativeDirectionEnum.Right), 
                                 new PlayerMoveEvent(new Player('C'), RelativeDirectionEnum.Right), 
                                 new PlayerMoveEvent(
                                     new Player('B'), RelativeDirectionEnum.StraightForward)
                             };

            var arbiter = new EventsArbiter();
            EventsCollection ouputEvents = arbiter.Arbitrage(events, playersInGame);

            Assert.AreEqual(2, ouputEvents.Count);

            Assert.AreEqual('A', ((PlayerMoveEvent)ouputEvents[0]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Right, ((PlayerMoveEvent)ouputEvents[0]).Direction);

            Assert.AreEqual('B', ((PlayerMoveEvent)ouputEvents[1]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.StraightForward, ((PlayerMoveEvent)ouputEvents[1]).Direction);
        }

        /// <summary>
        /// Arbitrage inputs checks.
        /// </summary>
        [Test]
        public void ArbitrageInputsChecks()
        {
            var playersInGame = new List<Player> { new Player('A'), new Player('B') };
            var events = new EventsCollection();

            var arbiter = new EventsArbiter();

            Assert.Throws<ArgumentNullException>(() => arbiter.Arbitrage(events, null));
            Assert.Throws<ArgumentNullException>(() => arbiter.Arbitrage(null, playersInGame));
        }

        /// <summary>
        /// Arbitrage on empty list makes default behavior.
        /// </summary>
        [Test]
        public void ArbitrageOnEmptyListMakesDefaultBehavior()
        {
            var playersInGame = new List<Player> { new Player('A'), new Player('B') };
            var events = new EventsCollection();

            var arbiter = new EventsArbiter();
            EventsCollection ouputEvents = arbiter.Arbitrage(events, playersInGame);

            Assert.AreEqual(2, ouputEvents.Count);

            Assert.AreEqual('A', ((PlayerMoveEvent)ouputEvents[0]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Undefined, ((PlayerMoveEvent)ouputEvents[0]).Direction);

            Assert.AreEqual('B', ((PlayerMoveEvent)ouputEvents[1]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Undefined, ((PlayerMoveEvent)ouputEvents[1]).Direction);
        }

        #endregion
    }
}