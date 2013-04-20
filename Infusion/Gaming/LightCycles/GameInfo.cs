namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.IO;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// Game info, describes game to be played
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfo" /> class.
        /// Game on specified map.
        /// </summary>
        /// <param name="numberOfPlayers">number of players in game</param>
        /// <param name="numberOfTeams">number of teams in game</param>
        /// <param name="gameMode">game mode</param>
        /// <param name="mapFileName">name of file with game map</param>
        public GameInfo(int numberOfPlayers, int numberOfTeams, GameModeEnum gameMode, string mapFileName)
        {
            if (gameMode == GameModeEnum.TeamDeathMatch && numberOfTeams >= numberOfPlayers)
            {
                throw new ArgumentOutOfRangeException("numberOfTeams", "in TeamDeatchMatch game number of teams must be the less than a number of players");
            }

            if (gameMode == GameModeEnum.FreeForAll && numberOfTeams != numberOfPlayers)
            {
                throw new ArgumentOutOfRangeException("numberOfTeams", "in FreeForAll game number of teams must be the same as number of players");
            }

            if (!File.Exists(mapFileName))
            {
                throw new FileNotFoundException("Unable to find specified map file", mapFileName);
            }

            this.NumberOfPlayers = numberOfPlayers;
            this.NumberOfTeams = numberOfTeams;
            this.GameMode = gameMode;
            this.MapFileName = mapFileName;
            this.MapWidth = 0;
            this.MapHeight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfo" /> class.
        /// Game on randomly generated map.
        /// </summary>
        /// <param name="numberOfPlayers">number of players in game</param>
        /// <param name="numberOfTeams">number of teams in game</param>
        /// <param name="gameMode">game mode</param>
        /// <param name="mapWidth">width of generated map</param>
        /// <param name="mapHeight">height of generated map</param>
        public GameInfo(int numberOfPlayers, int numberOfTeams, GameModeEnum gameMode, int mapWidth, int mapHeight)
        {
            if (gameMode == GameModeEnum.TeamDeathMatch && numberOfTeams >= numberOfPlayers)
            {
                throw new ArgumentOutOfRangeException("numberOfTeams", "in TeamDeatchMatch game number of teams must be the less than a number of players");
            }

            if (gameMode == GameModeEnum.FreeForAll && numberOfTeams != numberOfPlayers)
            {
                throw new ArgumentOutOfRangeException("numberOfTeams", "in FreeForAll game number of teams must be the same as number of players");
            }

            if (mapWidth < 0 || mapWidth > 100)
            {
                throw new ArgumentOutOfRangeException("mapWidth");
            }

            if (mapHeight < 0 || mapHeight > 100)
            {
                throw new ArgumentOutOfRangeException("mapHeight");
            }

            this.NumberOfPlayers = numberOfPlayers;
            this.NumberOfTeams = numberOfTeams;
            this.GameMode = gameMode;
            this.MapFileName = string.Empty;
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
        }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public int NumberOfPlayers { get; protected set; }

        /// <summary>
        /// Gets or sets number of teams in game
        /// </summary>
        public int NumberOfTeams { get; protected set; }

        /// <summary>
        /// Gets or sets game mode
        /// </summary>
        public GameModeEnum GameMode { get; protected set; }

        /// <summary>
        /// Gets or sets name of file with game map
        /// </summary>
        public string MapFileName { get; protected set; }

        /// <summary>
        /// Gets or sets width of random map
        /// </summary>
        public int MapWidth { get; protected set; }

        /// <summary>
        /// Gets or sets height of random map
        /// </summary>
        public int MapHeight { get; protected set; }
        
        /// <summary>
        /// Gets a value indicating whether map file is in use
        /// </summary>
        public bool UseMapFile
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.MapFileName);
            }
        }
    }
}
