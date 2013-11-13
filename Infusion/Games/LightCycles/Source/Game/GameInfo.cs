namespace Infusion.Gaming.LightCycles
{
    using System;
    using Infusion.Gaming.LightCycles.Definitions;

    /// <summary>
    /// Game map info
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfo" /> class.
        /// </summary>
        /// <param name="mapName">map name</param>
        /// <param name="mapWidth">width of generated map</param>
        /// <param name="mapHeight">height of generated map</param>
        /// <param name="trailAging">aging time of a light trail</param>
        /// <param name="obstacleRatio">obstacle ratio of generated map</param>
        /// <param name="cleanMoveScore">score on clean move</param>
        /// <param name="trailHitScore">score on trail hit</param>
        /// <param name="lastManStandScore">score on survival</param>
        /// <param name="randomizeStartLocations">randomize start locations</param>
        public GameInfo(string mapName, int mapWidth, int mapHeight, float trailAging, float obstacleRatio, int cleanMoveScore, int trailHitScore, int lastManStandScore, bool randomizeStartLocations)
        {
            if (mapWidth < Constraints.MinMapWidth || mapWidth > Constraints.MaxMapWidth)
            {
                throw new ArgumentOutOfRangeException("mapWidth");
            }

            if (mapHeight < Constraints.MinMapHeight || mapHeight > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException("mapHeight");
            }

            if (trailAging < 0 || trailAging >= 1)
            {
                throw new ArgumentOutOfRangeException("trailAging");
            }

            if (obstacleRatio < 0 || obstacleRatio >= 1)
            {
                throw new ArgumentOutOfRangeException("obstacleRatio");
            }

            if (cleanMoveScore < 0)
            {
                throw new ArgumentOutOfRangeException("cleanMoveScore");
            }

            if (trailHitScore < 0)
            {
                throw new ArgumentOutOfRangeException("trailHitScore");
            }

            if (lastManStandScore < 0)
            {
                throw new ArgumentOutOfRangeException("lastManStandScore");
            }

            this.MapName = mapName;
            this.MapFileName = string.Empty;
            this.MapType = MapType.Generated;
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
            this.TrailAging = trailAging;
            this.ObstacleRatio = obstacleRatio;
            this.CleanMoveScore = cleanMoveScore;
            this.TrailHitScore = trailHitScore;
            this.LastManStandScore = lastManStandScore;
            this.RandomizeStartLocations = randomizeStartLocations;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfo" /> class.
        /// </summary>
        /// <param name="mapName">map name</param>
        /// <param name="mapFileName">map file name</param>
        /// <param name="trailAging">aging time of a light trail</param>
        /// <param name="cleanMoveScore">score on clean move</param>
        /// <param name="trailHitScore">score on trail hit</param>
        /// <param name="lastManStandScore">score on survival</param>
        /// <param name="randomizeStartLocations">randomize start locations</param>
        public GameInfo(string mapName, string mapFileName, float trailAging, int cleanMoveScore, int trailHitScore, int lastManStandScore, bool randomizeStartLocations)
        {
            if (cleanMoveScore < 0)
            {
                throw new ArgumentOutOfRangeException("cleanMoveScore");
            }

            if (trailHitScore < 0)
            {
                throw new ArgumentOutOfRangeException("trailHitScore");
            }

            if (lastManStandScore < 0)
            {
                throw new ArgumentOutOfRangeException("lastManStandScore");
            }

            this.MapName = mapName;
            this.MapFileName = mapFileName;
            this.MapType = MapType.BitmapStream;
            this.MapWidth = 0;
            this.MapHeight = 0;
            this.TrailAging = trailAging;
            this.ObstacleRatio = 0;
            this.CleanMoveScore = cleanMoveScore;
            this.TrailHitScore = trailHitScore;
            this.LastManStandScore = lastManStandScore;
            this.RandomizeStartLocations = randomizeStartLocations;
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

        /// <summary>
        /// Gets or sets trail aging time
        /// </summary>
        public float TrailAging { get; protected set; }

        /// <summary>
        /// Gets or sets obstacle ratio on map
        /// </summary>
        public float ObstacleRatio { get; protected set; }

        /// <summary>
        /// Gets or sets score for clean move
        /// </summary>
        public int CleanMoveScore { get; protected set; }

        /// <summary>
        /// Gets or sets score for trail hit
        /// </summary>
        public int TrailHitScore { get; protected set; }

        /// <summary>
        /// Gets or sets score for survival
        /// </summary>
        public int LastManStandScore { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether game should randomize starting locations
        /// </summary>
        public bool RandomizeStartLocations { get; protected set; }
    }
}
