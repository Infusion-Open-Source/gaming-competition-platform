namespace Infusion.Gaming.LightCycles.Model.MapData.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCyclesCommon.Definitions;
    using Infusion.Gaming.LightCyclesCommon.Extensions;

    /// <summary>
    /// Provides reader for the map in image format
    /// </summary>
    public class ImageReader : IMapReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageReader"/> class.
        /// </summary>
        /// <param name="bitmap">bitmap with the map</param>
        public ImageReader(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            this.Bitmap = bitmap;
        }

        /// <summary>
        /// Gets or sets the bitmap to read
        /// </summary>
        public Bitmap Bitmap { get; protected set; }
        
        /// <summary>
        /// Read entire map from bitmap
        /// </summary>
        /// <returns>map read form bitmap</returns>
        public IMap Read()
        {
            Location[,] data = new Location[this.Bitmap.Width, this.Bitmap.Height];
            int nextPlayerIndex = 0;
            bool isTeamGame = this.CountNumberOfTeams() > 1;
            for (int x = 0; x < this.Bitmap.Width; x++)
            {
                for (int y = 0; y < this.Bitmap.Height; y++)
                {
                    Color c = this.Bitmap.GetPixel(x, y);
                    if (c.AreSame(Color.Black))
                    {
                        data[x, y] = new Obstacle();
                    }
                    else if (c.AreSame(Color.White))
                    {
                        data[x, y] = new Space();
                    }
                    else if (Constraints.TeamDefinitions.IsTeamColor(c))
                    {
                        char player = Constraints.TeamDefinitions[nextPlayerIndex++].Id;
                        if (isTeamGame)
                        {
                            char team = Constraints.TeamDefinitions.GetTeamByColor(c).Id;
                            data[x, y] = new PlayersStartingLocation(new Player(player, new Team(team)));
                        }
                        else
                        {
                            data[x, y] = new PlayersStartingLocation(new Player(player));
                        }
                    }
                }
            }

            return new Map(data);
        }
        
        /// <summary>
        /// Count number of teams on buffered map
        /// </summary>
        /// <returns>number of teams</returns>
        private int CountNumberOfTeams()
        {
            Dictionary<Color, char> teams = new Dictionary<Color, char>();
            for (int x = 0; x < this.Bitmap.Width; x++)
            {
                for (int y = 0; y < this.Bitmap.Height; y++)
                {
                    Color c = this.Bitmap.GetPixel(x, y);
                    if (c.AreSame(Color.Black))
                    {
                        continue;
                    }
                    
                    if (c.AreSame(Color.White))
                    {
                        continue;
                    }

                    if (!teams.ContainsKey(c) && Constraints.TeamDefinitions.IsTeamColor(c))
                    {
                        teams.Add(c, Constraints.TeamDefinitions.GetTeamByColor(c).Id);
                    }
                }
            }

            return teams.Count;
        }
    }
}
