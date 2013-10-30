namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Util;

    /// <summary>
    /// Class Teams and players used for serialization of players and teams settings for a gameplay
    /// </summary>
    [Serializable]
    public class TeamsAndPlayers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamsAndPlayers" /> class.
        /// </summary>
        public TeamsAndPlayers()
        {
            this.PlayerConfigs = new List<PlayerConfig>();
            this.TeamInformation = new List<TeamInfo>();
        }

        /// <summary>
        /// Gets or sets Root of players path
        /// </summary>
        [XmlAttribute]
        public string PathRoot { get; set; }

        /// <summary>
        /// Gets or sets List of players settings
        /// </summary>
        [XmlArray]
        public List<PlayerConfig> PlayerConfigs { get; set; }

        /// <summary>
        /// Gets or sets List of teams settings
        /// </summary>
        [XmlArray]
        public List<TeamInfo> TeamInformation { get; set; }

        /// <summary>
        /// Get player settings by name
        /// </summary>
        /// <param name="playerName">name of player</param>
        /// <returns>player settings by name</returns>
        public PlayerInfo GetPlayerInfo(string playerName)
        {
            foreach (PlayerConfig playerConfig in this.PlayerConfigs)
            {
                if (playerConfig.Name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    string configPath = Path.Combine(this.PathRoot, playerConfig.Config);
                    string configDirPath = Path.GetDirectoryName(configPath);
                    PlayerInfo playerInfo = new ConfigProvider<PlayerInfo>().Load(configPath);
                    playerInfo.ExePath = Path.Combine(configDirPath, playerInfo.ExePath);
                    return playerInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// Get team settings by name
        /// </summary>
        /// <param name="teamName">name of team</param>
        /// <returns>team settings by name</returns>
        public TeamInfo GetTeamInfo(string teamName)
        {
            foreach (TeamInfo teamInfo in this.TeamInformation)
            {
                if (teamInfo.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return teamInfo;
                }
            }

            throw new ArgumentOutOfRangeException("teamName");
        }

        /// <summary>
        /// Gets paths of player exe files
        /// </summary>
        /// <param name="playersData">player information</param>
        /// <returns>dictionary with player exe files paths</returns>
        public Dictionary<Identity, string> GetPlayersExePaths(Dictionary<Identity, PlayerInfo> playersData)
        {
            Dictionary<Identity, string> executables = new Dictionary<Identity, string>();
            foreach (KeyValuePair<Identity, PlayerInfo> pair in playersData)
            {
                executables.Add(pair.Key, pair.Value.ExePath);
            }

            return executables;
        }
        
        /// <summary>
        /// Gets player information
        /// </summary>
        /// <param name="playersSetup">player setup struct</param>
        /// <param name="mappings">game player mappings</param>
        /// <returns>player information dictionary</returns>
        public Dictionary<Identity, PlayerInfo> GetPlayersInformation(PlayerSetup playersSetup, List<Mapping> mappings)
        {
            Dictionary<Identity, PlayerInfo> playerinformations = new Dictionary<Identity, PlayerInfo>();
            foreach (Identity playerIdentity in playersSetup.PlayersIdentities)
            {
                playerinformations.Add(playerIdentity, this.GetPlayerInfo(this.ResolveMapping(playerIdentity, mappings)));
            }

            return playerinformations;
        }

        /// <summary>
        /// Gets teams information
        /// </summary>
        /// <param name="playersSetup">teams setup struct</param>
        /// <param name="mappings">game teams mappings</param>
        /// <returns>teams information dictionary</returns>
        public Dictionary<Identity, TeamInfo> GetTeamsInformation(PlayerSetup playersSetup, List<Mapping> mappings)
        {
            Dictionary<Identity, TeamInfo> teaminformations = new Dictionary<Identity, TeamInfo>();
            foreach (Identity teamIdentity in playersSetup.TeamsIdentities.Unique)
            {
                teaminformations.Add(teamIdentity, this.GetTeamInfo(this.ResolveMapping(teamIdentity, mappings)));
            }

            return teaminformations;
        }

        /// <summary>
        /// Resolves game mapping
        /// </summary>
        /// <param name="id">identity to map</param>
        /// <param name="mappings">configuration mappings</param>
        /// <returns>name to which id is mapped</returns>
        private string ResolveMapping(Identity id, IEnumerable<Mapping> mappings)
        {
            foreach (Mapping mapping in mappings)
            {
                if (mapping.Id.Equals(id.Identifier.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return mapping.Name;
                }
            }

            throw new ArgumentOutOfRangeException("id");
        }
    }
}
