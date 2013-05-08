namespace Infusion.Gaming.LightCycles.Model.Defines
{
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
        public const char MaxTeamId = 'H';

        /// <summary>
        /// Character for 'space' on a map
        /// </summary>
        public const char MapSpaceCharacter = ' ';

        /// <summary>
        /// Character for an 'obstacle' on a map
        /// </summary>
        public const char MapObstacleCharacter = '#';

        /// <summary>
        /// Gets the team definitions
        /// </summary>
        public static TeamDefinitions TeamDefinitions
        {
            get
            {
                return new TeamDefinitions
                    {
                        new TeamDefinition('A', Color.FromArgb(255, 0, 0)),
                        new TeamDefinition('B', Color.FromArgb(0, 255, 0)),
                        new TeamDefinition('C', Color.FromArgb(0, 0, 255)),
                        new TeamDefinition('D', Color.FromArgb(255, 255, 0)),
                        new TeamDefinition('E', Color.FromArgb(255, 0, 255)),
                        new TeamDefinition('F', Color.FromArgb(0, 255, 255)),
                        new TeamDefinition('G', Color.FromArgb(255, 127, 0)),
                        new TeamDefinition('H', Color.FromArgb(255, 0, 127)),
                    };
            }
        }
    }
}
