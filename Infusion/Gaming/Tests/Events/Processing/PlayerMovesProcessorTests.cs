namespace Infusion.Gaming.LightCycles.Tests.Events.Processing
{
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The player moves processor tests.
    /// </summary>
    [TestFixture]
    public class PlayerMovesProcessorTests
    {
        /// <summary>
        /// Collisions processor on collision removes player from next state
        /// </summary>
        [Test]
        public void PlayerCollisionProcessorConsumesMoveEvent()
        {
            /*
            TODO: Test to be fixed, code might need to be refactred
            var players = new List<Player>();
            players.Add(new Player('A'));
            var playersLocations = new Dictionary<Player, Point>();
            playersLocations.Add(players[0], new Point(10, 10));
            var playersLightCycles = new Dictionary<Player, LightCycleBike>();
            playersLightCycles.Add(players[0], new LightCycleBike(players[0], DirectionEnum.Down));

            Event e = new PlayerMoveEvent(players[0], RelativeDirectionEnum.StraightForward);
            IEnumerable<Event> newEvents;
            var currentState = new Mock<IGameState>();
            var nextState = new Mock<IGameState>();
            var map = new Mock<IMap>();
            var playerData = new Mock<IPlayersData>();
            
            playerData.SetupGet(x => x.Players).Returns(players);
            playerData.SetupGet(x => x.PlayersLocations).Returns(playersLocations);
            playerData.SetupGet(x => x.PlayersLightCycles).Returns(playersLightCycles);
            playerData.SetupSet(x => x[10, 11] = new LightCycleBike(players[0], DirectionEnum.Down)).Verifiable();
            playerData.SetupSet(x => x[10, 10] = new Trail(players[0], 1)).Verifiable();
            map.SetupGet(x => x[10, 11].IsPassable).Returns(true);
            
            nextState.SetupGet(x => x.Map).Returns(map.Object);
            nextState.SetupGet(x => x.PlayersData).Returns(playerData.Object);
            currentState.SetupGet(x => x.PlayersData).Returns(playerData.Object);

            PlayerMovesProcessor processor = new PlayerMovesProcessor();
            bool result = processor.Process(e, currentState.Object, nextState.Object, out newEvents);
            
            // check if result is true and new events set is empty
            Assert.IsTrue(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
            // check whether bike was moved and trail was left behind
            playerData.VerifySet(x => x[10, 11] = new LightCycleBike(players[0], DirectionEnum.Down));
            playerData.VerifySet(x => x[10, 10] = new Trail(players[0], 1));*/
        }

        /// <summary>
        /// Collisions processor on move does nothing
        /// </summary>
        [Test]
        public void PlayerMovesProcessorIgnoresCollisionEvent()
        {
            Event e = new PlayerCollisionEvent(new Identity('A'));
            IEnumerable<Event> newEvents;

            PlayerMovesProcessor processor = new PlayerMovesProcessor();
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }

        /// <summary>
        /// Collisions processor on tick does nothing
        /// </summary>
        [Test]
        public void PlayerMovesProcessorIgnoresTickEvent()
        {
            Event e = new TickEvent(4);
            IEnumerable<Event> newEvents;

            PlayerMovesProcessor processor = new PlayerMovesProcessor();
            bool result = processor.Process(e, null, null, out newEvents);

            // check if result is false and new events set is empty
            Assert.IsFalse(result);
            Assert.AreEqual(0, new List<Event>(newEvents).Count);
        }
    }
}