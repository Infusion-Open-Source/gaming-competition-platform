namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// The map generator.
    /// </summary>
    public class MapGenerator
    {
        /// <summary>
        /// Generates map.
        /// </summary>
        /// <param name="width">The width of the map.</param>
        /// <param name="height">The height of the map.</param>
        /// <param name="numberOfPlayers">The number of players in the game.</param>
        /// <param name="numberOfTeams">The number of teams in the game.</param>
        /// <returns>The generated map.</returns>
        public IMap GenerateMap(int width, int height, int numberOfPlayers, int numberOfTeams)
        {
            var locations = this.GenerateLocationsBox(width, height);
            var playersList = this.BuildPlayersList(numberOfPlayers, numberOfTeams);
            return new Map(this.AddPlayersStartLocations(locations, playersList));
        }

        /// <summary>
        /// Build list of players assigned to given number of teams 
        /// </summary>
        /// <param name="numberOfPlayers">The number of players in the game.</param>
        /// <param name="numberOfTeams">The number of teams in the game.</param>
        /// <returns>List of players assigned to the teams</returns>
        public List<Player> BuildPlayersList(int numberOfPlayers, int numberOfTeams)
        {
            if (numberOfPlayers < 1 || numberOfPlayers > 16)
            {
                throw new ArgumentOutOfRangeException("numberOfPlayers");
            }

            if (numberOfTeams < 1 || numberOfTeams > numberOfPlayers)
            {
                throw new ArgumentOutOfRangeException("numberOfTeams");
            }

            char nextTeam = 'A';
            List<Team> teams = new List<Team>(numberOfTeams);
            for (int t = 0; t < numberOfTeams; t++)
            {
                teams.Add(new Team(nextTeam++));
            }

            int nextTeamId = 0;
            char nextPlayer = 'A';
            List<Player> players = new List<Player>(numberOfPlayers);
            for (int p = 0; p < numberOfPlayers; p++)
            {
                if (nextTeamId >= teams.Count)
                {
                    nextTeamId = 0;
                }

                players.Add(new Player(nextPlayer++, teams[nextTeamId++]));
            }

            return players;
        }

        /// <summary>
        /// Adds players start locations to the map in random locations.
        /// </summary>
        /// <param name="locations">The number of players to add.</param>
        /// <param name="players">Players for which start locations should be added.</param>
        /// <returns>The map with players start locations on it.</returns>
        public Location[,] AddPlayersStartLocations(Location[,] locations, IEnumerable<Player> players)
        {
            var rnd = new Random();
            Queue<Player> playersQueue = new Queue<Player>(players);
            while (playersQueue.Count > 0)
            {
                int x = 1 + rnd.Next(locations.GetLength(0) - 1);
                int y = 1 + rnd.Next(locations.GetLength(1) - 1);
                if (locations[x, y] is Space)
                {
                    locations[x, y] = new PlayersStartingLocation(playersQueue.Dequeue());
                }    
            }

            return locations;
        }

        /// <summary>
        /// Makes locations box, with obstacles boundary and empty in the center.
        /// </summary>
        /// <param name="width">The width of the map.</param>
        /// <param name="height">The height of the map.</param>
        /// <returns>Locations box</returns>
        public Location[,] GenerateLocationsBox(int width, int height)
        {
            if (width <= 10 || height > 10000)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height <= 10 || height > 10000)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            Location[,] locations = new Location[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || x + 1 == width || y == 0 || y + 1 == height)
                    {
                        locations[x, y] = new Obstacle();
                    }
                    else
                    {
                        locations[x, y] = new Space();
                    }
                }
            }

            locations[4, 3] = new Obstacle();
            locations[4, 4] = new Obstacle();
            locations[4, 5] = new Obstacle();
            locations[3, 4] = new Obstacle();
            locations[5, 4] = new Obstacle();
            locations[10, 10] = new Obstacle();
            locations[15, 10] = new Obstacle();
            locations[15, 11] = new Obstacle();
            locations[5, 14] = new Obstacle();
            locations[6, 15] = new Obstacle();
            locations[13, 14] = new Obstacle();
            locations[12, 15] = new Obstacle();
            return locations;
        }
    }
}