namespace Infusion.Gaming.LightCycles.Tests.Events.Filtering
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Events.Filtering;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;
    using NUnit.Framework;

    /// <summary>
    /// The events tests.
    /// </summary>
    [TestFixture]
    public class PlayerRecentEventFilterTests
    {
        /// <summary>
        /// Checks keeping only players recent event on event list with empty events list
        /// </summary>
        [Test]
        public void KeepPlayerRecentEvent_CheckOnEmptyPlayerList()
        {
            // setup
            List<Event> events = new List<Event>();

            List<Player> players = new List<Player>();

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            // test
            PlayerRecentEventFilter filter = new PlayerRecentEventFilter();
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if recent event left on events list
            Assert.AreEqual(0, filteredEvents.Count);
        }

        /// <summary>
        /// Checks keeping only players recent event on event list with non empty events list
        /// </summary>
        [Test]
        public void KeepPlayerRecentEvent_CheckOnNonEmptyPlayerList()
        {
            // setup
            List<Event> events = new List<Event>();
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Right));
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Undefined));
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.StraightForward));
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Left));

            List<Player> players = new List<Player>();

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            // test
            PlayerRecentEventFilter filter = new PlayerRecentEventFilter();
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if recent event left on events list
            Assert.AreEqual(1, filteredEvents.Count);
            Assert.AreEqual('A', ((PlayerMoveEvent)filteredEvents[0]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Left, ((PlayerMoveEvent)filteredEvents[0]).Direction);
        }

        /// <summary>
        /// Checks keeping only players recent event on event list with mixed events list
        /// </summary>
        [Test]
        public void KeepPlayerRecentEvent_CheckOnMixedPlayerList()
        {
            // setup
            List<Event> events = new List<Event>();
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Right));
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Undefined));
            events.Add(new PlayerMoveEvent(new Player('B'), RelativeDirectionEnum.StraightForward));
            events.Add(new PlayerMoveEvent(new Player('B'), RelativeDirectionEnum.Left));

            List<Player> players = new List<Player>();

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            // test
            PlayerRecentEventFilter filter = new PlayerRecentEventFilter();
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if recent event left on events list
            Assert.AreEqual(2, filteredEvents.Count);
            Assert.AreEqual('A', ((PlayerMoveEvent)filteredEvents[0]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Undefined, ((PlayerMoveEvent)filteredEvents[0]).Direction);
            Assert.AreEqual('B', ((PlayerMoveEvent)filteredEvents[1]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Left, ((PlayerMoveEvent)filteredEvents[1]).Direction);
        }
    }
}