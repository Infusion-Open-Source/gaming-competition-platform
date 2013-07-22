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
    public class PlayersInGameFilterTests
    {
        /// <summary>
        /// Checks filtering players that are not in game state with empty events collection
        /// </summary>
        [Test]
        public void FilterOutPlayerNotInGameState_CheckOnEmptyEventsList()
        {
            // setup
            List<Event> events = new List<Event>();
            
            List<Identity> players = new List<Identity>();
            players.Add(new Identity('A'));

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            // test
            PlayersInGameFilter filter = new PlayersInGameFilter();
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if event list is stil empty
            Assert.AreEqual(0, filteredEvents.Count);
        }

        /// <summary>
        /// Checks filtering players that are not in game state with events sourced from different players
        /// </summary>
        [Test]
        public void FilterOutPlayerNotInGameState_CheckOnMixedEventsList()
        {
            // setup
            List<Event> events = new List<Event>();
            events.Add(new PlayerMoveEvent(new Identity('A'), RelativeDirection.Undefined));
            events.Add(new PlayerMoveEvent(new Identity('B'), RelativeDirection.Undefined));
            events.Add(new PlayerMoveEvent(new Identity('B'), RelativeDirection.StraightAhead));
            events.Add(new PlayerMoveEvent(new Identity('C'), RelativeDirection.Undefined));

            List<Identity> players = new List<Identity>();
            players.Add(new Identity('A'));
            players.Add(new Identity('C'));

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            // test
            PlayersInGameFilter filter = new PlayersInGameFilter();
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if player B event was removed
            Assert.AreEqual(2, filteredEvents.Count);
            Assert.AreEqual('A', ((PlayerMoveEvent)filteredEvents[0]).Player.Identifier);
            Assert.AreEqual('C', ((PlayerMoveEvent)filteredEvents[1]).Player.Identifier);
        }

        /// <summary>
        /// Checks filtering players that are not in game state with an empty players list
        /// </summary>
        [Test]
        public void FilterOutPlayerNotInGameState_CheckOnEmptyPlayerList()
        {
            // setup
            List<Event> events = new List<Event>();
            events.Add(new PlayerMoveEvent(new Identity('A'), RelativeDirection.Undefined));
            events.Add(new PlayerMoveEvent(new Identity('B'), RelativeDirection.Undefined));
            events.Add(new PlayerMoveEvent(new Identity('B'), RelativeDirection.StraightAhead));
            events.Add(new PlayerMoveEvent(new Identity('C'), RelativeDirection.Undefined));

            List<Identity> players = new List<Identity>();

            var gameState = MockHelper.CreateGameState();
            gameState.SetupGet(x => x.PlayersData.Players).Returns(players);

            // test
            PlayersInGameFilter filter = new PlayersInGameFilter();
            List<Event> filteredEvents = new List<Event>(filter.Filter(gameState.Object, events));

            // check if all players event were removed
            Assert.AreEqual(0, filteredEvents.Count);
        }
    }
}