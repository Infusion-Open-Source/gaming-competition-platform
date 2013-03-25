namespace Infusion.Gaming.LightCycles.Tests.Conditions
{
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Model.Data;
    using NUnit.Framework;

    /// <summary>
    /// The number of teams tests.
    /// </summary>
    [TestFixture]
    public class NumberOfTeamsTests
    {
        /// <summary>
        /// The exact number of teams check.
        /// </summary>
        [Test]
        public void ExactNumberOfTeamsChecks()
        {
            const int NumberOfTeams = 3;

            var condition = new NumberOfTeams(NumberOfTeams);
            Assert.AreEqual(NumberOfTeams, condition.Max);
            Assert.AreEqual(NumberOfTeams, condition.Min);

            var gameState = MockHelper.CreateGameState();
            gameState.Object.PlayersData.Teams.Add(new Team('A'));
            gameState.Object.PlayersData.Teams.Add(new Team('B'));
            Assert.IsFalse(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Teams.Add(new Team('C'));
            Assert.IsTrue(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Teams.Add(new Team('D'));
            Assert.IsFalse(condition.Check(gameState.Object));
        }
        
        /// <summary>
        /// The range of teams checks.
        /// </summary>
        [Test]
        public void RangeOfTeamsChecks()
        {
            const int NumberOfTeamsMin = 2;
            const int NumberOfTeamsMax = 4;

            var condition = new NumberOfTeams(NumberOfTeamsMin, NumberOfTeamsMax);
            Assert.AreEqual(NumberOfTeamsMax, condition.Max);
            Assert.AreEqual(NumberOfTeamsMin, condition.Min);

            var gameState = MockHelper.CreateGameState();
            gameState.Object.PlayersData.Teams.Add(new Team('A'));
            Assert.IsFalse(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Teams.Add(new Team('B'));
            Assert.IsTrue(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Teams.Add(new Team('C'));
            Assert.IsTrue(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Teams.Add(new Team('D'));
            Assert.IsTrue(condition.Check(gameState.Object));
            gameState.Object.PlayersData.Teams.Add(new Team('E'));
            Assert.IsFalse(condition.Check(gameState.Object));
        }
    }
}