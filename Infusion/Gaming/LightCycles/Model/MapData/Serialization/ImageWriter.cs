namespace Infusion.Gaming.LightCycles.Model.MapData.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// Provides writer for the map in image format
    /// </summary>
    public class ImageWriter : IMapWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageWriter"/> class.
        /// </summary>
        /// <param name="bitmap">bitmap to write the map to</param>
        public ImageWriter(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            this.Bitmap = bitmap;
        }

        /// <summary>
        /// Gets or sets bitmap to write to
        /// </summary>
        public Bitmap Bitmap { get; protected set; }
        
        /// <summary>
        /// Writes entire map to bitmap
        /// </summary>
        /// <param name="map">map to be written</param>
        public void Write(IMap map)
        {
            if (map.Width != this.Bitmap.Width || map.Height != this.Bitmap.Height)
            {
                throw new ArgumentOutOfRangeException("map", "map size is different than the bitmap size");
            }

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    var location = map[x, y];
                    if (location is Obstacle)
                    {
                        this.Bitmap.SetPixel(x, y, Color.Black);
                    }
                    else if (location is Space)
                    {
                        this.Bitmap.SetPixel(x, y, Color.White);
                    }
                    else if (location is PlayersStartingLocation)
                    {
                        PlayersStartingLocation playerStartingLocation = (PlayersStartingLocation)location;
                        this.Bitmap.SetPixel(x, y, Constraints.TeamDefinitions.GetTeamById(playerStartingLocation.Player.Team.Id).Color);
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
            }
        }
    }
}
