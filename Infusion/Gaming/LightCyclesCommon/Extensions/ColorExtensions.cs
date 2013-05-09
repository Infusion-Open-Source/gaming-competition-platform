namespace Infusion.Gaming.LightCyclesCommon.Extensions
{
    using System.Drawing;

    /// <summary>
    /// The color type extensions.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Checks whether color are the same
        /// </summary>
        /// <param name="a">First color to compare.</param>
        /// <param name="b">Second color to compare.</param>
        /// <returns>The changed character.</returns>
        public static bool AreSame(this Color a, Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;
        }
    }
}