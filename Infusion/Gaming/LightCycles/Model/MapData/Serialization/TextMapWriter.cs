namespace Infusion.Gaming.LightCycles.Model.MapData.Serialization
{
    using System;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// The map text writer.
    /// </summary>
    public class TextMapWriter : IMapWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMapWriter"/> class.
        /// </summary>
        /// <param name="writer">text writer to use</param>
        public TextMapWriter(System.IO.TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            this.Writer = writer;
        }

        /// <summary>
        /// Gets or sets text writer
        /// </summary>
        public System.IO.TextWriter Writer { get; protected set; }

        /// <summary>
        /// Writes the map to stream
        /// </summary>
        /// <param name="map">map to be written</param>
        public void Write(IMap map)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            if (map.Width < Constraints.MinMapWidth || map.Width > Constraints.MaxMapWidth || map.Height < Constraints.MinMapHeight || map.Height > Constraints.MaxMapHeight)
            {
                throw new ArgumentOutOfRangeException("map");
            }

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    this.Writer.Write(this.LocatationToChar(map[x, y]));
                }

                this.Writer.WriteLine();
            }
        }
        
        /// <summary>
        /// Converts map location into character
        /// </summary>
        /// <param name="location">location to be written</param>
        /// <returns>character to write</returns>
        private char LocatationToChar(Location location)
        {
            var playersStartingLocation = location as PlayersStartingLocation;
            if (playersStartingLocation != null)
            {
                return playersStartingLocation.Player.Id;
            }
            
            if (location is Obstacle)
            {
                return Constraints.MapObstacleCharacter;
            }
            
            if (location is Space)
            {
                return Constraints.MapSpaceCharacter;
            }
            
            throw new NotSupportedException();
        }
    }
}