
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Model.Data;
using Moq;

namespace Infusion.Gaming.LightCycles.Tests.Conditions
{
    using Infusion.Gaming.LightCycles.Conditions;

    using NUnit.Framework;

    /// <summary>
    ///     The number of players tests.
    /// </summary>
    [TestFixture]
    public class NumberOfPlayersTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The exact number of players check.
        /// </summary>
        [Test]
        public void ExactNumberOfPlayersChecks()
        {
            const int NumberOfPlayers = 3;

            var condition = new NumberOfPlayers(NumberOfPlayers);
            Assert.AreEqual(NumberOfPlayers, condition.Max);
            Assert.AreEqual(NumberOfPlayers, condition.Min);

            var gameState = MockHelper.CreateGameState();
            gameState.Object.PlayersData.Players.Add(new Player('A'));
            gameState.Object.PlayersData.Players.Add(new Player('B'));
            Assert.IsFalse(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Players.Add(new Player('C'));
            Assert.IsTrue(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Players.Add(new Player('D'));
            Assert.IsFalse(condition.Check(gameState.Object));
        }
        
        /// <summary>
        ///     The range of players checks.
        /// </summary>
        [Test]
        public void RangeOfPlayersChecks()
        {
            const int NumberOfPlayersMin = 2;
            const int NumberOfPlayersMax = 4;

            var condition = new NumberOfPlayers(NumberOfPlayersMin, NumberOfPlayersMax);
            Assert.AreEqual(NumberOfPlayersMax, condition.Max);
            Assert.AreEqual(NumberOfPlayersMin, condition.Min);

            var gameState = MockHelper.CreateGameState();
            gameState.Object.PlayersData.Players.Add(new Player('A'));
            Assert.IsFalse(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Players.Add(new Player('B'));
            Assert.IsTrue(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Players.Add(new Player('C'));
            Assert.IsTrue(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Players.Add(new Player('D'));
            Assert.IsTrue(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Players.Add(new Player('E'));
            Assert.IsFalse(condition.Check(gameState.Object));
        }
        
        #endregion
    }
}