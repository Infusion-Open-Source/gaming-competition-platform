namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    using Infusion.Gaming.LightCycles.UIClient.Data.Visuals;

    /// <summary>
    /// Game visual state implementation
    /// </summary>
    public class VisualState
    {
        /// <summary>
        /// Gets or sets size of the grid in pixels
        /// </summary>
        public float GridSize { get; set; }

        /// <summary>
        /// Gets half size of the grid
        /// </summary>
        public float GridSize2
        {
            get
            {
                return this.GridSize / 2;
            }
        }

        /// <summary>
        /// Gets quarter size of the grid
        /// </summary>
        public float GridSize4
        {
            get
            {
                return this.GridSize / 4;
            }
        }

        /// <summary>
        /// Gets or sets background layer
        /// </summary>
        public VisualsCollection BackgroundLayer { get; set; }

        /// <summary>
        /// Gets or sets grid layer
        /// </summary>
        public VisualsSurface GridLayer { get; set; }

        /// <summary>
        /// Gets or sets obstacles layer
        /// </summary>
        public VisualsSurface ObstaclesLayer { get; set; }

        /// <summary>
        /// Gets or sets trails layer
        /// </summary>
        public VisualsSurface TrailsLayer { get; set; }

        /// <summary>
        /// Gets or sets players layer
        /// </summary>
        public VisualsSurface PlayersLayer { get; set; }

        /// <summary>
        /// Gets or sets user interface layer
        /// </summary>
        public VisualsCollection UserInterfaceLayer { get; set; }

        /*
        /// <summary>
        /// Gets or sets list of teams
        /// </summary>
        public List<int> Teams { get; set; }

        /// <summary>
        /// Gets or sets list of players
        /// </summary>
        public Dictionary<int, int> Players { get; set; }

        /// <summary>
        /// Gets or sets turn number
        /// </summary>
        public int Turn { get; set; }

        /// <summary>
        /// Gets or sets game state
        /// </summary>
        public GameStateEnum State { get; set; }

        /// <summary>
        /// Gets or sets game mode
        /// </summary>
        public GameModeEnum Mode { get; set; }

        /// <summary>
        /// Gets or sets game result
        /// </summary>
        public GameResultEnum Result { get; set; }
        */ 
    }
}
