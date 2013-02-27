// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharExtensionsTests.cs" company="Infusion">
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
//   The char extensions tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Infusion.Gaming.LightCycles.Extensions;

namespace Infusion.Gaming.LightCycles.Tests.Model.Data
{
    using Infusion.Gaming.LightCycles.Model.Data;

    using NUnit.Framework;

    /// <summary>
    ///     The char extensions tests.
    /// </summary>
    [TestFixture]
    public class CharExtensionsTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     To lower checks.
        /// </summary>
        [Test]
        public void ToLowerChecks()
        {
            Assert.AreEqual('a', 'a'.ToLower());
            Assert.AreEqual('a', 'A'.ToLower());

            Assert.AreEqual('z', 'z'.ToLower());
            Assert.AreEqual('z', 'Z'.ToLower());

            Assert.AreEqual('1', '1'.ToLower());
            Assert.AreEqual('!', '!'.ToLower());
        }

        /// <summary>
        ///     To upper checks.
        /// </summary>
        [Test]
        public void ToUpperChecks()
        {
            Assert.AreEqual('A', 'a'.ToUpper());
            Assert.AreEqual('A', 'A'.ToUpper());

            Assert.AreEqual('Z', 'z'.ToUpper());
            Assert.AreEqual('Z', 'Z'.ToUpper());

            Assert.AreEqual('1', '1'.ToUpper());
            Assert.AreEqual('!', '!'.ToUpper());
        }

        #endregion
    }
}