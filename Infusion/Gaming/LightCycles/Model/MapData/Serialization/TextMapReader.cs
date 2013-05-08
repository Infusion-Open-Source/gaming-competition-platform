namespace Infusion.Gaming.LightCycles.Model.MapData.Serialization
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// The map text reader.
    /// </summary>
    public class TextMapReader : IMapReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMapReader"/> class.
        /// </summary>
        /// <param name="reader">stream text reader</param>
        public TextMapReader(System.IO.TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            this.Reader = reader;
        }

        /// <summary>
        /// Gets or sets text reader
        /// </summary>
        public System.IO.TextReader Reader { get; protected set; }
        
        /// <summary>
        /// Read entire map from stream
        /// </summary>
        /// <returns>map read form stream</returns>
        public IMap Read()
        {
            int height;
            int width;
            string text = this.Reader.ReadToEnd();
            string[] lines = this.PreprocessMapStream(text);
            this.GetMapStreamDimensions(lines, out width, out height);

            if (width < Constraints.MinMapWidth || width > Constraints.MaxMapWidth || height < Constraints.MinMapHeight || height > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException();
            }

            Location[,] data = new Location[width, height];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    data[x, y] = this.CharToLocation(lines[y][x]);
                }
            }
            
            return new Map(data);
        }
        
        /// <summary>
        /// Converts character into map location
        /// </summary>
        /// <param name="c">character to decode</param>
        /// <returns>location under given character</returns>
        private Location CharToLocation(char c)
        {
            if (c == Constraints.MapObstacleCharacter)
            {
                return new Obstacle();
            }

            if (c == Constraints.MapSpaceCharacter)
            {
                return new Space();
            }

            if (c >= Constraints.MinTeamId && c <= Constraints.MaxTeamId)
            {
                return new PlayersStartingLocation(new Player(c, new Team(c)));
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