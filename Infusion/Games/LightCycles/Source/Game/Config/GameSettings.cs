namespace Infusion.Gaming.LightCycles.Config
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Game settings
    /// </summary>
    [Serializable]
    public class GameSettings
    {
        /// <summary>
        /// Gets or sets game mode
        /// </summary>
        [XmlElement]
        public string GameMode { get; set; }

        /// <summary>
        /// Gets or sets players slots, example: ABCDEFGH
        /// </summary>
        [XmlElement]
        public string PlayerSlotAssignment { get; set; }

        /// <summary>
        /// Gets or sets teams slots, example: AAAABBBB
        /// </summary>
        [XmlElement]
        public string TeamSlotAssignment { get; set; }

        /// <summary>
        /// Gets or sets source of map
        /// </summary>
        [XmlElement]
        public string MapSource { get; set; }

        /// <summary>
        /// Gets or sets map file name
        /// </summary>
        [XmlElement]
        public string MapFileName { get; set; }

        /// <summary>
        /// Gets or sets map name
        /// </summary>
        [XmlElement]
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets map width
        /// </summary>
        [XmlElement]
        public int MapWidth { get; set; }

        /// <summary>
        /// Gets or sets map height
        /// </summary>
        [XmlElement]
        public int MapHeight { get; set; }

        /// <summary>
        /// Gets or sets trail ageing
        /// </summary>
        [XmlElement]
        public float TrailAging { get; set; }

        /// <summary>
        /// Gets or sets obstacle ratio
        /// </summary>
        [XmlElement]
        public float ObstacleRatio { get; set; }

        /// <summary>
        /// Gets or sets score for clear move
        /// </summary>
        [XmlElement]
        public int CleanMoveScore { get; set; }

        /// <summary>
        /// Gets or sets score for trail hit
        /// </summary>
        [XmlElement]
        public int TrailHitScore { get; set; }

        /// <summary>
        /// Gets or sets score for reaching end of game
        /// </summary>
        [XmlElement]
        public int LastManStandScore { get; set; }
    }
}
