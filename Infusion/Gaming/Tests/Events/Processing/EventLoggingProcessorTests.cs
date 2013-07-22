using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCyclesCommon.Definitions;

namespace Infusion.Gaming.LightCycles.Tests.Events.Processing
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Model.Data;
    using NUnit.Framework;

    /// <summary>
    /// The event logging processor tests.
    /// </summary>
    [TestFixture]
    public class EventLoggingProcessorTests
    {
        /// <summary>
        /// Event logging processor does not process move event
        /// </summary>
        [Test]
        public void EventLoggingProcessorDoesNotProcessMoveEvent()
        {
            Event e = new PlayerMoveEvent(new Identity('A'), RelativeDirection.Undefined);

            IEnumerable<Event> newEvents;

            EventLoggingProcessor processor = new EventLoggingProcessor(true);
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is always false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }

        /// <summary>
        /// Event logging processor does not process collision event
        /// </summary>
        [Test]
        public void EventLoggingProcessorDoesNotProcessCollisionEvent()
        {
            Event e = new PlayerCollisionEvent(new Identity('A'));

            IEnumerable<Event> newEvents;

            EventLoggingProcessor processor = new EventLoggingProcessor(true);
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is always false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }

        /// <summary>
        /// Event logging processor does not process tick event
        /// </summary>
        [Test]
        public void EventLoggingProcessorDoesNotProcessTickEvent()
        {
            Event e = new TickEvent(6);

            IEnumerable<Event> newEvents;

            EventLoggingProcessor processor = new EventLoggingProcessor(true);
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is always false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }
    }
}