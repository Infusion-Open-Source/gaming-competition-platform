using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCyclesCommon.Definitions;

namespace Infusion.Gaming.LightCycles.Tests.Events.Filtering
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Events.Filtering;
    using Infusion.Gaming.LightCycles.Model.Data;
    using NUnit.Framework;

    /// <summary>
    /// The events tests.
    /// </summary>
    [TestFixture]
    public class IdlePlayerMoveEventAppenderTests
    {
        /// <summary>
        /// Checks appending move event for idle players with empty events collection
        /// </summary>
        [Test]
        public void AppendIddlePlayerMove_CheckOnEmptyEventsList()
        {
            // setup
            List<Event> events = new List<Event>();

            List<Identity> players = new List<Identity>();
            players.Add(new Identity('A'));

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            const RelativeDirection DirectionToBeAppended = RelativeDirection.Left;

            // test
            IdlePlayerMoveEventAppender filter = new IdlePlayerMoveEventAppender(DirectionToBeAppended);
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if event was created for player A
            Assert.AreEqual(1, filteredEvents.Count);
            Assert.AreEqual('A', ((PlayerMoveEvent)filteredEvents[0]).Player.Identifier);
            Assert.AreEqual(DirectionToBeAppended, ((PlayerMoveEvent)filteredEvents[0]).Direction);
        }

        /// <summary>
        /// Checks appending move event for idle players with non empty events collection
        /// </summary>
        [Test]
        public void AppendIddlePlayerMove_CheckOnNonEmptyEventsList()
        {
            // setup
            List<Event> events = new List<Event>();
            events.Add(new PlayerMoveEvent(new Identity('A'), RelativeDirection.Right));
            events.Add(new PlayerMoveEvent(new Identity('A'), RelativeDirection.Left));

            List<Identity> players = new List<Identity>();
            players.Add(new Identity('A'));
            players.Add(new Identity('B'));

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            const RelativeDirection DirectionToBeAppended = RelativeDirection.StraightAhead;

            // test
            IdlePlayerMoveEventAppender filter = new IdlePlayerMoveEventAppender(DirectionToBeAppended);
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if event was created for player A
            Assert.AreEqual(3, filteredEvents.Count);
            Assert.AreEqual('B', ((PlayerMoveEvent)filteredEvents[2]).Player.Identifier);
            Assert.AreEqual(DirectionToBeAppended, ((PlayerMoveEvent)filteredEvents[2]).Direction);
        }

        /// <summary>
        /// Checks appending move event does not change already existing event of a player
        /// </summary>
        [Test]
        public void AppendIddlePlayerMove_CheckOnExisting()
        {
            // setup
            List<Event> events = new List<Event>();
            events.Add(new PlayerMoveEvent(new Identity('A'), RelativeDirection.Right));

            List<Identity> players = new List<Identity>();
            players.Add(new Identity('A'));

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            const RelativeDirection DirectionToBeAppended = RelativeDirection.Left;

            // test
            IdlePlayerMoveEventAppender filter = new IdlePlayerMoveEventAppender(DirectionToBeAppended);
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if event for player A was unchanged and naothing has been added
            Assert.AreEqual(1, filteredEvents.Count);
            Assert.AreEqual('A', ((PlayerMoveEvent)filteredEvents[0]).Player.Identifier);
            Assert.AreEqual(RelativeDirection.Right, ((PlayerMoveEvent)filteredEvents[0]).Direction);
        }
    }
}