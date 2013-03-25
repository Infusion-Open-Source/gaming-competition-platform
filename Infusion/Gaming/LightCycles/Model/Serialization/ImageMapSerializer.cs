namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// Provides serialization for the map in image format
    /// </summary>
    public class ImageMapSerializer : IMapSerializer
    {
        /// <summary>
        /// Predefined colors for the teams
        /// </summary>
        private Color[] teamColors = new[] 
        {
            Color.Red,
            Color.Blue,
            Color.Yellow,
            Color.YellowGreen,
            Color.Green,
            Color.Violet,
            Color.Wheat,
            Color.Tomato,
            Color.Aqua,
            Color.Coral
        };

        /// <summary>
        /// internal pixel buffer for map data
        /// </summary>
        private Color[,] buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMapSerializer"/> class.
        /// </summary>
        /// <param name="fileName">name of the file with map</param>
        public ImageMapSerializer(string fileName)
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
            this.buffer = new Color[width, height];
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
            char nextPlayer = 'A';
            char nextTeam = 'A';
            Dictionary<Color, char> teams = new Dictionary<Color, char>();
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Width; y++)
                {
                    Color c = this.buffer[x, y];
                    if (c == Color.Black)
                    {
                        data[x, y] = new Obstacle();
                    }
                    else if (c == Color.White)
                    {
                        data[x, y] = new Space();
                    }
                    else
                    {
                        char player = nextPlayer++;
                        char team = nextTeam;
                        if (teams.ContainsKey(c))
                        {
                            team = teams[c];
                        }
                        else
                        {
                            teams.Add(c, nextTeam++);
                        }

                        data[x, y] = new PlayersStartingLocation(new Player(player, new Team(team)));
                    }
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

            int nextTeamColor = 0;
            Dictionary<char, Color> teams = new Dictionary<char, Color>();
            this.Create(map.Width, map.Height);
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Width; y++)
                {
                    var location = map[x, y];
                    if (location is Obstacle)
                    {
                        this.buffer[x, y] = Color.Black;
                    }
                    else if (location is Space)
                    {
                        this.buffer[x, y] = Color.White;
                    }
                    else if (location is PlayersStartingLocation)
                    {
                        PlayersStartingLocation playerStartingLocation = (PlayersStartingLocation)location;
                        if (!teams.ContainsKey(playerStartingLocation.Player.Team.Id))
                        {
                            teams.Add(playerStartingLocation.Player.Team.Id, this.teamColors[nextTeamColor++]);
                        }

                        this.buffer[x, y] = this.teamColors[playerStartingLocation.Player.Team.Id];
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
            }
        }

        /// <summary>
        /// Load data from the source into the buffer
        /// </summary>
        public void Load()
        {
            using (Bitmap image = new Bitmap(this.FileName))
            {
                this.Create(image.Width, image.Height);
                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Width; y++)
                    {
                        this.buffer[x, y] = image.GetPixel(x, y);
                    }
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

            using (Bitmap image = new Bitmap(this.Width, this.Height))
            {
                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Width; y++)
                    {
                        image.SetPixel(x, y, this.buffer[x, y]);
                    }
                }

                image.Save(this.FileName, ImageFormat.Png);
            }
        }        
    }
}
