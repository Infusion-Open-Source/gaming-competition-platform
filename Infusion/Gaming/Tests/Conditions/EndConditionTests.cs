
namespace Infusion.Gaming.LightCycles.Tests.Conditions
{
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Defines;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    ///     The end condition tests.
    /// </summary>
    [TestFixture]
    public class EndConditionTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The check end condition setup.
        /// </summary>
        [Test]
        public void CheckEndConditionSetup()
        {
            var mockGameState = MockHelper.CreateGameState();
            var mockCondition = new Mock<ICondition>();

            // check constructor parameter passing
            Assert.AreEqual(GameResultEnum.FinishedWithoutWinner, new EndCondition(mockCondition.Object, GameResultEnum.FinishedWithoutWinner).Result);
            Assert.AreEqual(GameResultEnum.FinshedWithWinner, new EndCondition(mockCondition.Object, GameResultEnum.FinshedWithWinner).Result);
            Assert.AreEqual(mockCondition.Object, new EndCondition(mockCondition.Object, GameResultEnum.FinshedWithWinner).Condition);

            // check if state of end conditon is same as state of its condition
            var endCondition = new EndCondition(mockCondition.Object, GameResultEnum.FinshedWithWinner);
            
            mockCondition.Setup(condition => condition.Check(mockGameState.Object)).Returns(true);
            Assert.IsTrue(endCondition.Check(mockGameState.Object));
            
            mockCondition.Setup(condition => condition.Check(mockGameState.Object)).Returns(false);
            Assert.IsFalse(endCondition.Check(mockGameState.Object));
        }

        #endregion
    }
}