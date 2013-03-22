
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
            /*
            TODO: to be fixed 
             
            Mock<IGame> mockGame = MockHelper.CreateGame(2);
            var mockCondition = new Mock<ICondition>();

            Assert.AreEqual(
                GameResultEnum.FinishedWithoutWinner, 
                new EndCondition(mockCondition.Object, GameResultEnum.FinishedWithoutWinner).Result);
            Assert.AreEqual(
                GameResultEnum.FinshedWithWinner, 
                new EndCondition(mockCondition.Object, GameResultEnum.FinshedWithWinner).Result);
            Assert.AreEqual(
                mockCondition.Object, new EndCondition(mockCondition.Object, GameResultEnum.FinshedWithWinner).Condition);

            var endCondition = new EndCondition(mockCondition.Object, GameResultEnum.FinshedWithWinner);
            mockCondition.Setup(condition => condition.Check(mockGame.Object)).Returns(true);
            Assert.IsTrue(endCondition.Check(mockGame.Object));
            mockCondition.Setup(condition => condition.Check(mockGame.Object)).Returns(false);
            Assert.IsFalse(endCondition.Check(mockGame.Object));
            */
        }

        #endregion
    }
}