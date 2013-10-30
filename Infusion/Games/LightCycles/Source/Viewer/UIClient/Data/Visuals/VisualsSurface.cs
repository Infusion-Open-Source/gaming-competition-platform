namespace Infusion.Gaming.LightCycles.UIClient.Data.Visuals
{
    using System;

    /// <summary>
    /// Two dimensional surface of visuals
    /// </summary>
    public class VisualsSurface : IVisual
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualsSurface"/> class.
        /// </summary>
        /// <param name="width">
        /// The width of the surface.
        /// </param>
        /// <param name="height">
        /// The height of the surface.
        /// </param>
        public VisualsSurface(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            this.Width = width;
            this.Height = height;
            this.Visuals = new IVisual[this.Width, this.Height];
        }
        
        /// <summary>
        /// Gets or sets the height of the map.
        /// </summary>
        public int Height { get; protected set; }
        
        /// <summary>
        /// Gets or sets the map locations.
        /// </summary>
        public IVisual[,] Visuals { get; protected set; }
        
        /// <summary>
        /// Gets or sets the width of the map.
        /// </summary>
        public int Width { get; protected set; }
        
        /// <summary>
        /// Get or sets visual for specified location
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>visual at specified point</returns>
        public IVisual this[int x, int y]
        {
            get
            {
                return this.Visuals[x, y];
            }

            set
            {
                this.Visuals[x, y] = value;
            }
        }

        /// <summary>
        /// Check if given coordinates are in valid range 
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>whether coordinates are in valid range </returns>
        public bool IsInRange(int x, int y)
        {
            if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
            {
                return false;
            }

            return true;
        }
    }
}
