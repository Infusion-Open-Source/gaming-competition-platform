using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCyclesCommon.Definitions;

namespace Infusion.Gaming.LightCycles.Tests.Events.Processing
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Model.Data;
    using NUnit.Framework;

    /// <summary>
    /// The player collisions processor tests.
    /// </summary>
    [TestFixture]
    public class PlayerCollisionProcessorTests
    {
        /// <summary>
        /// Collisions processor on collision removes player from next state
        /// </summary>
        [Test]
        public void PlayerCollisionProcessorConsumesCollisionEvent()
        {
            Event e = new PlayerCollisionEvent(new Identity('A'));
            IEnumerable<Event> newEvents;
            var currentState = MockHelper.CreateGameState();
            var nextState = MockHelper.CreateGameState();
            nextState.Setup(x => x.PlayersData.RemovePlayer(new Identity('A'))).Verifiable();
            
            PlayerCollisionProcessor processor = new PlayerCollisionProcessor();
            bool result = processor.Process(e, currentState.Object, nextState.Object, out newEvents);
            
            // check if result is true and new events set is empty
            Assert.IsTrue(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);

            // check whether remove player was called on the new state
            nextState.Verify();
        }

        /// <summary>
        /// Collisions processor on move does nothing
        /// </summary>
        [Test]
        public void PlayerCollisionProcessorIgnoresMoveEvent()
        {
            Event e = new PlayerMoveEvent(new Identity('A'), RelativeDirection.Left);
            IEnumerable<Event> newEvents;
            
            PlayerCollisionProcessor processor = new PlayerCollisionProcessor();
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }

        /// <summary>
        /// Collisions processor on tick does nothing
        /// </summary>
        [Test]
        public void PlayerCollisionProcessorIgnoresTickEvent()
        {
            Event e = new TickEvent(4);
            IEnumerable<Event> newEvents;

            PlayerCollisionProcessor processor = new PlayerCollisionProcessor();
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }
    }
}