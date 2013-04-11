namespace UIClient.Drawers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Model;
    using SlimDX;
    using SlimDX.Direct2D;
    using SlimDX.DirectWrite;
    using UIClient.Assets;
    using UIClient.Data;

    /// <summary>
    /// Draws players layer 
    /// </summary>
    public class PlayersDrawer : IDrawer
    {
        /// <summary>
        /// DirectWrite factory for creation of rendered text
        /// </summary>
        private SlimDX.DirectWrite.Factory factory;

        /// <summary>
        /// brush to draw bike
        /// </summary>
        private SolidColorBrush solidBrush;
        
        /// <summary>
        /// bitmap with a bike 
        /// </summary>
        private SlimDX.Direct2D.Bitmap bikeN;

        /// <summary>
        /// bitmap with a bike 
        /// </summary>
        private SlimDX.Direct2D.Bitmap bikeS;

        /// <summary>
        /// bitmap with a bike 
        /// </summary>
        private SlimDX.Direct2D.Bitmap bikeE;

        /// <summary>
        /// bitmap with a bike 
        /// </summary>
        private SlimDX.Direct2D.Bitmap bikeW;

        /// <summary>
        /// Initialize drawer, create managed resources
        /// </summary>
        /// <param name="renderTarget">render target</param>
        /// <param name="assetProvider">provides assets</param>
        public void Initialize(RenderTarget renderTarget, IAssetProvider assetProvider)
        {
            this.bikeN = assetProvider.LoadBitmap("bikeN.png", renderTarget);
            this.bikeS = assetProvider.LoadBitmap("bikeS.png", renderTarget);
            this.bikeE = assetProvider.LoadBitmap("bikeE.png", renderTarget);
            this.bikeW = assetProvider.LoadBitmap("bikeW.png", renderTarget);
            this.solidBrush = new SolidColorBrush(renderTarget, new Color4(1.0f, 0.0f, 0.0f), new BrushProperties { Opacity = 0.7f });

            this.factory = new SlimDX.DirectWrite.Factory(SlimDX.DirectWrite.FactoryType.Isolated);
        }

        /// <summary>
        /// Begin rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        public void RenderBegin(RenderTarget renderTarget, VisualState visualState)
        {
        }

        /// <summary>
        /// Do the rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        public void Render(RenderTarget renderTarget, VisualState visualState)
        {
            var playersAndTeams = this.GetPlayers(visualState);
            foreach (KeyValuePair<char, List<Point>> playerTrailPair in this.GetTrailPaths(visualState, playersAndTeams))
            {
                var color = TeamColors.Data[playersAndTeams[playerTrailPair.Key]];
                PointF p = new PointF(playerTrailPair.Value[0].X * visualState.GridSize, playerTrailPair.Value[0].Y * visualState.GridSize);

                int prevPIndex = (playerTrailPair.Value.Count > 1) ? 1 : 0;

                PointF prevP = new PointF(playerTrailPair.Value[prevPIndex].X * visualState.GridSize, playerTrailPair.Value[prevPIndex].Y * visualState.GridSize);

                bool isHorizontal;
                SlimDX.Direct2D.Bitmap bitmapToDraw;
                if (p.X < prevP.X)
                {
                    isHorizontal = true;
                    bitmapToDraw = this.bikeW;
                }
                else if (p.Y > prevP.Y)
                {
                    isHorizontal = false;
                    bitmapToDraw = this.bikeS;
                }
                else if (p.Y < prevP.Y)
                {
                    isHorizontal = false;
                    bitmapToDraw = this.bikeN;
                }
                else
                {
                    isHorizontal = true;
                    bitmapToDraw = this.bikeE;
                }

                RectangleF destRect;
                if (isHorizontal)
                {
                    destRect = new RectangleF(p.X - visualState.GridSize2, p.Y - visualState.GridSize4, visualState.GridSize, visualState.GridSize2);
                }
                else
                {
                    destRect = new RectangleF(p.X - visualState.GridSize4, p.Y - visualState.GridSize2, visualState.GridSize2, visualState.GridSize);
                }

                renderTarget.DrawBitmap(bitmapToDraw, destRect);

                this.solidBrush.Opacity = 0.3f;
                this.solidBrush.Color = color;
                float elypseLarge = visualState.GridSize2 + 2;
                float elypseMid = visualState.GridSize4 + 2;
                float elypseSmall = visualState.GridSize4 - 2;
                renderTarget.FillEllipse(this.solidBrush, new Ellipse() { Center = p, RadiusX = isHorizontal ? elypseLarge : elypseMid, RadiusY = isHorizontal ? elypseMid : elypseLarge });
                renderTarget.FillEllipse(this.solidBrush, new Ellipse() { Center = p, RadiusX = isHorizontal ? elypseMid : elypseSmall, RadiusY = isHorizontal ? elypseSmall : elypseMid });
                this.solidBrush.Opacity = 0.5f;
                renderTarget.DrawText(
                    "Player " + playerTrailPair.Key,
                    new TextFormat(this.factory, "Courier", FontWeight.ExtraBold, SlimDX.DirectWrite.FontStyle.Normal, FontStretch.Normal, visualState.GridSize2, string.Empty),
                    new RectangleF(p.X + 20, p.Y - 20, 400, 50),
                    this.solidBrush);
            }
        }

        /// <summary>
        /// End rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        public void RenderEnd(RenderTarget renderTarget, VisualState visualState)
        {
        }

        /// <summary>
        /// Dispose drawer, release managed resources
        /// </summary>
        public void Dispose()
        {
            this.bikeN.Dispose();
            this.bikeS.Dispose();
            this.bikeE.Dispose();
            this.bikeW.Dispose();
            this.solidBrush.Dispose();
            this.factory.Dispose();
        }

        /// <summary>
        /// Gets list of players and teams
        /// </summary>
        /// <param name="visualState">visual state with data</param>
        /// <returns>list of players</returns>
        private Dictionary<char, char> GetPlayers(VisualState visualState)
        {
            Dictionary<char, char> players = new Dictionary<char, char>();
            for (int x = 0; x < visualState.GridLayer.Width; x++)
            {
                for (int y = 0; y < visualState.GridLayer.Height; y++)
                {
                    var bike = visualState.PlayersLayer[x, y] as Data.Visuals.Bike;
                    if (bike != null)
                    {
                        players.Add(bike.PlayerId, bike.TeamId);
                    }
                }
            }

            return players;
        }

        /// <summary>
        /// Gets trails for players
        /// </summary>
        /// <param name="visualState">visual state of the game</param>
        /// <param name="playersAndTeams">players with teams assignments</param>
        /// <returns>players trails</returns>
        private Dictionary<char, List<Point>> GetTrailPaths(VisualState visualState, Dictionary<char, char> playersAndTeams)
        {
            Dictionary<char, List<Point>> paths = new Dictionary<char, List<Point>>();
            foreach (KeyValuePair<char, char> playerTeamPair in playersAndTeams)
            {
                paths.Add(playerTeamPair.Key, this.GetTrailPath(playerTeamPair.Key, visualState));
            }

            return paths;
        }

        /// <summary>
        /// Gets trail for a specified player
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <param name="visualState">player visual state</param>
        /// <returns>players trail</returns>
        private List<Point> GetTrailPath(char playerId, VisualState visualState)
        {
            // sort trail by age
            SortedList trailParts = new SortedList();
            for (int x = 0; x < visualState.GridLayer.Width; x++)
            {
                for (int y = 0; y < visualState.GridLayer.Height; y++)
                {
                    var bike = visualState.PlayersLayer[x, y] as Data.Visuals.Bike;
                    if (bike != null && bike.PlayerId == playerId)
                    {
                        trailParts.Add(0, new Point(x, y));
                    }

                    var trail = visualState.TrailsLayer[x, y] as Data.Visuals.Trail;
                    if (trail != null && trail.PlayerId == playerId)
                    {
                        trailParts.Add(trail.Age, new Point(x, y));
                    }
                }
            }

            List<Point> results = new List<Point>();
            foreach (Point point in trailParts.Values)
            {
                results.Add(point);
            }

            return results;
        }
    }
}
