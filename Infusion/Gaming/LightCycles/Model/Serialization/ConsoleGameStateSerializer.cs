using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.MapData;
using Infusion.Gaming.LightCycles.Extensions;

namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    class ConsoleGameStateSerializer : IGameStateSerializer
    {
        public void Write(IGameState gameState)
        {
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
