namespace Infusion.Gaming.LightCycles.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data.MapObjects;

    /// <summary>
    /// The map text reader.
    /// </summary>
    public class TextMapReader : IMapReader<string>
    {
        /// <summary>
        /// Read entire map from stream
        /// </summary>
        /// <param name="text">text to be read</param>
        /// <returns>map read form stream</returns>
        public MapLocation[,] Read(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            int height;
            int width;
            string[] lines = this.PreprocessMapStream(text);
            this.GetMapStreamDimensions(lines, out width, out height);

            if (width < Constraints.MinMapWidth || width > Constraints.MaxMapWidth || height < Constraints.MinMapHeight || height > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException();
            }

            MapLocation[,] data = new MapLocation[width, height];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    data[x, y] = this.CharToLocation(new Point(x, y), lines[y][x]);
                }
            }
            
            return data;
        }
        
        /// <summary>
        /// Converts character into map location
        /// </summary>
        /// <param name="location">location coordinates</param>
        /// <param name="c">character to decode</param>
        /// <returns>location under given character</returns>
        private MapLocation CharToLocation(Point location, char c)
        {
            if (c == Constraints.MapObstacleCharacter)
            {
                return new Obstacle(location);
            }

            if (c == Constraints.MapSpaceCharacter)
            {
                return new Space(location);
            }

            if (c >= Constraints.MinTeamId && c <= Constraints.MaxTeamId)
            {
                return new PlayersStartingLocation(location, new Identity(c), new Identity(c));
            }

            throw new NotSupportedException();
        }
        
        /// <summary>
        /// Gets map dimensions from the text.
        /// </summary>
        /// <param name="lines">The lines with map rows.</param>
        /// <param name="width">Outputs the width.</param>
        /// <param name="height">Outputs the height.</param>
        private void GetMapStreamDimensions(string[] lines, out int width, out int height)
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
        /// <param name="text">The text to preprocess. </param>
        /// <returns>The set of map row strings.</returns>
        private string[] PreprocessMapStream(string text)
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
    }
}