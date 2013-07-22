using Infusion.Gaming.LightCycles.Model.State;

namespace Infusion.Gaming.LightCycles.Tests
{
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;
    using Moq;

    /// <summary>
    /// The mock helper.
    /// </summary>
    public class MockHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockHelper" /> class.
        /// </summary>
        protected MockHelper()
        {
        }

        /// <summary>
        /// Creates game mock with given number of players.
        /// </summary>
        /// <returns>
        /// The mocked game.
        /// </returns>
        public static Mock<IGame> CreateGame()
        {
            var mockGame = new Mock<IGame>();
            mockGame.SetupGet(x => x.CurrentState).Returns(MockHelper.CreateGameState().Object);
            return mockGame;
        }
        
        /// <summary>
        /// Create game state mock
        /// </summary>
        /// <returns>game state mock object</returns>
        public static Mock<IGameState> CreateGameState()
        {
            var gameState = new Mock<IGameState>();
            gameState.SetupGet(x => x.Map).Returns(MockHelper.CreateMap().Object);
            gameState.SetupGet(x => x.PlayersData).Returns(MockHelper.CreatePlayersData().Object);
            return gameState;
        }

        /// <summary>
        /// Create players data mock
        /// </summary>
        /// <returns>players data mock</returns>
        public static Mock<IPlayers> CreatePlayersData()
        {
            var players = new List<Identity>();
            var teams = new List<Team>();
            var playersData = new Mock<IPlayers>();
            playersData.SetupGet(x => x.Players).Returns(players);
            playersData.SetupGet(x => x.Teams).Returns(teams);
            return playersData;
        }

        /// <summary>
        /// Creates map mock object
        /// </summary>
        /// <returns>map mock object</returns>
        public static Mock<IMap> CreateMap()
        {
            var mockMap = new Mock<IMap>();
            return mockMap;
        }
    }
}