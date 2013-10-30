namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Player configuration map
    /// </summary>
    [Serializable]
    public class PlayerConfig
    {
        /// <summary>
        /// Gets or sets player map name
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets player config file
        /// </summary>
        [XmlAttribute]
        public string Config { get; set; }
    }
}
