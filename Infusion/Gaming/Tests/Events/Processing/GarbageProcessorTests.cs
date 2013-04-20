namespace Infusion.Gaming.LightCycles.Tests.Events.Processing
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;
    using NUnit.Framework;

    /// <summary>
    /// The garbage processor tests.
    /// </summary>
    [TestFixture]
    public class GarbageProcessorTests
    {
        /// <summary>
        /// Garbage processors consumes move event
        /// </summary>
        [Test]
        public void GarbageProcessorConsumesMoveEvent()
        {
            Event e = new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Undefined);

            IEnumerable<Event> newEvents;

            GarbageProcessor processor = new GarbageProcessor(true);
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is always true and new events set is empty
            Assert.IsTrue(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }

        /// <summary>
        /// Garbage processors consumes collision event
        /// </summary>
        [Test]
        public void GarbageProcessorConsumesCollisionEvent()
        {
            Event e = new PlayerCollisionEvent(new Player('A'));

            IEnumerable<Event> newEvents;

            GarbageProcessor processor = new GarbageProcessor(true);
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is always true and new events set is empty
            Assert.IsTrue(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }

        /// <summary>
        /// Garbage processors consumes tick event
        /// </summary>
        [Test]
        public void GarbageProcessorConsumesTickEvent()
        {
            Event e = new TickEvent(3);

            IEnumerable<Event> newEvents;

            GarbageProcessor processor = new GarbageProcessor(true);
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is always true and new events set is empty
            Assert.IsTrue(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }
    }
}