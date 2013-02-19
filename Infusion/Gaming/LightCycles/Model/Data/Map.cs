namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The game map.
    /// </summary>
    public class Map : IMap
    {
        #region Constructors and Destructors

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
                        this.Locations[x, y] = new Location(LocationTypeEnum.Wall);
                    }
                    else
                    {
                        this.Locations[x, y] = new Location(LocationTypeEnum.Space);
                    }
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the height of the map.
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// Gets or sets the map locations.
        /// </summary>
        public Location[,] Locations { get; protected set; }

        /// <summary>
        /// Gets the players locations.
        /// </summary>
        public Dictionary<Player, Point> PlayerLocations
        {
            get
            {
                var playerLocations = new Dictionary<Player, Point>();
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        Location location = this.Locations[x, y];
                        if (location.LocationType == LocationTypeEnum.Player)
                        {
                            playerLocations.Add(location.Player, new Point(x, y));
                        }
                    }
                }

                return playerLocations;
            }
        }

        /// <summary>
        /// Gets the players.
        /// </summary>
        public List<Player> Players
        {
            get
            {
                var players = new List<Player>();
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        Location location = this.Locations[x, y];
                        if (location.LocationType == LocationTypeEnum.Player)
                        {
                            players.Add(location.Player);
                        }
                    }
                }

                return players;
            }
        }

        /// <summary>
        /// Gets or sets the width of the map.
        /// </summary>
        public int Width { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Clone the map.
        /// </summary>
        /// <returns>
        /// The cloned map <see cref="IMap"/>.
        /// </returns>
        public IMap Clone()
        {
            var serializer = new MapSerializer();
            return serializer.Read(serializer.Write(this));
        }

        /// <summary>
        /// Check if equals.
        /// </summary>
        /// <param name="obj">
        /// The object to compare to.
        /// </param>
        /// <returns>
        /// The result of comparison.
        /// </returns>
        public override bool Equals(object obj)
        {
            var objMap = obj as Map;
            if (objMap != null)
            {
                if (obj.GetHashCode() != this.GetHashCode())
                {
                    return false;
                }

                if (this.Width != objMap.Width)
                {
                    return false;
                }

                if (this.Height != objMap.Height)
                {
                    return false;
                }

                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        if (!this.Locations[x, y].Equals(objMap.Locations[x, y]))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>
        /// The hash code.
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 1;
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    hash += (x * y) + this.Locations[x, y].GetHashCode();
                }
            }

            return hash;
        }

        /// <summary>
        /// Gets zero state of the map. Creates T-1 map from initial map which is helpful to find out players initial directions.
        /// </summary>
        /// <returns>
        /// The cloned map <see cref="IMap"/>.
        /// </returns>
        public IMap GetZeroStateMap()
        {
            var serializer = new MapSerializer();
            char[] charBuffer = serializer.Write(this).ToCharArray();
            for (int i = 0; i < charBuffer.Length; i++)
            {
                char c = charBuffer[i];
                if (c >= 'A' && c <= 'Z')
                {
                    charBuffer[i] = ' ';
                }

                if (c >= 'a' && c <= 'z')
                {
                    charBuffer[i] = charBuffer[i].ToUpper();
                }
            }

            return serializer.Read(new string(charBuffer));
        }

        /// <summary>
        /// Removes specified player from the map.
        /// </summary>
        /// <param name="player">
        /// The player to be removed.
        /// </param>
        public void RemovePlayer(Player player)
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    Location location = this.Locations[x, y];
                    if (location.Player != null && location.Player.Equals(player))
                    {
                        this.Locations[x, y] = new Location(LocationTypeEnum.Space);
                    }
                }
            }
        }

        /// <summary>
        /// Removes specified players from the map.
        /// </summary>
        /// <param name="playersToRemove">
        /// The players to be removed.
        /// </param>
        public void RemovePlayers(IEnumerable<Player> playersToRemove)
        {
            foreach (Player player in playersToRemove)
            {
                this.RemovePlayer(player);
            }
        }

        #endregion
    }
}