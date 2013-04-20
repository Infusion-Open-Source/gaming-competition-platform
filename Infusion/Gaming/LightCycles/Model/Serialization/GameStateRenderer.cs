namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    using System;
    using System.Text;
    using Infusion.Gaming.LightCycles.Extensions;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// The players data serializer.
    /// </summary>
    public class GameStateRenderer
    {
        /// <summary>
        /// Render game state
        /// </summary>
        /// <param name="gameState">state of the game</param>
        public void Render(IGameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException("gameState");
            }

            var builder = new StringBuilder();
            for (int y = 0; y < gameState.Map.Height; y++)
            {
                for (int x = 0; x < gameState.Map.Width; x++)
                {
                    if (gameState.PlayersData[x, y] is Trail)
                    {
                        builder.Append(((Trail)gameState.PlayersData[x, y]).Player.Id.ToLower());
                    }
                    else if (gameState.PlayersData[x, y] is LightCycleBike)
                    {
                        builder.Append(((LightCycleBike)gameState.PlayersData[x, y]).Player.Id.ToUpper());
                    }
                    else
                    {
                        if (gameState.Map[x, y] is Obstacle)
                        {
                            builder.Append("#");
                        }
                        else
                        {
                            builder.Append(" ");
                        }
                    }
                }

                builder.AppendLine();
            }

            Console.Clear();
            Console.Write(builder.ToString());
        }
    }
}