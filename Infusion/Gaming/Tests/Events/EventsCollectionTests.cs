namespace Infusion.Gaming.LightCycles.Tests.Events
{
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    using NUnit.Framework;

    /// <summary>
    /// The events collection tests.
    /// </summary>
    [TestFixture]
    public class EventsCollectionTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The player filtered recent event check.
        /// </summary>
        [Test]
        public void PlayerFilteredRecentEventCheck()
        {
            var playerA = new Player('A');
            var playerB = new Player('B');

            var events = new EventsCollection();
            events.Add(new PlayerMoveEvent(playerA, RelativeDirectionEnum.Left));
            events.Add(new PlayerMoveEvent(playerB, RelativeDirectionEnum.Left));
            events.Add(new PlayerMoveEvent(playerA, RelativeDirectionEnum.Right));
            events.Add(new PlayerMoveEvent(playerB, RelativeDirectionEnum.Right));

            events = events.FilterBy(playerA);
            Assert.IsNotNull(events.MostRecent);
            Assert.AreEqual(RelativeDirectionEnum.Right, ((PlayerMoveEvent)events.MostRecent).Direction);
        }

        /// <summary>
        /// The player filtering checks.
        /// </summary>
        [Test]
        public void PlayerFilteringChecks()
        {
            var playerA = new Player('A');
            var playerB = new Player('B');

            var events = new EventsCollection();
            events.Add(new PlayerMoveEvent(playerA, RelativeDirectionEnum.Left));
            events.Add(new PlayerMoveEvent(playerB, RelativeDirectionEnum.Left));
            events.Add(new PlayerMoveEvent(playerA, RelativeDirectionEnum.Right));
            events.Add(new PlayerMoveEvent(playerB, RelativeDirectionEnum.Right));

            Assert.AreEqual(4, events.Count);
            Assert.AreEqual(2, events.FilterBy(playerA).Count);
            Assert.AreEqual(2, events.FilterBy(playerB).Count);
        }

        /// <summary>
        /// The recent event checks.
        /// </summary>
        [Test]
        public void RecentEventChecks()
        {
            var playerA = new Player('A');

            var events = new EventsCollection();
            events.Add(new PlayerMoveEvent(playerA, RelativeDirectionEnum.Left));
            events.Add(new PlayerMoveEvent(playerA, RelativeDirectionEnum.Right));
            events.Add(new PlayerMoveEvent(playerA, RelativeDirectionEnum.StraightForward));

            Assert.IsNotNull(events.MostRecent);
            Assert.AreEqual(RelativeDirectionEnum.StraightForward, ((PlayerMoveEvent)events.MostRecent).Direction);
        }
        
        #endregion
    }
}