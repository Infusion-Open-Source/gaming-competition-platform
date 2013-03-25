namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// players data map
    /// </summary>
    public class PlayersData : IPlayersData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersData"/> class.
        /// </summary>
        /// <param name="map">
        /// The map for which player data map should be created.
        /// </param>
        /// <param name="players">
        /// The set of players in a game.
        /// </param>
        public PlayersData(IMap map, IEnumerable<Player> players)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            var playersInGame = new List<Player>(players);
            this.Width = map.Width;
            this.Height = map.Height;
            this.Data = new GameObject[this.Width, this.Height];
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    this.Data[x, y] = null;
                    var startLocation = map[x, y] as PlayersStartingLocation;
                    if (startLocation != null && playersInGame.Contains(startLocation.Player))
                    {
                        this.Data[x, y] = new LightCycleBike(startLocation.Player, DirectionHelper.RandomDirection());
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersData"/> class.
        /// </summary>
        /// <param name="data">
        /// Players data map to be cloned
        /// </param>
        public PlayersData(IPlayersData data)
        {
            this.Width = data.Width;
            this.Height = data.Height;
            this.Data = new GameObject[this.Width, this.Height];
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    this.Data[x, y] = data[x, y] != null ? data[x, y].Clone() : null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the players data map.
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// Gets or sets the width of the players data map.
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets or sets the players data.
        /// </summary>
        public GameObject[,] Data { get; protected set; }

        /// <summary>
        /// Gets the teams.
        /// </summary>
        public List<Team> Teams 
        {
            get
            {
                List<Team> results = new List<Team>();
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        LightCycleBike obj = this.Data[x, y] as LightCycleBike;
                        if (obj != null)
                        {
                            Team team = obj.Player.Team;
                            if (!results.Contains(team))
                            {
                                results.Add(team);
                            }
                        }
                    }
                }

                return results;
            } 
        }

        /// <summary>
        /// Gets the players.
        /// </summary>
        public List<Player> Players
        {
            get
            {
                List<Player> results = new List<Player>();
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        LightCycleBike obj = this.Data[x, y] as LightCycleBike;
                        if (obj != null)
                        {
                            results.Add(obj.Player);
                        }
                    }
                }

                return results;
            }
        }

        /// <summary>
        /// Gets the players light cycles.
        /// </summary>
        public Dictionary<Player, LightCycleBike> PlayersLightCycles
        {
            get
            {
                Dictionary<Player, LightCycleBike> results = new Dictionary<Player, LightCycleBike>();
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        LightCycleBike obj = this.Data[x, y] as LightCycleBike;
                        if (obj != null)
                        {
                            results.Add(obj.Player, obj);
                        }
                    }
                }

                return results;
            }
        }

        /// <summary>
        /// Gets the players locations.
        /// </summary>
        public Dictionary<Player, Point> PlayersLocations
        {
            get
            {
                Dictionary<Player, Point> results = new Dictionary<Player, Point>();
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        LightCycleBike obj = this.Data[x, y] as LightCycleBike;
                        if (obj != null)
                        {
                            results.Add(obj.Player, new Point(x, y));
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
        public GameObject this[int x, int y]
        {
            get
            {
                return this.Data[x, y];
            }

            set
            {
                this.Data[x, y] = value;
            }
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
                    PlayerGameObject obj = this.Data[x, y] as PlayerGameObject;
                    if (obj != null && obj.Player.Equals(player))
                    {
                        this.Data[x, y] = null;
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
    }
}
