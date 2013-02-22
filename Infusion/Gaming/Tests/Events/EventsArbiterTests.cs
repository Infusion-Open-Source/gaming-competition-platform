// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsArbiterTests.cs" company="Infusion">
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
//   The events arbiter tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Tests.Events
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    using NUnit.Framework;

    /// <summary>
    ///     The events arbiter tests.
    /// </summary>
    [TestFixture]
    public class EventsArbiterTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Arbitrage checks.
        /// </summary>
        [Test]
        public void ArbitrageChecks()
        {
            var playersInGame = new List<Player> { new Player('A'), new Player('B') };
            var events = new EventsCollection
                             {
                                 new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Left), 
                                 new PlayerMoveEvent(new Player('B'), RelativeDirectionEnum.Left), 
                                 new PlayerMoveEvent(new Player('A'), RelativeDirectionEnum.Right), 
                                 new PlayerMoveEvent(new Player('B'), RelativeDirectionEnum.Right), 
                                 new PlayerMoveEvent(new Player('C'), RelativeDirectionEnum.Right), 
                                 new PlayerMoveEvent(
                                     new Player('B'), RelativeDirectionEnum.StraightForward)
                             };

            var arbiter = new EventsArbiter();
            EventsCollection ouputEvents = arbiter.Arbitrage(events, playersInGame);

            Assert.AreEqual(2, ouputEvents.Count);

            Assert.AreEqual('A', ((PlayerMoveEvent)ouputEvents[0]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Right, ((PlayerMoveEvent)ouputEvents[0]).Direction);

            Assert.AreEqual('B', ((PlayerMoveEvent)ouputEvents[1]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.StraightForward, ((PlayerMoveEvent)ouputEvents[1]).Direction);
        }

        /// <summary>
        ///     Arbitrage inputs checks.
        /// </summary>
        [Test]
        public void ArbitrageInputsChecks()
        {
            var playersInGame = new List<Player> { new Player('A'), new Player('B') };
            var events = new EventsCollection();

            var arbiter = new EventsArbiter();

            Assert.Throws<ArgumentNullException>(() => arbiter.Arbitrage(events, null));
            Assert.Throws<ArgumentNullException>(() => arbiter.Arbitrage(null, playersInGame));
        }

        /// <summary>
        ///     Arbitrage on empty list makes default behavior.
        /// </summary>
        [Test]
        public void ArbitrageOnEmptyListMakesDefaultBehavior()
        {
            var playersInGame = new List<Player> { new Player('A'), new Player('B') };
            var events = new EventsCollection();

            var arbiter = new EventsArbiter();
            EventsCollection ouputEvents = arbiter.Arbitrage(events, playersInGame);

            Assert.AreEqual(2, ouputEvents.Count);

            Assert.AreEqual('A', ((PlayerMoveEvent)ouputEvents[0]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Undefined, ((PlayerMoveEvent)ouputEvents[0]).Direction);

            Assert.AreEqual('B', ((PlayerMoveEvent)ouputEvents[1]).Player.Id);
            Assert.AreEqual(RelativeDirectionEnum.Undefined, ((PlayerMoveEvent)ouputEvents[1]).Direction);
        }

        #endregion
    }
}