using Infusion.Gaming.LightCycles.Model.Definitions;

namespace Infusion.Gaming.LightCycles.Tests.Conditions
{
    using Infusion.Gaming.LightCycles.Conditions;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The end condition tests.
    /// </summary>
    [TestFixture]
    public class EndConditionTests
    {
        /// <summary>
        /// The check end condition setup.
        /// </summary>
        [Test]
        public void CheckEndConditionSetup()
        {
            var mockGameState = MockHelper.CreateGameState();
            var mockCondition = new Mock<ICondition>();

            // check constructor parameter passing
            Assert.AreEqual(GameResult.FinishedWithoutWinner, new EndCondition(mockCondition.Object, GameResult.FinishedWithoutWinner).Result);
            Assert.AreEqual(GameResult.FinshedWithWinner, new EndCondition(mockCondition.Object, GameResult.FinshedWithWinner).Result);
            Assert.AreEqual(mockCondition.Object, new EndCondition(mockCondition.Object, GameResult.FinshedWithWinner).Condition);

            // check if state of end conditon is same as state of its condition
            var endCondition = new EndCondition(mockCondition.Object, GameResult.FinshedWithWinner);
            
            mockCondition.Setup(condition => condition.Check(mockGameState.Object)).Returns(true);
            Assert.IsTrue(endCondition.Check(mockGameState.Object));
            
            mockCondition.Setup(condition => condition.Check(mockGameState.Object)).Returns(false);
            Assert.IsFalse(endCondition.Check(mockGameState.Object));
        }
    }
}