namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    using System;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// The location serializer.
    /// </summary>
    public class LocationSerializer
    {
        /// <summary>
        /// Reads location from character.
        /// </summary>
        /// <param name="c">
        /// The character to read.
        /// </param>
        /// <returns>
        /// The location read from character.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when character cannot be properly read as a location
        /// </exception>
        public Location Read(char c)
        {
            if (c == '#')
            {
                return new Obstacle();
            }

            if (c == ' ')
            {
                return new Space();
            }

            if (c >= 'A' && c <= 'Z')
            {
                return new PlayersStartingLocation(new Player(c));
            }
            
            throw new ArgumentOutOfRangeException("c");
        }

        /// <summary>
        /// Write location as a character.
        /// </summary>
        /// <param name="location">
        /// The location to write.
        /// </param>
        /// <returns>
        /// The written character.
        /// </returns>
        public char Write(Location location)
        {
            if (location is Obstacle)
            {
                return '#';
            }
            
            if (location is Space)
            {
                return ' ';
            }

            var playersStartingLocation = location as PlayersStartingLocation;
            if (playersStartingLocation != null)
            {
                return playersStartingLocation.Player.Id;
            }
            
            throw new ArgumentOutOfRangeException("location");
        }
    }
}