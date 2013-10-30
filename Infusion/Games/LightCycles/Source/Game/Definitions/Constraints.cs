namespace Infusion.Gaming.LightCycles.Definitions
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Game constraints class
    /// </summary>
    public static class Constraints
    {
        /// <summary>
        /// Minimal map width
        /// </summary>
        public const int MinMapWidth = 10;

        /// <summary>
        /// Maximal map width
        /// </summary>
        public const int MaxMapWidth = 1000;

        /// <summary>
        /// Minimal map height
        /// </summary>
        public const int MinMapHeight = 10;

        /// <summary>
        /// Maximal map height
        /// </summary>
        public const int MaxMapHeight = 1000;

        /// <summary>
        /// Minimal number of players
        /// </summary>
        public const int MinNumberOfPlayers = 2;

        /// <summary>
        /// Maximal number of players
        /// </summary>
        public const int MaxNumberOfPlayers = 8;

        /// <summary>
        /// Minimal number of teams
        /// </summary>
        public const int MinNumberOfTeams = 1;

        /// <summary>
        /// Maximal number of teams
        /// </summary>
        public const int MaxNumberOfTeams = 8;

        /// <summary>
        /// Minimal id of a team
        /// </summary>
        public const char MinTeamId = 'A';

        /// <summary>
        /// Maximal id of a team
        /// </summary>
        public const char MaxTeamId = 'Z';

        /// <summary>
        /// Minimal id of a player
        /// </summary>
        public const char MinPlayerId = 'A';

        /// <summary>
        /// Maximal id of a player
        /// </summary>
        public const char MaxPlayerId = 'Z';

        /// <summary>
        /// Minimal id of a player trail
        /// </summary>
        public const char MinPlayerTrailId = 'a';

        /// <summary>
        /// Maximal id of a player trail
        /// </summary>
        public const char MaxPlayerTrailId = 'z';

        /// <summary>
        /// Character for 'space' on a map
        /// </summary>
        public const char MapSpaceCharacter = ' ';

        /// <summary>
        /// Character for an 'obstacle' on a map
        /// </summary>
        public const char MapObstacleCharacter = '#';

        /// <summary>
        /// Color map
        /// </summary>
        private static List<Color> colorMap = new List<Color>
        {
            Color.FromArgb(255, 0, 0),
            Color.FromArgb(0, 255, 0),
            Color.FromArgb(0, 0, 255),
            Color.FromArgb(255, 255, 0),
            Color.FromArgb(255, 0, 255),
            Color.FromArgb(0, 255, 255),
            Color.FromArgb(255, 127, 0),
            Color.FromArgb(255, 0, 127),
        };

        /// <summary>
        /// Gets color map
        /// </summary>
        public static List<Color> ColorMap 
        {
            get { return colorMap; }
        }
    }
}
