using System;
using System.Drawing;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Exceptions;
using Infusion.Gaming.LightCycles.Model.Data;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// Provides game map
    /// </summary>
    public class MapProvider : IMapProvider
    {
        /// <summary>
        /// Get or sets game info
        /// </summary>
        public MapInfo MapInfo { get; protected set; }

        /// <summary>
        /// Get or sets players info
        /// </summary>
        public PlayersInfo PlayersInfo { get; protected set; }

        /// <summary>
        /// Creates new instance of MapProvider
        /// </summary>
        /// <param name="gameInfo">game info</param>
        /// <param name="playersInfo">players info</param>
        public MapProvider(MapInfo gameInfo, PlayersInfo playersInfo)
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
        /// Provides map described by a given game info
        /// </summary>
        /// <returns>map described by game info</returns>
        public Map Provide()
        {
            if (this.MapInfo.MapType == MapType.Streamed)
            {
                using (Bitmap bitmap = new Bitmap(this.MapInfo.MapName))
                {
                    var reader = new MapImageReader();
                    return new Map(this.MapInfo.MapName, reader.Read(bitmap));
                }
            }
            
            if (MapInfo.MapType == MapType.Generated)
            {
                return new MapGenerator(this.MapInfo.MapName, this.MapInfo.MapWidth, this.MapInfo.MapHeight, this.PlayersInfo.NumberOfPlayers, this.PlayersInfo.NumberOfTeams).Provide();
            }
            
            throw new GameException("GameInfo map type is undefined");
        }
    }
}
