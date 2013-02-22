// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapSerializerTests.cs" company="Infusion">
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
//   The map serializer tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Tests.Model.Data
{
    using System;
    using System.Text;

    using Infusion.Gaming.LightCycles.Model.Data;

    using NUnit.Framework;

    /// <summary>
    ///     The map serializer tests.
    /// </summary>
    [TestFixture]
    public class MapSerializerTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The empty map read write check.
        /// </summary>
        [Test]
        public void EmptyMapReadWriteCheck()
        {
            string input = string.Empty;
            var serializer = new MapSerializer();
            Assert.Throws<ArgumentNullException>(() => serializer.Read(input));
        }

        /// <summary>
        ///     The simple map read write check.
        /// </summary>
        [Test]
        public void SimpleMapReadWriteCheck()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("#####");
            stringBuilder.AppendLine("# A #");
            stringBuilder.AppendLine("# a #");
            stringBuilder.AppendLine("#####");

            string input = stringBuilder.ToString();
            var serializer = new MapSerializer();
            string output = serializer.Write(serializer.Read(input));
            Assert.AreEqual(input, output);
        }

        /// <summary>
        ///     The simple map write read check.
        /// </summary>
        [Test]
        public void SimpleMapWriteReadCheck()
        {
            var map = new Map(4, 4);
            var player = new Player('A');
            map.Locations[2, 2] = new Location(LocationTypeEnum.Trail, player);
            map.Locations[2, 3] = new Location(LocationTypeEnum.Player, player);

            var serializer = new MapSerializer();
            IMap outMap = serializer.Read(serializer.Write(map));

            Assert.AreEqual(map, outMap);
        }

        #endregion
    }
}