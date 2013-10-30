namespace Infusion.Gaming.LightCycles.UIClient.Data.Visuals
{
    using System.Drawing;
    using SlimDX;

    /// <summary>
    /// Heading text visual
    /// </summary>
    public class VisualText : IVisual
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualText"/> class.
        /// </summary>
        /// <param name="text">text to show</param>
        public VisualText(string text)
            : this(text, new PointF(0, 0), new Color4(1f, 1f, 1f), 1.0f, 12.0f)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualText"/> class.
        /// </summary>
        /// <param name="text">text to show</param>
        /// <param name="location">location of text to show</param>
        /// <param name="color">text color</param>
        /// <param name="opacity">text opacity</param>
        /// <param name="fontSize">text fontSize</param>
        public VisualText(string text, PointF location, SlimDX.Color4 color, float opacity, float fontSize)
        {
            this.Text = text;
            this.Location = location;
            this.Color = color;
            this.Opacity = opacity;
            this.FontSize = fontSize;
        }

        /// <summary>
        /// Gets or sets text
        /// </summary>
        public string Text { get; protected set; }

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

        /// <summary>
        /// Gets or sets font size
        /// </summary>
        public float FontSize { get; protected set; }

        /// <summary>
        /// Creates super heading text
        /// </summary>
        /// <param name="text">heading text</param>
        /// <param name="location">text location</param>
        /// <returns>heading visual</returns>
        public static VisualText CreateSuperHeadingText(string text, PointF location)
        {
            return new VisualText(text, location, new Color4(0f, 1f, 0f), 1.0f, 50.0f);
        }

        /// <summary>
        /// Creates super heading text
        /// </summary>
        /// <param name="text">heading text</param>
        /// <param name="color">heading text color</param>
        /// <param name="location">text location</param>
        /// <returns>heading visual</returns>
        public static VisualText CreateSuperHeadingText(string text, Color color, PointF location)
        {
            return new VisualText(text, location, new Color4(color.R / 255f, color.G / 255f, color.B / 255f), 1.0f, 50.0f);
        }

        /// <summary>
        /// Creates heading text
        /// </summary>
        /// <param name="text">heading text</param>
        /// <param name="location">text location</param>
        /// <returns>heading visual</returns>
        public static VisualText CreateHeadingText(string text, PointF location)
        {
            return new VisualText(text, location, new Color4(0f, 1f, 0f), 1.0f, 30.0f);
        }

        /// <summary>
        /// Creates heading text
        /// </summary>
        /// <param name="text">heading text</param>
        /// <param name="color">heading text color</param>
        /// <param name="location">text location</param>
        /// <returns>heading visual</returns>
        public static VisualText CreateHeadingText(string text, Color color, PointF location)
        {
            return new VisualText(text, location, new Color4(color.R / 255f, color.G / 255f, color.B / 255f), 1.0f, 30.0f);
        }

        /// <summary>
        /// Creates regular text
        /// </summary>
        /// <param name="text">regular text</param>
        /// <param name="location">text location</param>
        /// <returns>regular text visual</returns>
        public static VisualText CreateRegularText(string text, PointF location)
        {
            return new VisualText(text, location, new Color4(0f, 0.6f, 0f), 1.0f, 18.0f);
        }

        /// <summary>
        /// Creates regular text
        /// </summary>
        /// <param name="text">regular text</param>
        /// <param name="color">regular text color</param>
        /// <param name="location">text location</param>
        /// <returns>regular text visual</returns>
        public static VisualText CreateRegularText(string text, Color color, PointF location)
        {
            return new VisualText(text, location, new Color4(color.R / 255f, color.G / 255f, color.B / 255f), 1.0f, 18.0f);
        }
    }
}
