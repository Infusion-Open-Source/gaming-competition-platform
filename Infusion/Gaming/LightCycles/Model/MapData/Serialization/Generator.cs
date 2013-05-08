namespace Infusion.Gaming.LightCycles.Model.MapData.Serialization
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// The map generator.
    /// </summary>
    public class Generator : IMapReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Generator" /> class.
        /// </summary>
        /// <param name="width">Width of a map to generate</param>
        /// <param name="height">Height of a map to generate</param>
        /// <param name="numberOfPlayers">Number of players on a map to generate</param>
        /// <param name="numberOfTeams">Width of a number of teams on a map to generate</param>
        public Generator(int width, int height, int numberOfPlayers, int numberOfTeams)
        {
            if (numberOfPlayers < Constraints.MinNumberOfPlayers || numberOfPlayers > Constraints.MaxNumberOfPlayers)
            {
                throw new ArgumentOutOfRangeException("numberOfPlayers");
            }

            if (numberOfTeams < Constraints.MinNumberOfTeams || numberOfTeams > Constraints.MaxNumberOfTeams)
            {
                throw new ArgumentOutOfRangeException("numberOfTeams");
            }

            if (width <= Constraints.MinMapWidth || width > Constraints.MaxMapWidth)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height <= Constraints.MinMapHeight || height > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            this.Width = width;
            this.Height = height;
            this.NumberOfPlayers = numberOfPlayers;
            this.NumberOfTeams = numberOfTeams;
        }

        /// <summary>
        /// Gets or sets width of map to generate
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets or sets height of map to generate
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// Gets or sets number of players on the map to generate
        /// </summary>
        public int NumberOfPlayers { get; protected set; }

        /// <summary>
        /// Gets or sets number of teams the map to generate
        /// </summary>
        public int NumberOfTeams { get; protected set; }

        /// <summary>
        /// Reads in generated map
        /// </summary>
        /// <returns>Generated map</returns>
        public IMap Read()
        {
            var locations = this.GenerateLocationsBox(this.Width, this.Height);
            var playersList = this.BuildPlayersList(this.NumberOfPlayers, this.NumberOfTeams);
            return new Map(this.AddPlayersStartLocations(locations, playersList));
        }
        
        /// <summary>
        /// Build list of players assigned to given number of teams 
        /// </summary>
        /// <param name="numberOfPlayers">The number of players in the game.</param>
        /// <param name="numberOfTeams">The number of teams in the game.</param>
        /// <returns>List of players assigned to the teams</returns>
        protected List<Player> BuildPlayersList(int numberOfPlayers, int numberOfTeams)
        {
            List<Team> teams = new List<Team>(numberOfTeams);
            for (int t = 0; t < numberOfTeams; t++)
            {
                teams.Add(new Team(Constraints.TeamDefinitions[t].Id));
            }

            int nextTeamId = 0;
            List<Player> players = new List<Player>(numberOfPlayers);
            for (int p = 0; p < numberOfPlayers; p++)
            {
                if (nextTeamId >= teams.Count)
                {
                    nextTeamId = 0;
                }

                players.Add(new Player(Constraints.TeamDefinitions[p].Id, teams[nextTeamId++]));
            }

            return players;
        }

        /// <summary>
        /// Adds players start locations to the map in random locations.
        /// </summary>
        /// <param name="locations">The number of players to add.</param>
        /// <param name="players">Players for which start locations should be added.</param>
        /// <returns>The map with players start locations on it.</returns>
        protected Location[,] AddPlayersStartLocations(Location[,] locations, IEnumerable<Player> players)
        {
            var rnd = new Random();
            Queue<Player> playersQueue = new Queue<Player>(players);
            while (playersQueue.Count > 0)
            {
                const int Border = 3;
                int x = Border + rnd.Next(locations.GetLength(0) - (2 * Border));
                int y = Border + rnd.Next(locations.GetLength(1) - (2 * Border));
                if (locations[x + 1, y - 1] is Space
                    && locations[x + 1, y] is Space
                    && locations[x + 1, y + 1] is Space
                    && locations[x, y - 1] is Space
                    && locations[x, y] is Space
                    && locations[x, y + 1] is Space
                    && locations[x - 1, y - 1] is Space
                    && locations[x - 1, y] is Space
                    && locations[x - 1, y + 1] is Space)
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
        protected Location[,] GenerateLocationsBox(int width, int height)
        {
            Random random = new Random();
            Location[,] locations = new Location[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || x + 1 == width || y == 0 || y + 1 == height)
                    {
                        locations[x, y] = new Obstacle();
                    }
                    else if (random.NextDouble() < 0.025)
                    {
                        locations[x, y] = new Obstacle();
                    }
                    else
                    {
                        locations[x, y] = new Space();
                    }
                }
            }
            
            return locations;
        }
    }
}