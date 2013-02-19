namespace Infusion.Gaming.LightCycles.Tests.Conditions
{
    using Infusion.Gaming.LightCycles.Conditions;

    using NUnit.Framework;

    /// <summary>
    /// The number of players tests.
    /// </summary>
    [TestFixture]
    public class NumberOfPlayersTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The exact number of players checks.
        /// </summary>
        [Test]
        public void ExactNumberOfPlayersChecks()
        {
            const int NumberOfPlayers = 3;

            var condition = new NumberOfPlayers(NumberOfPlayers);
            Assert.AreEqual(NumberOfPlayers, condition.Max);
            Assert.AreEqual(NumberOfPlayers, condition.Min);

            Assert.IsFalse(condition.Check(MockHelper.CreateGame(NumberOfPlayers - 1).Object));
            Assert.IsTrue(condition.Check(MockHelper.CreateGame(NumberOfPlayers).Object));
            Assert.IsFalse(condition.Check(MockHelper.CreateGame(NumberOfPlayers + 1).Object));
        }

        /// <summary>
        /// The range of players checks.
        /// </summary>
        [Test]
        public void RangeOfPlayersChecks()
        {
            const int NumberOfPlayersMin = 2;
            const int NumberOfPlayersMax = 4;

            var condition = new NumberOfPlayers(NumberOfPlayersMin, NumberOfPlayersMax);
            Assert.AreEqual(NumberOfPlayersMax, condition.Max);
            Assert.AreEqual(NumberOfPlayersMin, condition.Min);

            Assert.IsFalse(condition.Check(MockHelper.CreateGame(0).Object));
            Assert.IsFalse(condition.Check(MockHelper.CreateGame(1).Object));
            Assert.IsTrue(condition.Check(MockHelper.CreateGame(2).Object));
            Assert.IsTrue(condition.Check(MockHelper.CreateGame(3).Object));
            Assert.IsTrue(condition.Check(MockHelper.CreateGame(4).Object));
            Assert.IsFalse(condition.Check(MockHelper.CreateGame(5).Object));
            Assert.IsFalse(condition.Check(MockHelper.CreateGame(6).Object));
        }
        
        #endregion
    }
}