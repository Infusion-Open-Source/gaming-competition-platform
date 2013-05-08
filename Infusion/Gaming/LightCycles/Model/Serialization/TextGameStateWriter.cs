namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    using System;
    using Infusion.Gaming.LightCycles.Extensions;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// Game state writer to text
    /// </summary>
    public class TextGameStateWriter : IGameStateWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextGameStateWriter"/> class.
        /// </summary>
        /// <param name="textWriter">text writer to be used</param>
        public TextGameStateWriter(System.IO.TextWriter textWriter)
        {
            if (textWriter == null)
            {
                throw new ArgumentNullException("textWriter");
            }

            this.Writer = textWriter;
        }

        /// <summary>
        /// Gets or sets text writer
        /// </summary>
        public System.IO.TextWriter Writer { get; protected set; }

        /// <summary>
        /// Writes game state to stream
        /// </summary>
        /// <param name="gameState">game state to be written to stream</param>
        public void Write(IGameState gameState)
        {
            if (gameState == null || gameState.Map == null || gameState.PlayersData == null)
            {
                throw new ArgumentNullException("gameState");
            }

            for (int y = 0; y < gameState.Map.Height; y++)
            {
                for (int x = 0; x < gameState.Map.Width; x++)
                {
                    this.Writer.Write(this.GetChar(gameState.Map[x, y], gameState.PlayersData[x, y]));
                }

                this.Writer.WriteLine();
            }
        }

        /// <summary>
        /// Converts map location into character
        /// </summary>
        /// <param name="location">location of given position</param>
        /// <param name="gameObject">game object at given position</param>
        /// <returns>character to write</returns>
        private char GetChar(Location location, GameObject gameObject)
        {
            if (gameObject != null)
            {
                var trail = gameObject as Trail;
                if (trail != null)
                {
                    return trail.Player.Id.ToLower();
                }

                var lightCycleBike = gameObject as LightCycleBike;
                if (lightCycleBike != null)
                {
                    return lightCycleBike.Player.Id;
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
