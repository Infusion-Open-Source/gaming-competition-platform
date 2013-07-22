using System.Text;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Extensions;
using Infusion.Gaming.LightCycles.Model.Data.GameObjects;
using Infusion.Gaming.LightCycles.Model.Data.MapObjects;
using Infusion.Gaming.LightCycles.Model.State;
using System;
using Infusion.Gaming.LightCycles.Model.Data;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// Game state writer to text
    /// </summary>
    public class TextGameStateWriter : IGameStateWriter<string>
    {
        /// <summary>
        /// Writes game state to stream
        /// </summary>
        /// <param name="map">game map</param>
        /// <param name="gameState">game state to be written to stream</param>
        public string Write(Map map, IGameState gameState)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            if (gameState == null || gameState.Objects == null)
            {
                throw new ArgumentNullException("gameState");
            }

            StringBuilder builder = new StringBuilder();
            for (int y = 0; y < gameState.Objects.Height; y++)
            {
                for (int x = 0; x < gameState.Objects.Width; x++)
                {
                    builder.Append(this.GetChar(map[x, y], gameState.Objects[x, y]));
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts map location into character
        /// </summary>
        /// <param name="location">location of given position</param>
        /// <param name="gameObject">game object at given position</param>
        /// <returns>character to write</returns>
        private char GetChar(MapLocation location, GameObject gameObject)
        {
            if (gameObject != null)
            {
                var trail = gameObject as Trail;
                if (trail != null)
                {
                    return trail.Player.Identifier.ToLower();
                }

                var lightCycleBike = gameObject as LightCycleBike;
                if (lightCycleBike != null)
                {
                    return lightCycleBike.Player.Identifier;
                }
            }
            
            if (location is Obstacle)
            {
                return Constraints.MapObstacleCharacter;
            }

            if (location is Space || location is PlayersStartingLocation)
            {
                return Constraints.MapSpaceCharacter;
            }

            throw new NotSupportedException();
        }
    }
}
