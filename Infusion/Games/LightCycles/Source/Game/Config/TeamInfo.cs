namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Setup for a team
    /// </summary>
    [Serializable]
    public class TeamInfo
    {
        /// <summary>
        /// Gets or sets the name of team
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets color of the trail of the team
        /// </summary>
        [XmlAttribute]
        public string TrailColor { get; set; }
    }
}
