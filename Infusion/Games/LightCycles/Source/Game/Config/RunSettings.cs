namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Util;

    /// <summary>
    /// Game run settings
    /// </summary>
    [Serializable]
    public class RunSettings
    {
        /// <summary>
        /// Players info
        /// </summary>
        private PlayerInfoCollection playersInfoData = null;

        /// <summary>
        /// Gets or sets players files root path
        /// </summary>
        [XmlElement]
        public string PlayersPathRoot { get; set; }

        /// <summary>
        /// Gets or sets player response time limit
        /// </summary>
        [XmlElement]
        public int TimeLimit { get; set; }

        /// <summary>
        /// Gets or sets player view area size
        /// </summary>
        [XmlElement]
        public int ViewArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether fog of war is in use
        /// </summary>
        [XmlElement]
        public bool FogOfWar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether game is in debug mode
        /// </summary>
        [XmlElement]
        public bool DebugMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether game should randomize starting locations
        /// </summary>
        [XmlElement]
        public bool RandomizeStartLocations { get; set; }

        /// <summary>
        /// Gets or sets players mappings
        /// </summary>
        [XmlArray]
        public MappingCollection PlayerMappings { get; set; }

        /// <summary>
        /// Gets or sets teams mappings
        /// </summary>
        [XmlArray]
        public MappingCollection TeamMappings { get; set; }

        /// <summary>
        /// Gets or sets teams information
        /// </summary>
        [XmlArray]
        public TeamInfoCollection TeamInformation { get; set; }

        /// <summary>
        /// Gets players information
        /// </summary>
        [XmlIgnore]
        public PlayerInfoCollection PlayersInformation
        {
            get
            {
                if (this.playersInfoData == null)
                {
                    this.playersInfoData = this.DiscoverPlayersInfos(this.PlayersPathRoot);
                }

                return this.playersInfoData;
            }
        }

        /// <summary>
        /// Gets players information map
        /// </summary>
        [XmlIgnore]
        public Dictionary<Identity, PlayerInfo> PlayersInfoMap
        {
            get 
            {
                Dictionary<Identity, PlayerInfo> map = new Dictionary<Identity, PlayerInfo>();
                foreach (Mapping mapping in this.PlayerMappings)
                {
                    map.Add(new Identity(mapping.Id), this.PlayersInformation.FindByName(mapping.Name));
                }

                return map;
            }
        }

        /// <summary>
        /// Gets teams information map
        /// </summary>
        [XmlIgnore]
        public Dictionary<Identity, TeamInfo> TeamInfoMap
        {
            get
            {
                Dictionary<Identity, TeamInfo> map = new Dictionary<Identity, TeamInfo>();
                foreach (Mapping mapping in this.TeamMappings)
                {
                    map.Add(new Identity(mapping.Id), this.TeamInformation.FindByName(mapping.Name));
                }

                return map;
            }
        }

        /// <summary>
        /// Discover players information files
        /// </summary>
        /// <param name="rootDir">directory to search</param>
        /// <returns>players information found</returns>
        private PlayerInfoCollection DiscoverPlayersInfos(string rootDir)
        {
            PlayerInfoCollection result = new PlayerInfoCollection();
            foreach (string directory in Directory.GetDirectories(rootDir))
            {
                result.AddRange(this.DiscoverPlayersInfos(directory));
            }

            foreach (string file in Directory.GetFiles(rootDir, "PlayerInfo.xml"))
            {
                PlayerInfo playerInfo = new ConfigProvider<PlayerInfo>().Load(file);
                playerInfo.ExePath = Path.Combine(rootDir, playerInfo.ExePath);
                result.Add(playerInfo);
            }

            return result;
        }
    }
}
