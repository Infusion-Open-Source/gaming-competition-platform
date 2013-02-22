// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberOfPlayersTests.cs" company="Infusion">
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
//   The number of players tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
        ///     The exact number of players checks.
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