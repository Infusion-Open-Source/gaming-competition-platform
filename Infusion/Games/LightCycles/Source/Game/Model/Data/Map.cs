namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Model.Data.MapObjects;
    using Infusion.Gaming.LightCycles.Util;

    /// <summary>
    /// game map 2D space
    /// </summary>
    public class Map : Array2D<MapLocation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="name">map name. </param>
        /// <param name="width"> The width of the map. </param>
        /// <param name="height"> The height of the map. </param>
        public Map(string name, int width, int height)
            : base(width, height)
        {
            this.Name = name;
            if (width < Constraints.MinMapWidth || width > Constraints.MaxMapWidth)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < Constraints.MinMapHeight || height > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            // fill entrie map with space
            this.Fill((location) => new Space(location));

            // put obstacles on map border
            this.Fill(
                (location, obj) => (location.X == 0 || location.Y == 0 || location.X + 1 == this.Width || location.Y + 1 == this.Height),
                (location) => new Obstacle(location));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="name">map name.</param>
        /// <param name="data">Initializing data.</param>
        public Map(string name, MapLocation[,] data)
            : base(data)
        {
            this.Name = name;
            int width = data.GetLength(0);
            int height = data.GetLength(1);
            if (width < Constraints.MinMapWidth || width > Constraints.MaxMapWidth)
            {
                throw new ArgumentOutOfRangeException("data");
            }

            if (height < Constraints.MinMapHeight || height > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException("data");
            }
        }

        /// <summary>
        /// Gets or sets map name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets players start locations
        /// </summary>
        public List<PlayersStartingLocation> StartLocations
        {
            get
            {
                return this.FindObjects<PlayersStartingLocation>();
            }
        }

        /// <summary>
        /// Gets obstacles
        /// </summary>
        public List<Obstacle> Obstacles
        {
            get
            {
                return this.FindObjects<Obstacle>();
            }
        }

        /// <summary>
        /// Get player start location
        /// </summary>
        /// <param name="playerIdentity">player identity</param>
        /// <returns>start location</returns>
        public PlayersStartingLocation GetPlayerStartLocation(Identity playerIdentity)
        {
            foreach (PlayersStartingLocation startLocation in this.FindObjects<PlayersStartingLocation>())
            {
                if (startLocation.Player.Equals(playerIdentity))
                {
                    return startLocation;
                }
            }

            return null;
        }
    }
}
