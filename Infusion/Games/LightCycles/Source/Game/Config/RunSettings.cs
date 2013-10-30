namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Game run settings
    /// </summary>
    [Serializable]
    public class RunSettings
    {
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
        /// Gets or sets players mappings
        /// </summary>
        [XmlArray]
        public List<Mapping> PlayerMappings { get; set; }

        /// <summary>
        /// Gets or sets teams mappings
        /// </summary>
        [XmlArray]
        public List<Mapping> TeamMappings { get; set; }
    }
}
