using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Extensions;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Data.MapObjects;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// Provides reader for the map in image format
    /// </summary>
    public class MapImageReader : IMapReader<Bitmap>
    {
        /// <summary>
        /// Read entire map from bitmap
        /// </summary>
        /// <returns>map read form bitmap</returns>
        public MapLocation[,] Read(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            MapLocation[,] data = new MapLocation[bitmap.Width, bitmap.Height];
            int nextPlayerIndex = 0;
            bool isTeamGame = this.CountNumberOfTeams(bitmap) > 1;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Point p = new Point(x, y);
                    Color c = bitmap.GetPixel(x, y);
                    if (c.AreSame(Color.Black))
                    {
                        data[x, y] = new Obstacle(p);
                    }
                    else if (c.AreSame(Color.White))
                    {
                        data[x, y] = new Space(p);
                    }
                    else if (Constraints.ColorMap.Contains(c))
                    {
                        char player = (char)(Constraints.MinPlayerId + (nextPlayerIndex++));
                        if (isTeamGame)
                        {
                            char team = (char)(Constraints.ColorMap.IndexOf(c) + Constraints.MinTeamId);
                            data[x, y] = new PlayersStartingLocation(p, new Identity(player), new Identity(team));
                        }
                        else
                        {
                            data[x, y] = new PlayersStartingLocation(p, new Identity(player), new Identity(player));
                        }
                    }
                }
            }

            return data;
        }
        
        /// <summary>
        /// Count number of teams on buffered map
        /// </summary>
        /// <param name="bitmap">bitmap</param>
        /// <returns>number of teams</returns>
        private int CountNumberOfTeams(Bitmap bitmap)
        {
            Dictionary<Color, char> teams = new Dictionary<Color, char>();
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    if (c.AreSame(Color.Black))
                    {
                        continue;
                    }
                    
                    if (c.AreSame(Color.White))
                    {
                        continue;
                    }

                    if (!teams.ContainsKey(c) && Constraints.ColorMap.Contains(c))
                    {
                        teams.Add(c, (char)(Constraints.ColorMap.IndexOf(c) + Constraints.MinTeamId));
                    }
                }
            }

            return teams.Count;
        }
    }
}
