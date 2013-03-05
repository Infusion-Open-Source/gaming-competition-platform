// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsTests.cs" company="Infusion">
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
//   The events tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Tests.Events
{
    using System;

    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    using NUnit.Framework;

    /// <summary>
    ///     The events tests.
    /// </summary>
    [TestFixture]
    public class EventsTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The player collision event checks.
        /// </summary>
        [Test]
        public void PlayerCollisionEventChecks()
        {
            var player = new Player('A');
            Assert.AreEqual(player, new PlayerCollisionEvent(player).Player);
        }

        /// <summary>
        ///     The player move event checks.
        /// </summary>
        [Test]
        public void PlayerMoveEventChecks()
        {
            var player = new Player('A');
            Assert.AreEqual(player, new PlayerMoveEvent(player, RelativeDirectionEnum.Left).Player);
            Assert.AreEqual(
                RelativeDirectionEnum.Left, new PlayerMoveEvent(player, RelativeDirectionEnum.Left).Direction);
            Assert.AreEqual(
                RelativeDirectionEnum.Right, new PlayerMoveEvent(player, RelativeDirectionEnum.Right).Direction);
            Assert.AreEqual(
                RelativeDirectionEnum.StraightForward, 
                new PlayerMoveEvent(player, RelativeDirectionEnum.StraightForward).Direction);
            Assert.AreEqual(
                RelativeDirectionEnum.Undefined, new PlayerMoveEvent(player, RelativeDirectionEnum.Undefined).Direction);
        }

        /// <summary>
        ///     The tick event checks.
        /// </summary>
        [Test]
        public void TickEventChecks()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TickEvent(-1));
            Assert.AreEqual(0, new TickEvent(0).Turn);
            Assert.AreEqual(3, new TickEvent(3).Turn);
            Assert.AreEqual(6, new TickEvent(6).Turn);
            Assert.AreEqual(12, new TickEvent(12).Turn);
        }

        #endregion
    }
}