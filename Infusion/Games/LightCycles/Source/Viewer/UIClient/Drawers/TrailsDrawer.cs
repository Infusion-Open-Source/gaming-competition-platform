namespace Infusion.Gaming.LightCycles.UIClient.Drawers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.UIClient.Assets;
    using Infusion.Gaming.LightCycles.UIClient.Data;
    using Infusion.Gaming.LightCycles.UIClient.Data.Visuals;
    using SlimDX;
    using SlimDX.Direct2D;

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
        /// <param name="settings">playback settings</param>
        public void RenderBegin(RenderTarget renderTarget, VisualState visualState, PlaybackSettings settings)
        {
        }

        /// <summary>
        /// Do the rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        /// <param name="settings">playback settings</param>
        public void Render(RenderTarget renderTarget, VisualState visualState, PlaybackSettings settings)
        {
            var bikes = this.GetBikes(visualState);
            foreach (KeyValuePair<Bike, List<Point>> bikeTrailPair in this.GetTrailPaths(visualState, bikes))
            {
                PointF prevP = PointF.Empty;
                for (int i = 0; i < bikeTrailPair.Value.Count; i++)
                {
                    GameIdentity playerId = bikeTrailPair.Key.PlayerId;
                    Point point = bikeTrailPair.Value[i];
                    PointF p = new PointF(point.X * visualState.GridSize, point.Y * visualState.GridSize);

                    if (prevP == PointF.Empty)
                    {
                        prevP = p;
                        continue;
                    }

                    float trailLarge = 3.0f;
                    float trailSmall = 1.5f;
                    float trail = visualState.GridSize > 7 ? trailLarge : trailSmall;
                    float trail2 = trail / 2;
                    
                    Color color = playerId.Color;
                    this.DrawTrailPart(renderTarget, prevP, p, color, new PointF(-trail2, -trail), i, bikeTrailPair.Value.Count, 1.0f);
                    this.DrawTrailPart(renderTarget, prevP, p, color, new PointF(0.0f, 0.0f), i, bikeTrailPair.Value.Count, 0.3f);
                    this.DrawTrailPart(renderTarget, prevP, p, color, new PointF(trail2, trail), i, bikeTrailPair.Value.Count, 1.0f);
                    prevP = p;
                }
            }
        }
        
        /// <summary>
        /// End rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        /// <param name="settings">playback settings</param>
        public void RenderEnd(RenderTarget renderTarget, VisualState visualState, PlaybackSettings settings)
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
        /// Gets list of bikes
        /// </summary>
        /// <param name="visualState">visual state with data</param>
        /// <returns>list of bikes</returns>
        private List<Bike> GetBikes(VisualState visualState)
        {
            List<Bike> bikes = new List<Bike>();
            for (int x = 0; x < visualState.GridLayer.Width; x++)
            {
                for (int y = 0; y < visualState.GridLayer.Height; y++)
                {
                    var bike = visualState.PlayersLayer[x, y] as Bike;
                    if (bike != null)
                    {
                        bikes.Add(bike);
                    }
                }
            }

            return bikes;
        }

        /// <summary>
        /// Gets trails for players
        /// </summary>
        /// <param name="visualState">visual state of the game</param>
        /// <param name="playersAndTeams">players with teams assignments</param>
        /// <returns>players trails</returns>
        private Dictionary<Bike, List<Point>> GetTrailPaths(VisualState visualState, List<Bike> playersAndTeams)
        {
            Dictionary<Bike, List<Point>> paths = new Dictionary<Bike, List<Point>>();
            foreach (Bike bike in playersAndTeams)
            {
                paths.Add(bike, this.GetTrailPath(bike, visualState));
            }

            return paths;
        }

        /// <summary>
        /// Gets trail for a specified player
        /// </summary>
        /// <param name="playersBike">players bike</param>
        /// <param name="visualState">player visual state</param>
        /// <returns>players trail</returns>
        private List<Point> GetTrailPath(Bike playersBike, VisualState visualState)
        {
            // sort trail by age
            SortedList trailParts = new SortedList();
            for (int x = 0; x < visualState.GridLayer.Width; x++)
            {
                for (int y = 0; y < visualState.GridLayer.Height; y++)
                {
                    var bike = visualState.PlayersLayer[x, y] as Data.Visuals.Bike;
                    if (bike != null && bike.PlayerId.Id == playersBike.PlayerId.Id)
                    {
                        trailParts.Add(1, new Point(x, y));
                    }

                    var trail = visualState.TrailsLayer[x, y] as Data.Visuals.Trail;
                    if (trail != null && trail.PlayerId.Id == playersBike.PlayerId.Id)
                    {
                        int i = 0;
                        while (trailParts.ContainsKey(trail.Age + i))
                        {
                            i++;
                        }

                        trailParts.Add(trail.Age + i, new Point(x, y));
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

            var strokeStyle = new StrokeStyle(renderTarget.Factory, new StrokeStyleProperties { LineJoin = LineJoin.Round });

            this.solidBrush.Color = color;
            this.solidBrush.Opacity = 0.1f * pathProgress * opacityModifier;
            renderTarget.DrawLine(this.solidBrush, prevP.X + shiftVector.X, prevP.Y + shiftVector.Y, p.X + shiftVector.X, p.Y + shiftVector.Y, 9.0f, strokeStyle);
            this.solidBrush.Opacity = 0.2f * pathProgress * opacityModifier;
            renderTarget.DrawLine(this.solidBrush, prevP.X + shiftVector.X, prevP.Y + shiftVector.Y, p.X + shiftVector.X, p.Y + shiftVector.Y, 7.0f, strokeStyle);
            this.solidBrush.Opacity = 0.3f * pathProgress * opacityModifier;
            renderTarget.DrawLine(this.solidBrush, prevP.X + shiftVector.X, prevP.Y + shiftVector.Y, p.X + shiftVector.X, p.Y + shiftVector.Y, 3.0f, strokeStyle);
            this.solidBrush.Opacity = 1.0f * pathProgress * opacityModifier;
            renderTarget.DrawLine(this.solidBrush, prevP.X + shiftVector.X, prevP.Y + shiftVector.Y, p.X + shiftVector.X, p.Y + shiftVector.Y, 1.0f, strokeStyle);
        }
    }
}
