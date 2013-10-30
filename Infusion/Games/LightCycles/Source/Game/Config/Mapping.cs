namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Identity mapping
    /// </summary>
    [Serializable]
    public class Mapping
    {
        /// <summary>
        /// Gets or sets mapping identity
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets mapping name
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
    }
}
