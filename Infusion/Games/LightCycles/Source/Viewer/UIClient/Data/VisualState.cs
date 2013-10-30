namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.UIClient.Data.Visuals;

    /// <summary>
    /// Game visual state implementation
    /// </summary>
    public class VisualState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualState"/> class.
        /// </summary>
        public VisualState()
        {
            this.PlayersLocations = new Dictionary<GameIdentity, PointF>();
        }

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

        /// <summary>
        /// Gets or sets player focus points to track
        /// </summary>
        public Dictionary<GameIdentity, PointF> PlayersLocations { get; set; }
    }
}
