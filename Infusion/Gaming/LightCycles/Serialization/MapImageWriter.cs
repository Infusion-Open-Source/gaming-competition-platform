using System.Collections.Generic;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Model.Data;
using Infusion.Gaming.LightCycles.Model.Data.MapObjects;
using System;
using System.Drawing;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// Provides writer for the map in image format
    /// </summary>
    public class MapImageWriter : IMapWriter<Bitmap>
    {
        /// <summary>
        /// Writes entire map to bitmap
        /// </summary>
        /// <param name="map">map to be written</param>
        public Bitmap Write(Map map)
        {
            Bitmap bitmap = new Bitmap(map.Width, map.Height);
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    var location = map[x, y];
                    if (location is Obstacle)
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                    else if (location is Space)
                    {
                        bitmap.SetPixel(x, y, Color.White);
                    }
                    else if (location is PlayersStartingLocation)
                    {
                        PlayersStartingLocation playerStartingLocation = (PlayersStartingLocation)location;
                        bitmap.SetPixel(x, y, Constraints.ColorMap[playerStartingLocation.Team.Identifier - Constraints.MinTeamId]);
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
            }

            return bitmap;
        }
    }
}
