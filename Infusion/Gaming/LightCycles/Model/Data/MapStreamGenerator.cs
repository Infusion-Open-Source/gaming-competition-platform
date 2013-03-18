
namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System;
    using System.Text;

    /// <summary>
    ///     The map stream generator.
    /// </summary>
    public class MapStreamGenerator
    {
        #region Fields

        /// <summary>
        ///     The internal map representation.
        /// </summary>
        private char[,] map;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Generates map.
        /// </summary>
        /// <param name="width">
        /// The width of the map.
        /// </param>
        /// <param name="height">
        /// The height of the map.
        /// </param>
        /// <param name="numberOfPlayers">
        /// The number of players on the map.
        /// </param>
        /// <returns>
        /// The generated map stream.
        /// </returns>
        public string Generate(int width, int height, int numberOfPlayers)
        {
            if (width < 10)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 10)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            this.MakeBox(width, height);
            this.AddPlayers(width, height, numberOfPlayers);

            var mapStreamBuilder = new StringBuilder();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    mapStreamBuilder.Append(this.map[x, y]);
                }

                mapStreamBuilder.AppendLine();
            }

            return mapStreamBuilder.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds players to the map in random locations.
        /// </summary>
        /// <param name="width">
        /// The width of the map.
        /// </param>
        /// <param name="height">
        /// The height of the map.
        /// </param>
        /// <param name="numberOfPlayers">
        /// The number of players to add.
        /// </param>
        protected void AddPlayers(int width, int height, int numberOfPlayers)
        {
            var rnd = new Random();
            for (int p = 0; p < numberOfPlayers; p++)
            {
                int x = 1 + rnd.Next(width - 1);
                int y = 1 + rnd.Next(height - 1);
                if (this.map[x, y] == ' ')
                {
                    this.map[x, y] = (char)('A' + p);
                }
                else
                {
                    // randomly chosen place already in use, try again
                    p--;
                }
            }
        }

        /// <summary>
        /// Makes map as a box, with wall boundary and empty in the center.
        /// </summary>
        /// <param name="width">
        /// The width of the map.
        /// </param>
        /// <param name="height">
        /// The height of the map.
        /// </param>
        protected void MakeBox(int width, int height)
        {
            this.map = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || x + 1 == width || y == 0 || y + 1 == height)
                    {
                        this.map[x, y] = '#';
                    }
                    else
                    {
                        this.map[x, y] = ' ';
                    }
                }
            }
        }

        #endregion
    }
}