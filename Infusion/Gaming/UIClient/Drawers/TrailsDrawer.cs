namespace UIClient.Drawers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using SlimDX;
    using SlimDX.Direct2D;
    using UIClient.Assets;
    using UIClient.Data;

    /// <summary>
    /// Draws trails layer
    /// </summary>
    public class TrailsDrawer : IDrawer
    {
        /// <summary>
        /// brush used for drawing
        /// </summary>
        private SolidColorBrush solidBrush;

        /// <summary>
        /// TODO: this needs to be moved to separate common class
        /// dictionary with team colors
        /// </summary>
        private Dictionary<char, Color> teamColors = new Dictionary<char, Color>
        {
            { 'A', Color.FromArgb(255, 0, 0) },
            { 'B', Color.FromArgb(0, 255, 0) },
            { 'C', Color.FromArgb(0, 0, 255) },
            { 'D', Color.FromArgb(255, 255, 0) },
            { 'E', Color.FromArgb(255, 0, 255) },
            { 'F', Color.FromArgb(0, 255, 255) },
            { 'G', Color.FromArgb(255, 127, 0) },
            { 'H', Color.FromArgb(255, 0, 127) },
            { 'I', Color.FromArgb(127, 0, 255) },
            { 'J', Color.FromArgb(0, 255, 127) },
            { 'K', Color.FromArgb(0, 127, 255) },
            { 'L', Color.FromArgb(127, 255, 0) },
        };

        /// <summary>
        /// Initialize drawer, create managed resources
        /// </summary>
        /// <param name="renderTarget">render target</param>
        /// <param name="assetProvider">provides assets</param>
        public void Initialize(RenderTarget renderTarget, IAssetProvider assetProvider)
        {
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
            var playersAndTeams = this.GetPlayers(visualState);
            foreach (KeyValuePair<char, List<Point>> playerTrailPair in this.GetTrailPaths(visualState, playersAndTeams))
            {
                PointF prevP = PointF.Empty;
                for (int i = 0; i < playerTrailPair.Value.Count; i++)
                {
                    Point point = playerTrailPair.Value[i];
                    PointF p = new PointF(visualState.BorderSize + (point.X * visualState.GridSize), visualState.BorderSize + (point.Y * visualState.GridSize));

                    if (prevP == PointF.Empty)
                    {
                        prevP = p;
                        continue;
                    }

                    var color = this.teamColors[playersAndTeams[playerTrailPair.Key]];
                    this.DrawTrailPart(renderTarget, prevP, p, color, new PointF(0, 0), i, playerTrailPair.Value.Count, 1.0f);
                    this.DrawTrailPart(renderTarget, prevP, p, color, new PointF(1.5f, 3), i, playerTrailPair.Value.Count, 0.3f);
                    this.DrawTrailPart(renderTarget, prevP, p, color, new PointF(3, 6), i, playerTrailPair.Value.Count, 1.0f);
                    prevP = p;
                }
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

        /// <summary>
        /// Craws part of the trail
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="prevP">previous point</param>
        /// <param name="p">current point</param>
        /// <param name="color">color of trail</param>
        /// <param name="shiftVector">vector of trail shift</param>
        /// <param name="partNumber">index of trail in sequence</param>
        /// <param name="numberOfparts">length of trail</param>
        /// <param name="opacityModifier">modifies overall opacity of trail</param>
        private void DrawTrailPart(RenderTarget renderTarget, PointF prevP, PointF p, Color color, PointF shiftVector, int partNumber, int numberOfparts, float opacityModifier)
        {
            float pathProgress = 1;
            const int TailLength = 10;
            int tail = numberOfparts - partNumber;
            if (tail <= TailLength)
            {
                pathProgress = (float)Math.Pow((1.0f * tail) / TailLength, 1);
            }

            this.solidBrush.Color = color;
            this.solidBrush.Opacity = 0.1f * pathProgress * opacityModifier;
            renderTarget.DrawLine(this.solidBrush, prevP.X + shiftVector.X, prevP.Y + shiftVector.Y, p.X + shiftVector.X, p.Y + shiftVector.Y, 9.0f);
            this.solidBrush.Opacity = 0.2f * pathProgress * opacityModifier;
            renderTarget.DrawLine(this.solidBrush, prevP.X + shiftVector.X, prevP.Y + shiftVector.Y, p.X + shiftVector.X, p.Y + shiftVector.Y, 7.0f);
            this.solidBrush.Opacity = 0.3f * pathProgress * opacityModifier;
            renderTarget.DrawLine(this.solidBrush, prevP.X + shiftVector.X, prevP.Y + shiftVector.Y, p.X + shiftVector.X, p.Y + shiftVector.Y, 3.0f);
            this.solidBrush.Opacity = 1.0f * pathProgress * opacityModifier;
            renderTarget.DrawLine(this.solidBrush, prevP.X + shiftVector.X, prevP.Y + shiftVector.Y, p.X + shiftVector.X, p.Y + shiftVector.Y, 1.0f);
        }
    }
}
