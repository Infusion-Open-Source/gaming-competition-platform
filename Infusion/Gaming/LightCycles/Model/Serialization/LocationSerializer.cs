
using System;
using Infusion.Gaming.LightCycles.Extensions;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Defines;

namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    /// <summary>
    ///     The location serializer.
    /// </summary>
    public class LocationSerializer
    {
        #region Public Methods and Operators

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
                return new Location(LocationTypeEnum.Wall);
            }

            if (c == ' ')
            {
                return new Location(LocationTypeEnum.Space);
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
            switch (location.LocationType)
            {
                case LocationTypeEnum.Wall:
                    return '#';
                case LocationTypeEnum.Space:
                    return ' ';
                case LocationTypeEnum.PlayersStartingLocation:
                    return ((PlayersStartingLocation)location).Player.Id;
                default:
                    throw new ArgumentOutOfRangeException("location");
            }
        }

        #endregion
    }
}