namespace UIClient.Drawers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using SlimDX;
    using SlimDX.Direct2D;
    using UIClient.Assets;
    using UIClient.Data;

    /// <summary>
    /// Draws players layer 
    /// </summary>
    public class PlayersDrawer : IDrawer
    {
        /// <summary>
        /// brush to draw bike
        /// </summary>
        private SolidColorBrush solidBrush;

        /// <summary>
        /// TODO: this need to be moved to common separate class
        /// dictionary of team colors
        /// </summary>
        private Dictionary<char, Color> teamColors = new Dictionary<char, Color>
        {
            { 'A', Color.Red },
            { 'B', Color.Blue },
            { 'C', Color.Yellow },
            { 'D', Color.YellowGreen },
            { 'E', Color.Green },
            { 'F', Color.Violet },
            { 'G', Color.Orange },
            { 'H', Color.Tomato },
            { 'I', Color.Aqua },
            { 'J', Color.Coral }
        };

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
            float w2 = visualState.GridSize / 2;
            var playersAndTeams = this.GetPlayers(visualState);
            foreach (KeyValuePair<char, List<Point>> playerTrailPair in this.GetTrailPaths(visualState, playersAndTeams))
            {
                var color = this.teamColors[playersAndTeams[playerTrailPair.Key]];
                PointF p = new PointF(
                    visualState.BorderSize + (playerTrailPair.Value[0].X * visualState.GridSize),
                    visualState.BorderSize + (playerTrailPair.Value[0].Y * visualState.GridSize));
                var destRect = new RectangleF(p.X - w2, p.Y - w2, visualState.GridSize, visualState.GridSize);

                int prevPIndex = (playerTrailPair.Value.Count > 1) ? 1 : 0;

                PointF prevP = new PointF(
                    visualState.BorderSize + (playerTrailPair.Value[prevPIndex].X * visualState.GridSize),
                    visualState.BorderSize + (playerTrailPair.Value[prevPIndex].Y * visualState.GridSize));

                bool isHorizontal;
                if (p.X < prevP.X)
                {
                    isHorizontal = true;
                    renderTarget.DrawBitmap(this.bikeW, destRect);
                }
                else if (p.Y > prevP.Y)
                {
                    isHorizontal = false;
                    renderTarget.DrawBitmap(this.bikeS, destRect);
                }
                else if (p.Y < prevP.Y)
                {
                    isHorizontal = false;
                    renderTarget.DrawBitmap(this.bikeN, destRect);
                }
                else
                {
                    isHorizontal = true;
                    renderTarget.DrawBitmap(this.bikeE, destRect);
                }

                this.solidBrush.Opacity = 0.2f;
                this.solidBrush.Color = color;
                renderTarget.FillEllipse(this.solidBrush, new Ellipse() { Center = p, RadiusX = isHorizontal ? 20 : 15, RadiusY = isHorizontal ? 15 : 20 });
                renderTarget.FillEllipse(this.solidBrush, new Ellipse() { Center = p, RadiusX = isHorizontal ? 15 : 10, RadiusY = isHorizontal ? 10 : 15 });
                renderTarget.FillEllipse(this.solidBrush, new Ellipse() { Center = p, RadiusX = isHorizontal ? 10 : 5, RadiusY = isHorizontal ? 5 : 10 }); 
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
