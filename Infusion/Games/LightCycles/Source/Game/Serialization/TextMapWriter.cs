namespace Infusion.Gaming.LightCycles.Serialization
{
    using System;
    using System.Text;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Data.MapObjects;

    /// <summary>
    /// The map text writer.
    /// </summary>
    public class TextMapWriter : IMapWriter<string>
    {
        /// <summary>
        /// Writes the map to stream
        /// </summary>
        /// <param name="map">map to be written</param>
        /// <returns>string with the map written in</returns>
        public string Write(Map map)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            StringBuilder builder = new StringBuilder();
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    builder.Append(this.LocatationToChar(map[x, y]));
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
        
        /// <summary>
        /// Converts map location into character
        /// </summary>
        /// <param name="location">location to be written</param>
        /// <returns>character to write</returns>
        private char LocatationToChar(MapLocation location)
        {
            var playersStartingLocation = location as PlayersStartingLocation;
            if (playersStartingLocation != null)
            {
                return playersStartingLocation.Player.Identifier;
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