
using Infusion.Gaming.LightCycles.Model.MapData;

namespace Infusion.Gaming.LightCycles.Tests
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    using Moq;

    /// <summary>
    ///     The mock helper.
    /// </summary>
    public class MockHelper
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockHelper" /> class.
        /// </summary>
        protected MockHelper()
        {
        }

        #endregion

        #region Public Methods and Operators

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
        
        public static Mock<IGameState> CreateGameState()
        {
            var gameState = new Mock<IGameState>();
            gameState.SetupGet(x => x.Map).Returns(MockHelper.CreateMap().Object);
            gameState.SetupGet(x => x.PlayersData).Returns(MockHelper.CreatePlayersData().Object);
            return gameState;
        }

        public static Mock<IPlayersData> CreatePlayersData()
        {
            var players = new List<Player>();
            var teams = new List<Team>();
            var playersData = new Mock<IPlayersData>();
            playersData.SetupGet(x => x.Players).Returns(players);
            playersData.SetupGet(x => x.Teams).Returns(teams);
            return playersData;
        }

        public static Mock<IMap> CreateMap()
        {
            var mockMap = new Mock<IMap>();
            return mockMap;
        }

        #endregion
    }
}