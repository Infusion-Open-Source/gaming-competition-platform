namespace Infusion.Gaming.LightCycles.Serialization
{
    using System;
    using System.Text;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.State;

    /// <summary>
    /// Text player game state writer, writes game state as a text in context of a given player
    /// </summary>
    public class TextPlayerGameStateWriter : IGameStateWriter<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextPlayerGameStateWriter" /> class.
        /// </summary>
        /// <param name="playerId">player id for which the context text is written</param>
        /// <param name="viewArea">size of view area</param>
        /// <param name="fogOfWar">whether fog of war is in use</param>
        public TextPlayerGameStateWriter(Identity playerId, int viewArea, bool fogOfWar)
        {
            if (playerId == null)
            {
                throw new ArgumentNullException("playerId");
            }

            if (viewArea <= 0 || viewArea >= 100)
            {
                throw new ArgumentOutOfRangeException("viewArea");
            }

            this.PlayerId = playerId;
            this.ViewArea = 1 + (2 * viewArea);
            this.FogOfWar = fogOfWar;
        }

        /// <summary>
        /// Gets or sets a value indicating whether fog of war is in use
        /// </summary>
        public bool FogOfWar { get; protected set; }

        /// <summary>
        /// Gets or sets size of view area
        /// </summary>
        public int ViewArea { get; protected set; }

        /// <summary>
        /// Gets or sets player id for which the context text is written
        /// </summary>
        public Identity PlayerId { get; protected set; }

        /// <summary>
        /// Writes game state to stream
        /// </summary>
        /// <param name="map">game map</param>
        /// <param name="gameState">game state to be written to stream</param>
        /// <returns>string containing game state in payer context</returns>
        public string Write(Map map, IGameState gameState)
        {
            TextGameStateWriter writer = new TextGameStateWriter();
            string stream = writer.Write(map, gameState);
            char[,] data = this.ToCharArray(stream.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

            int playerX;
            int playerY;
            this.FindPlayer(data, out playerX, out playerY);

            data = this.CropArray(data, playerX - (this.ViewArea / 2), playerY - (this.ViewArea / 2), this.ViewArea, this.ViewArea);
            switch (gameState.Objects.FindLightCycle(this.PlayerId).Direction)
            {
                case Direction.Up:
                    break;
                case Direction.Left:
                    data = this.RotateArray(data);
                    data = this.RotateArray(data);
                    data = this.RotateArray(data);
                    break;
                case Direction.Down:
                    data = this.RotateArray(data);
                    data = this.RotateArray(data);
                    break;
                case Direction.Right:
                    data = this.RotateArray(data);
                    break;
            }

            if (this.FogOfWar)
            {
                data = this.ApplyFog(data);
            }

            return this.ArrayToString(data);
        }

        /// <summary>
        /// Change char 2D array to string
        /// </summary>
        /// <param name="data">char 2D array</param>
        /// <returns>string result of operation</returns>
        private string ArrayToString(char[,] data)
        {
            int lines = 0;
            StringBuilder builder = new StringBuilder();
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    builder.Append(data[x, y]);
                }

                lines++;
                builder.AppendLine();
            }

            builder.AppendLine();
            return lines + Environment.NewLine + builder;
        }

        /// <summary>
        /// change string array to char 2D array
        /// </summary>
        /// <param name="data">string array to change</param>
        /// <returns>result char 2D array</returns>
        private char[,] ToCharArray(string[] data)
        {
            char[,] newData = new char[data.Length - 1, data[1].Length];
            for (int line = 1; line < data.Length; line++)
            {
                for (int index = 0; index < data[line].Length; index++)
                {
                    newData[line - 1, index] = data[line][index];
                }
            }

            return newData;
        }

        /// <summary>
        /// Crop rectangle of data from 2D char array
        /// </summary>
        /// <param name="data">data to be cropped</param>
        /// <param name="ix">start x</param>
        /// <param name="iy">start y</param>
        /// <param name="width">width of cropped area</param>
        /// <param name="height">height of cropped area</param>
        /// <returns>data cropped from original data</returns>
        private char[,] CropArray(char[,] data, int ix, int iy, int width, int height)
        {
            char[,] newData = new char[height, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (iy + y < 0 || iy + y >= data.GetLength(0))
                    {
                        newData[y, x] = '#';
                    }
                    else if (ix + x < 0 || ix + x >= data.GetLength(1))
                    {
                        newData[y, x] = '#';
                    }
                    else
                    {
                        newData[y, x] = data[iy + y, ix + x];
                    }
                }
            }

            return newData;
        }

        /// <summary>
        /// Rotate char array by 90 degrees
        /// </summary>
        /// <param name="data">char array to be rotated</param>
        /// <returns>rotated char array</returns>
        private char[,] RotateArray(char[,] data)
        {
            int width = data.GetLength(0);
            int height = data.GetLength(1);
            int newWidth = height;
            int newHeight = width;
            char[,] newData = new char[newWidth, newHeight];
            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    newData[x, y] = data[y, newWidth - (x + 1)];
                }
            }

            return newData;
        }

        /// <summary>
        /// Apply fog of war to char 2D array with data
        /// </summary>
        /// <param name="data">char 2D array with data</param>
        /// <returns>char 2D array with data with fog of war applied</returns>
        private char[,] ApplyFog(char[,] data)
        {
            char[,] newData = new char[data.GetLength(0), data.GetLength(1)];
            for (int y = 0; y < data.GetLength(1); y++)
            {
                for (int x = 0; x < data.GetLength(0); x++)
                {
                    int value = Math.Abs(x - (data.GetLength(0) / 2)) + y;
                    if (value < 3 + (data.GetLength(1) / 2))
                    {
                        newData[y, x] = data[y, x];
                    }
                    else
                    {
                        newData[y, x] = '.';
                    }
                }
            }

            return newData;
        }

        /// <summary>
        /// finds player position in char 2D array
        /// </summary>
        /// <param name="data">char 2D array to search</param>
        /// <param name="playerX">outputs player x position</param>
        /// <param name="playerY">outputs player y position</param>
        private void FindPlayer(char[,] data, out int playerX, out int playerY)
        {
            for (int x = 0; x < data.GetLength(1); x++)
            {
                for (int y = 0; y < data.GetLength(0); y++)
                {
                    if (data[y, x] == this.PlayerId.Identifier)
                    {
                        playerX = x;
                        playerY = y;
                        return;
                    }
                }
            }

            playerX = -1;
            playerY = -1;
        }
    }
}
