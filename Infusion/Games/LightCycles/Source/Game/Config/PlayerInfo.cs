namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Player settings
    /// </summary>
    [Serializable]
    public class PlayerInfo
    {
        /// <summary>
        /// Gets or sets name of the player
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets name of color of player trail
        /// </summary>
        [XmlAttribute]
        public string TrailColor { get; set; }

        /// <summary>
        /// Gets or sets player execution path
        /// </summary>
        [XmlAttribute]
        public string ExePath { get; set; }
    }
}
