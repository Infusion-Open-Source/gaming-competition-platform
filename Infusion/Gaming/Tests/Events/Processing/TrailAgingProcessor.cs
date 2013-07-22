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
    /// The trail aging processor tests.
    /// </summary>
    [TestFixture]
    public class TrailAgingProcessorTests
    {
        /// <summary>
        /// Trails fading speed for testing
        /// </summary>
        private const float FadeSpeed = 0.36f;

        /// <summary>
        /// Trail aging processor processor on collision removes player from next state
        /// </summary>
        [Test]
        public void TrailAgingProcessorAgesTrails()
        {
            const int TurnNumber = 7;

            Event e = new TickEvent(TurnNumber);
            IEnumerable<Event> newEvents;
            var currentState = MockHelper.CreateGameState();
            var nextState = MockHelper.CreateGameState();
            nextState.Setup(x => x.PlayersData.AgeTrails(TurnNumber, FadeSpeed)).Verifiable();

            TrailAgingProcessor processor = new TrailAgingProcessor(FadeSpeed);
            bool result = processor.Process(e, currentState.Object, nextState.Object, out newEvents);
            
            // check if result is always false to allow stacking tick event processors, finally should be consumed by garbage processor
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);

            // check whether remove player was called on the new state
            nextState.Verify();
        }

        /// <summary>
        /// Trail aging processor on move does nothing
        /// </summary>
        [Test]
        public void TrailAgingProcessorIgnoresMoveEvent()
        {
            Event e = new PlayerMoveEvent(new Identity('A'), RelativeDirection.Left);
            IEnumerable<Event> newEvents;

            TrailAgingProcessor processor = new TrailAgingProcessor(FadeSpeed);
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }

        /// <summary>
        /// Trail aging processor on collision does nothing
        /// </summary>
        [Test]
        public void TrailAgingProcessorIgnoresCollisionEvent()
        {
            Event e = new PlayerCollisionEvent(new Identity('A'));
            IEnumerable<Event> newEvents;

            TrailAgingProcessor processor = new TrailAgingProcessor(FadeSpeed);
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }
    }
}