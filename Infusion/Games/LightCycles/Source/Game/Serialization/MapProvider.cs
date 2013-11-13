namespace Infusion.Gaming.LightCycles.Serialization
{
    using System;
    using System.Drawing;
    using System.IO;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Exceptions;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// Provides game map
    /// </summary>
    public class MapProvider : IMapProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapProvider" /> class.
        /// </summary>
        /// <param name="gameInfo">game info</param>
        /// <param name="playersInfo">players info</param>
        public MapProvider(GameInfo gameInfo, PlayerSetup playersInfo)
        {
            if (gameInfo == null)
            {
                throw new ArgumentNullException("gameInfo");
            }

            if (playersInfo == null)
            {
                throw new ArgumentNullException("playersInfo");
            }

            this.MapInfo = gameInfo;
            this.PlayersInfo = playersInfo;
        }

        /// <summary>
        /// Gets or sets game info
        /// </summary>
        public GameInfo MapInfo { get; protected set; }

        /// <summary>
        /// Gets or sets players info
        /// </summary>
        public PlayerSetup PlayersInfo { get; protected set; }

        /// <summary>
        /// Provides map described by a given game info
        /// </summary>
        /// <returns>map described by game info</returns>
        public Map Provide()
        {
            if (this.MapInfo.MapType == MapType.BitmapStream)
            {
                if (!File.Exists(this.MapInfo.MapFileName))
                {
                    throw new GameConfigurationException("Map file path invalid, no such file: " + this.MapInfo.MapFileName);
                }

                using (Bitmap bitmap = new Bitmap(this.MapInfo.MapFileName))
                {
                    var reader = new MapImageReader();
                    return new Map(this.MapInfo.MapName, reader.Read(bitmap));
                }
            }

            if (this.MapInfo.MapType == MapType.Generated)
            {
                return new MapGenerator(this.MapInfo.MapName, this.MapInfo.MapWidth, this.MapInfo.MapHeight, this.PlayersInfo.NumberOfPlayers, this.PlayersInfo.NumberOfTeams, this.MapInfo.ObstacleRatio).Provide();
            }
            
            throw new GameException("GameInfo map type is undefined");
        }
    }
}
