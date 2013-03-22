
using System;
using System.Collections.Generic;
using System.Text;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Defines;
using Infusion.Gaming.LightCycles.Extensions;

namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    /// <summary>
    ///     The players data serializer.
    /// </summary>
    public class GameStateRenderer
    {
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
                    switch(gameState.PlayersData[x,y].PlayerDataType)
                    {
                        case PlayerDataTypeEnum.Trail:
                            builder.Append(gameState.PlayersData[x, y].Player.Id.ToLower());
                            break;
                        case PlayerDataTypeEnum.Player:
                            builder.Append(gameState.PlayersData[x, y].Player.Id.ToUpper());
                            break;
                        default:
                            switch (gameState.Map[x,y].LocationType)
                            {
                                case LocationTypeEnum.Wall:
                                    builder.Append("#");
                                    break;
                                case LocationTypeEnum.Space:
                                case LocationTypeEnum.PlayersStartingLocation:
                                    builder.Append(" ");
                                    break;
                                default:
                                    builder.Append("!");
                                    break;
                            }
                            break;
                    }
                }

                builder.AppendLine();
            }

            Console.Clear();
            Console.Write(builder.ToString());
        }
    }
}