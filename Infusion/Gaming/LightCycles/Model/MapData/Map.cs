namespace Infusion.Gaming.LightCycles.Model.MapData
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

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
        /// <exception cref="ArgumentOutOfRangeException">
        /// Height and/or width are too small. Must be at least zero
        /// </exception>
        public Map(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 0)
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