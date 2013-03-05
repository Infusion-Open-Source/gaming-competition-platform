// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndConditionTests.cs" company="Infusion">
//    Copyright (C) 2013 Paweł Drozdowski
//
//    This file is part of LightCycles Game Engine.
//
//    LightCycles Game Engine is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    LightCycles Game Engine is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with LightCycles Game Engine.  If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
//   The end condition tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
        }

        #endregion
    }
}