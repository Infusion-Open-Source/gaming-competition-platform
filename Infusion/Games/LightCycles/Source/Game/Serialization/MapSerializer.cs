
using System;
using System.Collections.Generic;
using System.Text;
using Infusion.Gaming.LightCycles.Model.Data;

namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    /// <summary>
    ///     The map serializer.
    /// </summary>
    public class MapSerializer
    {
        #region Public Methods and Operators

        /// <summary>
        /// Reads text representation of the map.
        /// </summary>
        /// <param name="text">
        /// The text with a map.
        /// </param>
        /// <returns>
        /// The map read from text <see cref="IMap"/>.
        /// </returns>
        public IMap Read(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            int height;
            int width;
            string[] lines = this.PreprocessMapStream(text);
            this.GetMapStreamDimensions(lines, out width, out height);

            var serializer = new LocationSerializer();
            var newMap = new Map(width, height);
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    newMap.Locations[x, y] = serializer.Read(lines[y][x]);
                }
            }

            return newMap;
        }

        /// <summary>
        /// Writes the map into the string.
        /// </summary>
        /// <param name="map">
        /// The map to be written.
        /// </param>
        /// <returns>
        /// The string wrote with map data <see cref="string"/>.
        /// </returns>
        public string Write(IMap map)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            var serializer = new LocationSerializer();
            var builder = new StringBuilder();
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    builder.Append(serializer.Write(map[x, y]));
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets map dimensions from the text.
        /// </summary>
        /// <param name="lines">
        /// The lines with map rows.
        /// </param>
        /// <param name="width">
        /// Outputs the width.
        /// </param>
        /// <param name="height">
        /// Outputs the height.
        /// </param>
        protected void GetMapStreamDimensions(string[] lines, out int width, out int height)
        {
            height = lines.Length;
            width = 0;
            foreach (string line in lines)
            {
                if (line.Length > width)
                {
                    width = line.Length;
                }
            }
        }

        /// <summary>
        /// Preprocess map text, does a cleanup and splits by map rows.
        /// </summary>
        /// <param name="text">
        /// The text to preprocess.
        /// </param>
        /// <returns>
        /// The set of map row strings.
        /// </returns>
        protected string[] PreprocessMapStream(string text)
        {
            var lines = new List<string>(text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            for (int i = 0; i < lines.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    lines.RemoveAt(i--);
                }
            }

            return lines.ToArray();
        }

        #endregion
    }
}