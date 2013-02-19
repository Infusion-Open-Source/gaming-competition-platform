namespace Infusion.Gaming.LightCycles.Tests.Model.Data
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    using NUnit.Framework;

    /// <summary>
    /// The player direction resolver tests.
    /// </summary>
    [TestFixture]
    public class PlayerDirectionResolverTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// resolve fails on incompatible maps.
        /// </summary>
        [Test]
        public void ResolveFailsOnIncompatibleMaps()
        {
            var playerA = new Player('A');
            var map1 = new Map(3, 3);
            map1.Locations[1, 2] = new Location(LocationTypeEnum.Player, playerA);
            map1.Locations[1, 1] = new Location(LocationTypeEnum.Trail, playerA);
            var map2 = new Map(3, 3);

            var resolver = new PlayerDirectionResolver();
            Assert.Throws<ArgumentException>(() => resolver.Resolve(map1, map2));
        }

        /// <summary>
        /// Resolve for player going down checks.
        /// </summary>
        [Test]
        public void ResolveForPlayerGoingDownChecks()
        {
            var playerA = new Player('A');
            var map1 = new Map(3, 3);
            map1.Locations[1, 2] = new Location(LocationTypeEnum.Player, playerA);
            map1.Locations[1, 1] = new Location(LocationTypeEnum.Trail, playerA);
            var map2 = new Map(3, 3);
            map2.Locations[1, 1] = new Location(LocationTypeEnum.Player, playerA);

            var resolver = new PlayerDirectionResolver();
            Dictionary<Player, DirectionEnum> directions = resolver.Resolve(map1, map2);

            Assert.IsNotNull(directions);
            Assert.AreEqual(1, directions.Count);
            Assert.AreEqual(DirectionEnum.Down, directions[playerA]);
        }

        /// <summary>
        /// Resolve for player going left checks.
        /// </summary>
        [Test]
        public void ResolveForPlayerGoingLeftChecks()
        {
            var playerA = new Player('A');
            var map1 = new Map(3, 3);
            map1.Locations[0, 1] = new Location(LocationTypeEnum.Player, playerA);
            map1.Locations[1, 1] = new Location(LocationTypeEnum.Trail, playerA);
            var map2 = new Map(3, 3);
            map2.Locations[1, 1] = new Location(LocationTypeEnum.Player, playerA);

            var resolver = new PlayerDirectionResolver();
            Dictionary<Player, DirectionEnum> directions = resolver.Resolve(map1, map2);

            Assert.IsNotNull(directions);
            Assert.AreEqual(1, directions.Count);
            Assert.AreEqual(DirectionEnum.Left, directions[playerA]);
        }

        /// <summary>
        /// Resolve for player going right checks.
        /// </summary>
        [Test]
        public void ResolveForPlayerGoingRightChecks()
        {
            var playerA = new Player('A');
            var map1 = new Map(3, 3);
            map1.Locations[2, 1] = new Location(LocationTypeEnum.Player, playerA);
            map1.Locations[1, 1] = new Location(LocationTypeEnum.Trail, playerA);
            var map2 = new Map(3, 3);
            map2.Locations[1, 1] = new Location(LocationTypeEnum.Player, playerA);

            var resolver = new PlayerDirectionResolver();
            Dictionary<Player, DirectionEnum> directions = resolver.Resolve(map1, map2);

            Assert.IsNotNull(directions);
            Assert.AreEqual(1, directions.Count);
            Assert.AreEqual(DirectionEnum.Right, directions[playerA]);
        }

        /// <summary>
        /// Resolve for player going up checks.
        /// </summary>
        [Test]
        public void ResolveForPlayerGoingUpChecks()
        {
            var playerA = new Player('A');
            var map1 = new Map(3, 3);
            map1.Locations[1, 0] = new Location(LocationTypeEnum.Player, playerA);
            map1.Locations[1, 1] = new Location(LocationTypeEnum.Trail, playerA);
            var map2 = new Map(3, 3);
            map2.Locations[1, 1] = new Location(LocationTypeEnum.Player, playerA);

            var resolver = new PlayerDirectionResolver();
            Dictionary<Player, DirectionEnum> directions = resolver.Resolve(map1, map2);

            Assert.IsNotNull(directions);
            Assert.AreEqual(1, directions.Count);
            Assert.AreEqual(DirectionEnum.Up, directions[playerA]);
        }

        /// <summary>
        /// Resolve with no results after players death.
        /// </summary>
        [Test]
        public void ResolveNoResultsAfterPlayersDeath()
        {
            var playerA = new Player('A');
            var map1 = new Map(3, 3);
            var map2 = new Map(3, 3);
            map2.Locations[1, 1] = new Location(LocationTypeEnum.Player, playerA);

            var resolver = new PlayerDirectionResolver();
            Dictionary<Player, DirectionEnum> directions = resolver.Resolve(map1, map2);

            Assert.IsNotNull(directions);
            Assert.AreEqual(0, directions.Count);
        }

        #endregion
    }
}