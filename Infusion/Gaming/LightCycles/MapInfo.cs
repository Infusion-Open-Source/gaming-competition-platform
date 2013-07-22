using System;
using System.IO;
using Infusion.Gaming.LightCycles.Definitions;

namespace Infusion.Gaming.LightCycles
{
    /// <summary>
    /// Game map info
    /// </summary>
    public class MapInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapInfo" /> class.
        /// </summary>
        /// <param name="mapName">map name</param>
        /// <param name="mapWidth">width of generated map</param>
        /// <param name="mapHeight">height of generated map</param>
        public MapInfo(string mapName, int mapWidth, int mapHeight)
        {
            if (mapWidth < Constraints.MinMapWidth || mapWidth > Constraints.MaxMapWidth)
            {
                throw new ArgumentOutOfRangeException("mapWidth");
            }

            if (mapHeight < Constraints.MinMapHeight || mapHeight > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException("mapHeight");
            }

            this.MapName = mapName;
            this.MapFileName = string.Empty;
            this.MapType = MapType.Generated;
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapInfo" /> class.
        /// </summary>
        /// <param name="mapName">map name</param>
        /// <param name="mapFileName">map file name</param>
        public MapInfo(string mapName, string mapFileName)
        {
            this.MapName = mapName;
            this.MapFileName = mapFileName;
            this.MapType = MapType.Streamed;
            this.MapWidth = 0;
            this.MapHeight = 0;
        }
        
        /// <summary>
        /// Gets or sets name of the map
        /// </summary>
        public string MapName { get; protected set; }

        /// <summary>
        /// Gets or sets name of the map file
        /// </summary>
        public string MapFileName { get; protected set; }

        /// <summary>
        /// Gets or sets map type
        /// </summary>
        public MapType MapType { get; protected set; }

        /// <summary>
        /// Gets or sets width of map
        /// </summary>
        public int MapWidth { get; protected set; }

        /// <summary>
        /// Gets or sets height of map
        /// </summary>
        public int MapHeight { get; protected set; }
    }
}
