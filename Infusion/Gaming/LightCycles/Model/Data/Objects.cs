using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Model.Data.GameObjects;
using Infusion.Gaming.LightCycles.Model.Data.MapObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using Infusion.Gaming.LightCycles.Events;
using Infusion.Gaming.LightCycles.Util;

namespace Infusion.Gaming.LightCycles.Model.Data
{
    /// <summary>
    /// game objects 2D space
    /// </summary>
    public class Objects : Array2D<GameObject>
    {
        private IGameSlotsPool playersPool;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="width"> The width of the map. </param>
        /// <param name="height"> The height of the map. </param>
        /// <param name="pool"> Players pool in the game. </param>
        public Objects(int width, int height, IGameSlotsPool pool)
            : base(width, height)
        {
            if (width < Constraints.MinMapWidth || width > Constraints.MaxMapWidth)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < Constraints.MinMapHeight || height > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            if (pool == null)
            {
                throw new ArgumentNullException("pool");
            }

            this.playersPool = pool;
            this.Fill((location) => null);
        }
        
        /// <summary>
        /// Initializes a new instance of the Array2D class with initial data set.
        /// </summary>
        /// <param name="data">Initializing data. </param>
        /// <param name="pool"> Players pool in the game. </param>
        public Objects(GameObject[,] data, IGameSlotsPool pool)
            : base(data)
        {
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

            if (pool == null)
            {
                throw new ArgumentNullException("pool");
            }

            this.playersPool = pool;
        }

        /// <summary>
        /// Clone object
        /// </summary>
        /// <returns>clonned object</returns>
        public Objects Clone()
        {
            GameObject[,] data = new GameObject[this.Width, this.Height];
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (this[x, y] != null)
                    {
                        data[x, y] = this[x, y].Clone();
                    }
                }
            }

            return new Objects(data, this.playersPool);
        }

        /// <summary>
        /// Gets all LightCycles
        /// </summary>
        public List<LightCycleBike> LightCycles
        {
            get
            {
                return this.FindObjects<LightCycleBike>();
            }
        }
        
        /// <summary>
        /// Gets all trails
        /// </summary>
        public List<Trail> Trails
        {
            get { return this.FindObjects<Trail>(); }
        }

        /// <summary>
        /// Gets players 
        /// </summary>
        public IdentityCollection Players
        {
            get
            {
                IdentityCollection result = new IdentityCollection();
                foreach (LightCycleBike bike in this.LightCycles)
                {
                    result.Add(bike.Player);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets teams
        /// </summary>
        public IdentityCollection Teams
        {
            get
            {
                IdentityCollection teams = new IdentityCollection();
                foreach (Identity identity in this.Players)
                {
                    Identity team = this.playersPool.Slots.GetPlayersSlot(identity).Team;
                    if (!teams.Contains(team))
                    {
                        teams.Add(team);
                    }
                }

                return teams;
            }
        }

        /// <summary>
        /// Find light cycle for player identity
        /// </summary>
        /// <param name="identity">player identity</param>
        /// <returns>LightCycle matching identity</returns>
        public LightCycleBike FindLightCycle(Identity identity)
        {
            foreach (LightCycleBike bike in this.LightCycles)
            {
                if(bike.Player.Equals(identity))
                {
                    return bike;
                }
            }

            return null;
        }
        
        /// <summary>
        /// Remove players objects
        /// </summary>
        /// <param name="identity">player identity</param>
        public void RemovePlayer(Identity identity)
        {
            // find any object related to player and nullify
            this.Fill(
                (location, obj) =>
                    {
                        PlayerGameObject playerObject = (PlayerGameObject) obj;
                        return playerObject != null && playerObject.Player.Equals(identity);
                    },
                (location) => null);
        }



        /// <summary>
        /// Ages light trails
        /// </summary>
        /// <param name="gameTurn">Game turn.</param>
        /// <param name="fadingSpeed">Fading speed.</param>
        public void AgeTrails(int gameTurn, float fadingSpeed)
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    // Trail age increases automatically each time it is cloned
                    Trail obj = this[x, y] as Trail;
                    if (obj != null && obj.Age >= gameTurn * (1 - fadingSpeed))
                    {
                        this[x, y] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Move player to new location
        /// </summary>
        /// <param name="player">player to be moved</param>
        /// <param name="location">current location of player</param>
        /// <param name="moveDirection">player move direction</param>
        /// <param name="map">map used to play</param>
        /// <returns>move result</returns>
        public PlayerMoveResult MovePlayer(Identity player, Point location, Direction moveDirection, Map map)
        {
            Point newLocation = DirectionHelper.NextLocation(location, moveDirection);
            if (map[newLocation].IsPassable && this[newLocation] == null)
            {
                // player moves to new loaction
                this[newLocation] = new LightCycleBike(newLocation, player, moveDirection);
                this[location] = new Trail(location, player, 1);
                return new PlayerMoveResult(MoveResult.Successful);
            }

            // collision detected
            if (!map[newLocation].IsPassable)
            {
                return new PlayerMoveResult(MoveResult.CollisionWithObstacle);
            }

            var trail = this[newLocation] as Trail;
            if (trail != null)
            {
                return new PlayerMoveResult(MoveResult.CollisionWithTrail, trail.Player);
            }

            var bike = this[newLocation] as LightCycleBike;
            if (bike != null)
            {
                return new PlayerMoveResult(MoveResult.CollisionWithPlayer, bike.Player);
            }

            throw new NotSupportedException();
        }
    }
}
