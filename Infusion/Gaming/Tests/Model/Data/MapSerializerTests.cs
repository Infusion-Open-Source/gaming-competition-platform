
namespace Infusion.Gaming.LightCycles.Tests.Model.Data
{
    using System;
    using System.Text;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

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