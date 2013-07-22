using System;
using System.Drawing;

namespace Infusion.Gaming.LightCycles.Extensions
{
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

        /// <summary>
        /// Parse color from a string
        /// </summary>
        /// <param name="text">text with coma separated color values like 0,0,255</param>
        /// <returns>Color instance</returns>
        public static Color Parse(string text)
        {
            string[] colors = text.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return Color.FromArgb(
                int.Parse(colors[0]), 
                int.Parse(colors[1]), 
                int.Parse(colors[2]));
        }
    }
}