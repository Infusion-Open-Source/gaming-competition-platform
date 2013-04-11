namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// The map serializer.
    /// </summary>
    public class PlainTextMapSerializer : IMapSerializer
    {
        /// <summary>
        /// internal char buffer for map data
        /// </summary>
        private char[,] buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextMapSerializer"/> class.
        /// </summary>
        /// <param name="fileName">name of the file with map</param>
        public PlainTextMapSerializer(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            this.FileName = fileName;
        }

        /// <summary>
        /// Gets or sets file name
        /// </summary>
        public string FileName { get; protected set; }

        /// <summary>
        /// Gets or sets current map buffer width
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets or sets current map buffer height
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// Create map buffer
        /// </summary>
        /// <param name="width">width of the buffer</param>
        /// <param name="height">height of the buffer</param>
        public void Create(int width, int height)
        {
            if (width <= 0 || width > 10000)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height <= 0 || height > 10000)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            this.Width = width;
            this.Height = height;
            this.buffer = new char[width, height];
        }

        /// <summary>
        /// Read entire map from buffer
        /// </summary>
        /// <returns>map read form buffer</returns>
        public IMap Read()
        {
            if (this.buffer == null)
            {
                throw new InvalidOperationException("Buffer has not been initialized");
            }

            Location[,] data = new Location[this.Width, this.Height];
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    data[x, y] = this.Read(x, y);
                }
            }

            return new Map(data);
        }

        /// <summary>
        /// Write entire map to buffer
        /// </summary>
        /// <param name="map">map to be written</param>
        public void Write(IMap map)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            this.Create(map.Width, map.Height);
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    this.Write(x, y, map[x, y]);
                }
            }
        }

        /// <summary>
        /// Load data from the source into the buffer
        /// </summary>
        public void Load()
        {
            int height;
            int width;
            string text = File.ReadAllText(this.FileName);
            string[] lines = this.PreprocessMapStream(text);
            this.GetMapStreamDimensions(lines, out width, out height);
            this.Create(width, height);
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    this.buffer[x, y] = lines[y][x];
                }
            }
        }

        /// <summary>
        /// Save data from the buffer to the source
        /// </summary>
        public void Save()
        {
            if (this.buffer == null)
            {
                throw new InvalidOperationException("Buffer has not been initialized");
            }

            using (StreamWriter writer = new StreamWriter(this.FileName, false))
            {
                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Height; y++)
                    {
                        writer.Write(this.buffer[x, y]);
                    }

                    writer.WriteLine();
                }

                writer.Flush();
                writer.Close();
            }
        }

        /// <summary>
        /// Read map location on given coordinates from buffer
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>location under coordinates</returns>
        private Location Read(int x, int y)
        {
            if (x <= 0 || x >= this.Width)
            {
                throw new ArgumentOutOfRangeException("x");
            }

            if (y <= 0 || y >= this.Height)
            {
                throw new ArgumentOutOfRangeException("y");
            }

            char c = this.buffer[x, y];
            if (c == '#')
            {
                return new Obstacle();
            }

            if (c == ' ')
            {
                return new Space();
            }

            if (c >= 'A' && c <= 'Z')
            {
                return new PlayersStartingLocation(new Player(c, new Team(c)));
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Writes location into map buffer at given coordinates
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="location">location to be written</param>
        private void Write(int x, int y, Location location)
        {
            if (x <= 0 || x >= this.Width)
            {
                throw new ArgumentOutOfRangeException("x");
            }

            if (y <= 0 || y >= this.Height)
            {
                throw new ArgumentOutOfRangeException("y");
            }

            var playersStartingLocation = location as PlayersStartingLocation;
            if (playersStartingLocation != null)
            {
                this.buffer[x, y] = playersStartingLocation.Player.Id;
            }
            else if (location is Obstacle)
            {
                this.buffer[x, y] = '#';
            }
            else if (location is Space)
            {
                this.buffer[x, y] = ' ';
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets map dimensions from the text.
        /// </summary>
        /// <param name="lines">The lines with map rows.</param>
        /// <param name="width">Outputs the width.</param>
        /// <param name="height">Outputs the height.</param>
        private void GetMapStreamDimensions(string[] lines, out int width, out int height)
        {
            height = lines.Length;
            width = 0;
            foreach (string line in lines)
            {
                if (line.Length > width)
                {
                    width = line.Length;
                }
            }
        }

        /// <summary>
        /// Preprocess map text, does a cleanup and splits by map rows.
        /// </summary>
        /// <param name="text">The text to preprocess. </param>
        /// <returns>The set of map row strings.</returns>
        private string[] PreprocessMapStream(string text)
        {
            var lines = new List<string>(text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            for (int i = 0; i < lines.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    lines.RemoveAt(i--);
                }
            }

            return lines.ToArray();
        }
    }
}