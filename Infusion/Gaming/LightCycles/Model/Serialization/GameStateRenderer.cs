using System;


namespace Infusion.Gaming.LightCycles.Model.Serialization
{

    /// <summary>
    /// The players data serializer.
    /// </summary>
    public class GameStateRenderer : IGameStateSink
    {

        /// <summary>
        /// Render game state
        /// </summary>
        /// <param name="gameState">state of the game</param>
        public void Flush(IGameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException("gameState");
            }

            Console.Clear();
            var serializer = new StringGameStateSerializer(Console.Out);
            serializer.Write(gameState);
            
        }

    }
}