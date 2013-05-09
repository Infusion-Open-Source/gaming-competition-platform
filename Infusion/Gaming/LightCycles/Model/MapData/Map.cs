namespace Infusion.Gaming.LightCycles.Model.MapData
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCyclesCommon.Definitions;

    /// <summary>
    /// The game map.
    /// </summary>
    public class Map : IMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="width">
        /// The width of the map.
        /// </param>
        /// <param name="height">
        /// The height of the map.
        /// </param>
        public Map(int width, int height)
        {
            if (width < 0 || width > Constraints.MaxMapWidth)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 0 || height > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            this.Width = width;
            this.Height = height;
            this.Locations = new Location[this.Width, this.Height];
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (x == 0 || y == 0 || x + 1 == this.Width || y + 1 == this.Height)
                    {
                        this.Locations[x, y] = new Obstacle();
                    }
                    else
                    {
                        this.Locations[x, y] = new Space();
                    }
                }
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="locations">
        /// The locations with the map data.
        /// </param>
        public Map(Location[,] locations)
        {
            if (locations == null)
            {
                throw new ArgumentNullException("locations");
            }

            this.Width = locations.GetLength(0);
            this.Height = locations.GetLength(1);
            this.Locations = new Location[this.Width, this.Height];
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    this.Locations[x, y] = locations[x, y];
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the map.
        /// </summary>
        public int Height { get; protected set; }
        
        /// <summary>
        /// Gets or sets the map locations.
        /// </summary>
        public Location[,] Locations { get; protected set; }
        
        /// <summary>
        /// Gets or sets the width of the map.
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets the players starting locations.
        /// </summary>
        public Dictionary<PlayersStartingLocation, Point> StartingLocations
        {
            get
            {
                Dictionary<PlayersStartingLocation, Point> results = new Dictionary<PlayersStartingLocation, Point>();
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        var location = this.Locations[x, y] as PlayersStartingLocation;
                        if (location != null)
                        {
                            results.Add(location, new Point(x, y));
                        }
                    }
                }

                return results;
            }
        }

        /// <summary>
        /// Get location data for specified location
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>location data at specified point</returns>
        public Location this[int x, int y]
        {
            get
            {
                return this.Locations[x, y];
            }
        }
    }
}