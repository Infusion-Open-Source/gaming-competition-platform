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

            List<Player> players = new List<Player>();
            players.Add(new Player('A'));

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            const RelativeDirectionEnum DirectionToBeAppended = RelativeDirectionEnum.Left;

            // test
            IdlePlayerMoveEventAppender filter = new IdlePlayerMoveEventAppender(DirectionToBeAppended);
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if event was created for player A
            Assert.AreEqual(1, filteredEvents.Count);
            Assert.AreEqual('A', ((PlayerMoveEvent)filteredEvents[0]).Player.Id);
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
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Right));
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Left));

            List<Player> players = new List<Player>();
            players.Add(new Player('A'));
            players.Add(new Player('B'));

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            const RelativeDirectionEnum DirectionToBeAppended = RelativeDirectionEnum.StraightForward;

            // test
            IdlePlayerMoveEventAppender filter = new IdlePlayerMoveEventAppender(DirectionToBeAppended);
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if event was created for player A
            Assert.AreEqual(3, filteredEvents.Count);
            Assert.AreEqual('B', ((PlayerMoveEvent)filteredEvents[2]).Player.Id);
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
            events.Add(new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Right));

            List<Player> players = new List<Player>();
            players.Add(new Player('A'));

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            const RelativeDirectionEnum DirectionToBeAppended = RelativeDirectionEnum.Left;

            // test
            IdlePlayerMoveEventAppender filter = new IdlePlayerMoveEventAppender(DirectionToBeAppended);
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if event for player A was unchanged and naothing has been added
            Assert.AreEqual(1, filteredEvents.Count);
            Assert.AreEqual('A', ((PlayerMoveEvent)filteredEvents[0]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Right, ((PlayerMoveEvent)filteredEvents[0]).Direction);
        }
    }
}