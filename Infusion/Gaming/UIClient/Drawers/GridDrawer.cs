namespace Infusion.Gaming.LightCycles.UIClient.Drawers
{
    using System.Drawing;
    using Infusion.Gaming.LightCycles.UIClient.Assets;
    using Infusion.Gaming.LightCycles.UIClient.Data;
    using Infusion.Gaming.LightCycles.UIClient.Data.Visuals;
    using SlimDX;
    using SlimDX.Direct2D;

    /// <summary>
    /// Draws a map grid
    /// </summary>
    public class GridDrawer : IDrawer
    {
        /// <summary>
        /// opacity of the grid
        /// </summary>
        private const float GridOpacity = 0.25f;

        /// <summary>
        /// color of the grid
        /// </summary>
        private readonly Color4 gridColor = new Color4(0.0f, 1.0f, 1.0f);
        
        /// <summary>
        /// brush for grid drawing
        /// </summary>
        private SolidColorBrush solidBrush;

        /// <summary>
        /// Initialize drawer, create managed resources
        /// </summary>
        /// <param name="renderTarget">render target</param>
        /// <param name="assetProvider">provides assets</param>
        public void Initialize(RenderTarget renderTarget, IAssetProvider assetProvider)
        {
            this.solidBrush = new SolidColorBrush(
                renderTarget, 
                this.gridColor,
                new BrushProperties
                    {
                        Opacity = GridOpacity
                    });
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
            PointF c = new PointF(0, 0);
            PointF nw = new PointF(-visualState.GridSize2, -visualState.GridSize2);
            PointF n = new PointF(0, -visualState.GridSize2);
            PointF ne = new PointF(visualState.GridSize2, -visualState.GridSize2);
            PointF se = new PointF(visualState.GridSize2, visualState.GridSize2);
            PointF s = new PointF(0, visualState.GridSize2);
            PointF sw = new PointF(-visualState.GridSize2, visualState.GridSize2);
            PointF e = new PointF(visualState.GridSize2, 0);
            PointF w = new PointF(-visualState.GridSize2, 0);

            for (int x = 0; x < visualState.GridLayer.Width; x++)
            {
                for (int y = 0; y < visualState.GridLayer.Height; y++)
                {
                    PointF p = new PointF(x * visualState.GridSize, y * visualState.GridSize);
                    bool hasC = visualState.GridLayer[x, y] is Grid;
                    bool hasN = visualState.GridLayer.IsInRange(x, y - 1) && visualState.GridLayer[x, y - 1] is Grid;
                    bool hasS = visualState.GridLayer.IsInRange(x, y + 1) && visualState.GridLayer[x, y + 1] is Grid;
                    bool hasE = visualState.GridLayer.IsInRange(x + 1, y) && visualState.GridLayer[x + 1, y] is Grid;
                    bool hasW = visualState.GridLayer.IsInRange(x - 1, y) && visualState.GridLayer[x - 1, y] is Grid;

                    if (hasC)
                    {
                        if (hasN)
                        {
                            renderTarget.DrawLine(this.solidBrush, p.X + n.X, p.Y + n.Y, p.X + c.X, p.Y + c.Y, 1f);
                        }

                        if (hasS)
                        {
                            renderTarget.DrawLine(this.solidBrush, p.X + s.X, p.Y + s.Y, p.X + c.X, p.Y + c.Y, 1f);
                        }

                        if (hasE)
                        {
                            renderTarget.DrawLine(this.solidBrush, p.X + e.X, p.Y + e.Y, p.X + c.X, p.Y + c.Y, 1f);
                        }

                        if (hasW)
                        {
                            renderTarget.DrawLine(this.solidBrush, p.X + w.X, p.Y + w.Y, p.X + c.X, p.Y + c.Y, 1f);
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
            this.solidBrush.Dispose();
        }
    }
}
