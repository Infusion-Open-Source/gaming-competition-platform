namespace UIClient.Drawers
{
    using System.Drawing;
    using SlimDX;
    using SlimDX.Direct2D;
    using UIClient.Assets;
    using UIClient.Data;

    /// <summary>
    /// Draws map obstacles
    /// </summary>
    public class ObstaclesDrawer : IDrawer
    {
        /// <summary>
        /// bitmap with obstacle arc
        /// </summary>
        private SlimDX.Direct2D.Bitmap obstacleArcNE;

        /// <summary>
        /// bitmap with obstacle arc
        /// </summary>
        private SlimDX.Direct2D.Bitmap obstacleArcNW;

        /// <summary>
        /// bitmap with obstacle arc
        /// </summary>
        private SlimDX.Direct2D.Bitmap obstacleArcSE;

        /// <summary>
        /// bitmap with obstacle arc
        /// </summary>
        private SlimDX.Direct2D.Bitmap obstacleArcSW;

        /// <summary>
        /// bitmap with obstacle line
        /// </summary>
        private SlimDX.Direct2D.Bitmap obstacleLineH;

        /// <summary>
        /// bitmap with obstacle line
        /// </summary>
        private SlimDX.Direct2D.Bitmap obstacleLineV;

        /// <summary>
        /// Initialize drawer, create managed resources
        /// </summary>
        /// <param name="renderTarget">render target</param>
        /// <param name="assetProvider">provides assets</param>
        public void Initialize(RenderTarget renderTarget, IAssetProvider assetProvider)
        {
            this.obstacleArcNE = assetProvider.LoadBitmap("obstacleArcNE.png", renderTarget);
            this.obstacleArcNW = assetProvider.LoadBitmap("obstacleArcNW.png", renderTarget);
            this.obstacleArcSE = assetProvider.LoadBitmap("obstacleArcSE.png", renderTarget);
            this.obstacleArcSW = assetProvider.LoadBitmap("obstacleArcSW.png", renderTarget);
            this.obstacleLineH = assetProvider.LoadBitmap("obstacleLineH.png", renderTarget);
            this.obstacleLineV = assetProvider.LoadBitmap("obstacleLineV.png", renderTarget);
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
            for (int x = 0; x < visualState.GridLayer.Width; x++)
            {
                for (int y = 0; y < visualState.GridLayer.Height; y++)
                {
                    PointF p = new PointF(x * visualState.GridSize, y * visualState.GridSize);
                    bool obstacle = visualState.ObstaclesLayer[x, y] is Data.Visuals.Obstacle;
                    bool obstacleInN = !visualState.ObstaclesLayer.IsInRange(x, y - 1) || visualState.ObstaclesLayer[x, y - 1] is Data.Visuals.Obstacle;
                    bool obstacleInS = !visualState.ObstaclesLayer.IsInRange(x, y + 1) || visualState.ObstaclesLayer[x, y + 1] is Data.Visuals.Obstacle;
                    bool obstacleInE = !visualState.ObstaclesLayer.IsInRange(x + 1, y) || visualState.ObstaclesLayer[x + 1, y] is Data.Visuals.Obstacle;
                    bool obstacleInW = !visualState.ObstaclesLayer.IsInRange(x - 1, y) || visualState.ObstaclesLayer[x - 1, y] is Data.Visuals.Obstacle;

                    bool obstacleInNE = !visualState.ObstaclesLayer.IsInRange(x + 1, y - 1) || visualState.ObstaclesLayer[x + 1, y - 1] is Data.Visuals.Obstacle;
                    bool obstacleInNW = !visualState.ObstaclesLayer.IsInRange(x - 1, y - 1) || visualState.ObstaclesLayer[x - 1, y - 1] is Data.Visuals.Obstacle;
                    bool obstacleInSE = !visualState.ObstaclesLayer.IsInRange(x + 1, y + 1) || visualState.ObstaclesLayer[x + 1, y + 1] is Data.Visuals.Obstacle;
                    bool obstacleInSW = !visualState.ObstaclesLayer.IsInRange(x - 1, y + 1) || visualState.ObstaclesLayer[x - 1, y + 1] is Data.Visuals.Obstacle;
                    
                    if (obstacle)
                    {
                        // TODO: needs to be refactored to be human readable!

                        // +--+--+
                        // |##|  |
                        // +--+--+
                        // |  |  |
                        // +--+--+
                        var destRect = new RectangleF(p.X - visualState.GridSize2, p.Y - visualState.GridSize2, visualState.GridSize2, visualState.GridSize2);
                        if (obstacleInNW)
                        {
                            if (obstacleInW && obstacleInN)
                            {
                            }
                            else if (obstacleInW)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineH, destRect);
                            }
                            else if (obstacleInN)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineV, destRect);
                            }
                            else
                            {
                                renderTarget.DrawBitmap(this.obstacleArcSE, destRect);
                            }
                        }
                        else
                        {
                            if (obstacleInW && obstacleInN)
                            {
                                renderTarget.DrawBitmap(this.obstacleArcNW, destRect);
                            }
                            else if (obstacleInW)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineH, destRect);
                            }
                            else if (obstacleInN)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineV, destRect);
                            }
                            else
                            {
                                renderTarget.DrawBitmap(this.obstacleArcSE, destRect);
                            }
                        }

                        // +--+--+
                        // |  |##|
                        // +--+--+
                        // |  |  |
                        // +--+--+
                        destRect = new RectangleF(p.X, p.Y - visualState.GridSize2, visualState.GridSize2, visualState.GridSize2);
                        if (obstacleInNE)
                        {
                            if (obstacleInE && obstacleInN)
                            {
                            }
                            else if (obstacleInE)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineH, destRect);
                            }
                            else if (obstacleInN)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineV, destRect);
                            }
                            else
                            {
                                renderTarget.DrawBitmap(this.obstacleArcSW, destRect);
                            }
                        }
                        else
                        {
                            if (obstacleInE && obstacleInN)
                            {
                                renderTarget.DrawBitmap(this.obstacleArcNE, destRect);
                            }
                            else if (obstacleInE)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineH, destRect);
                            }
                            else if (obstacleInN)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineV, destRect);
                            }
                            else
                            {
                                renderTarget.DrawBitmap(this.obstacleArcSW, destRect);
                            }
                        }

                        // +--+--+
                        // |  |  |
                        // +--+--+
                        // |##|  |
                        // +--+--+
                        destRect = new RectangleF(p.X - visualState.GridSize2, p.Y, visualState.GridSize2, visualState.GridSize2);
                        if (obstacleInSW)
                        {
                            if (obstacleInW && obstacleInS)
                            {
                            }
                            else if (obstacleInW)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineH, destRect);
                            }
                            else if (obstacleInS)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineV, destRect);
                            }
                            else
                            {
                                renderTarget.DrawBitmap(this.obstacleArcNE, destRect);
                            }
                        }
                        else
                        {
                            if (obstacleInW && obstacleInS)
                            {
                                renderTarget.DrawBitmap(this.obstacleArcSW, destRect);
                            }
                            else if (obstacleInW)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineH, destRect);
                            }
                            else if (obstacleInS)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineV, destRect);
                            }
                            else
                            {
                                renderTarget.DrawBitmap(this.obstacleArcNE, destRect);
                            }
                        }

                        // +--+--+
                        // |  |  |
                        // +--+--+
                        // |  |##|
                        // +--+--+
                        destRect = new RectangleF(p.X, p.Y, visualState.GridSize2, visualState.GridSize2);
                        if (obstacleInSE)
                        {
                            if (obstacleInE && obstacleInS)
                            {
                            }
                            else if (obstacleInE)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineH, destRect);
                            }
                            else if (obstacleInS)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineV, destRect);
                            }
                            else
                            {
                                renderTarget.DrawBitmap(this.obstacleArcNW, destRect);
                            }
                        }
                        else
                        {
                            if (obstacleInE && obstacleInS)
                            {
                                renderTarget.DrawBitmap(this.obstacleArcSE, destRect);
                            }
                            else if (obstacleInE)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineH, destRect);
                            }
                            else if (obstacleInS)
                            {
                                renderTarget.DrawBitmap(this.obstacleLineV, destRect);
                            }
                            else
                            {
                                renderTarget.DrawBitmap(this.obstacleArcNW, destRect);
                            }
                        }
                    }
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
            this.obstacleArcNE.Dispose();
            this.obstacleArcNW.Dispose();
            this.obstacleArcSE.Dispose();
            this.obstacleArcSW.Dispose();
            this.obstacleLineH.Dispose();
            this.obstacleLineV.Dispose();
        }
    }
}
