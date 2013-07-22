using System.Drawing;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Data.MapObjects;
using System;
using System.Collections.Generic;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// The map generator.
    /// </summary>
    public class MapGenerator : IMapProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapGenerator" /> class.
        /// </summary>
        /// <param name="mapName">name of the map</param>
        /// <param name="width">Width of a map to generate</param>
        /// <param name="height">Height of a map to generate</param>
        /// <param name="numberOfPlayers">Number of players on a map to generate</param>
        /// <param name="numberOfTeams">Width of a number of teams on a map to generate</param>
        public MapGenerator(string mapName, int width, int height, int numberOfPlayers, int numberOfTeams)
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

            this.MapName = mapName;
            this.Width = width;
            this.Height = height;
            this.NumberOfPlayers = numberOfPlayers;
            this.NumberOfTeams = numberOfTeams;
        }

        /// <summary>
        /// Gets or sets width of map to generate
        /// </summary>
        public string MapName { get; protected set; }

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
        /// Provides a map 
        /// </summary>
        /// <returns>map to be used</returns>
        public Map Provide()
        {
            var locations = this.GenerateLocationsBox(this.Width, this.Height);
            var playersList = this.BuildPlayersList(this.NumberOfPlayers);
            var teamAssignments = this.BuildTeamAssignments(playersList, this.NumberOfTeams);
            var data = this.AddPlayersStartLocations(locations, playersList, teamAssignments);
            return new Map(this.MapName, data);
        }
        
        /// <summary>
        /// Build list of players assigned to given number of teams 
        /// </summary>
        /// <param name="numberOfPlayers">The number of players in the game.</param>
        /// <returns>List of players assigned to the teams</returns>
        protected List<Identity> BuildPlayersList(int numberOfPlayers)
        {
            List<Identity> players = new List<Identity>(numberOfPlayers);
            for (int p = 0; p < numberOfPlayers; p++)
            {
                players.Add(new Identity((char)(Constraints.MinPlayerId + p)));
            }

            return players;
        }

        /// <summary>
        /// Creates players team assignemnts
        /// </summary>
        /// <param name="playersList">list of players</param>
        /// <param name="numberOfTeams">number of teams</param>
        /// <returns>players assignments</returns>
        protected Dictionary<Identity, Identity> BuildTeamAssignments(List<Identity> playersList, int numberOfTeams)
        {
            Dictionary<Identity, Identity> assignamnets = new Dictionary<Identity, Identity>();
            List<Identity> teams = new List<Identity>(numberOfTeams);
            for (int t = 0; t < numberOfTeams; t++)
            {
                teams.Add(new Identity((char)(Constraints.MinTeamId + t)));
            }

            int nextTeamId = 0;
            foreach(Identity player in playersList)
            {
                if (nextTeamId >= teams.Count)
                {
                    nextTeamId = 0;
                }

                assignamnets.Add(player, teams[nextTeamId++]);
            }

            return assignamnets;
        }

        /// <summary>
        /// Adds players start locations to the map in random locations.
        /// </summary>
        /// <param name="locations">The number of players to add.</param>
        /// <param name="players">Players for which start locations should be added.</param>
        /// <param name="teamAssignments">Teams to which players are assigned.</param>
        /// <returns>The map with players start locations on it.</returns>
        protected MapLocation[,] AddPlayersStartLocations(MapLocation[,] locations, IEnumerable<Identity> players, IDictionary<Identity, Identity> teamAssignments)
        {
            var rnd = new Random();
            Queue<Identity> playersQueue = new Queue<Identity>(players);
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
                    Identity player = playersQueue.Dequeue();
                    locations[x, y] = new PlayersStartingLocation(new Point(x, y), player, teamAssignments[player]);
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
        protected MapLocation[,] GenerateLocationsBox(int width, int height)
        {
            Random random = new Random();
            MapLocation[,] locations = new MapLocation[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Point p = new Point(x, y);
                    if (x == 0 || x + 1 == width || y == 0 || y + 1 == height)
                    {
                        locations[x, y] = new Obstacle(p);
                    }
                    else if (random.NextDouble() < 0.025)
                    {
                        locations[x, y] = new Obstacle(p);
                    }
                    else
                    {
                        locations[x, y] = new Space(p);
                    }
                }
            }
            
            return locations;
        }
    }
}