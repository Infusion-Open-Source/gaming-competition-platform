using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Infusion.Gaming.LightCycles.Model.Defines;

namespace Infusion.Gaming.LightCycles.Model.Data
{
    /// <summary>
    /// players data map
    /// </summary>
    public class PlayersData : IPlayersData
    {
        /// <summary>
        ///     Gets or sets the height of the players data map.
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        ///     Gets or sets the width of the players data map.
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        ///     Gets or sets the players data.
        /// </summary>
        public LocationData[,] Data { get; protected set; }

        /// <summary>
        /// Get location data for specified loaction
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>location data at specified point</returns>
        public LocationData this[int x, int y]
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
        ///     Gets the teams.
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
                        if (this.Data[x, y].PlayerDataType == PlayerDataTypeEnum.Player)
                        {
                            Team team = this.Data[x, y].Player.Team;
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
        ///     Gets the players.
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
                        if (this.Data[x, y].PlayerDataType == PlayerDataTypeEnum.Player)
                        {
                            results.Add(this.Data[x, y].Player);
                        }
                    }
                }

                return results;
            }
        }

        /// <summary>
        ///     Gets the players locations.
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
                        if(this.Data[x, y].PlayerDataType == PlayerDataTypeEnum.Player)
                        {
                            results.Add(this.Data[x,y].Player, new Point(x, y));
                        }
                    }
                }

                return results;
            }
        }

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
            this.Data = new LocationData[this.Width, this.Height];
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if(map[x,y].LocationType == LocationTypeEnum.PlayersStartingLocation)
                    {
                        var startLocation = (PlayersStartingLocation)map[x, y];
                        if (playersInGame.Contains(startLocation.Player))
                        {
                            this.Data[x, y] = new LocationData(startLocation.Player, PlayerDataTypeEnum.Player);
                            continue;
                        }
                    }
                    
                    this.Data[x, y] = new LocationData();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersData"/> class.
        /// </summary>
        /// <param name="data">
        /// Players data map to be clonned
        /// </param>
        public PlayersData(IPlayersData data)
        {
            this.Width = data.Width;
            this.Height = data.Height;
            this.Data = new LocationData[this.Width, this.Height];
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    this.Data[x, y] = new LocationData(data[x, y]);
                }
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
                    LocationData location = this.Data[x, y];
                    if (player.Equals(location.Player))
                    {
                        this.Data[x, y] = new LocationData();
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
