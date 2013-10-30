namespace Infusion.Gaming.LightCycles.UIClient.Data.Visuals
{
    using System.Drawing;

    /// <summary>
    /// Masking visual
    /// </summary>
    public class Mask : IVisual
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mask"/> class.
        /// </summary>
        /// <param name="color">text color</param>
        /// <param name="opacity">text opacity</param>
        /// <param name="rectangle">rectangle to fill</param>
        public Mask(SlimDX.Color4 color, float opacity, RectangleF rectangle)
        {
            this.Color = color;
            this.Opacity = opacity;
            this.Rectangle = rectangle;
        }

        /// <summary>
        /// Gets or sets rectangle to fill
        /// </summary>
        public RectangleF Rectangle { get; protected set; }

        /// <summary>
        /// Gets or sets text location
        /// </summary>
        public PointF Location { get; protected set; }

        /// <summary>
        /// Gets or sets text color
        /// </summary>
        public SlimDX.Color4 Color { get; protected set; }

        /// <summary>
        /// Gets or sets opacity
        /// </summary>
        public float Opacity { get; protected set; }
    }
}
