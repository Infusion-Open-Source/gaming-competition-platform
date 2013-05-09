namespace Infusion.Gaming.LightCycles.UIClient.Drawers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.UIClient.Assets;
    using Infusion.Gaming.LightCycles.UIClient.Data;
    using SlimDX;
    using SlimDX.Direct2D;

    /// <summary>
    /// Draws visual state
    /// </summary>
    public class VisualStateDrawer : IDrawer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStateDrawer" /> class.
        /// </summary>
        /// <param name="internalDrawers">set of internal drawers</param>
        /// <param name="windowDimensions">window dimensions rectangle</param>
        public VisualStateDrawer(IEnumerable<IDrawer> internalDrawers, RectangleF windowDimensions)
        {
            if (internalDrawers == null)
            {
                throw new ArgumentNullException("internalDrawers");
            }

            this.InternalDrawers = new List<IDrawer>(internalDrawers);
            this.GameDimensions = new RectangleF(0, 0, 0, 0);
            this.WindowDimensions = windowDimensions;
        }

        /// <summary>
        /// Gets or sets game view focus point
        /// </summary>
        public PointF FocusPoint { get; protected set; }
        
        /// <summary>
        /// Gets or sets game dimensions
        /// </summary>
        public RectangleF GameDimensions { get; protected set; }

        /// <summary>
        /// Gets game center point
        /// </summary>
        public PointF GameCenter
        {
            get
            {
                return new PointF(this.GameDimensions.Width / 2, this.GameDimensions.Height / 2);
            }
        }

        /// <summary>
        /// Gets or sets window dimensions
        /// </summary>
        public RectangleF WindowDimensions { get; protected set; }

        /// <summary>
        /// Gets window center point
        /// </summary>
        public PointF WindowCenter
        {
            get
            {
                return new PointF(this.WindowDimensions.Width / 2, this.WindowDimensions.Height / 2);
            }
        }

        /// <summary>
        /// Gets or sets internal drawers collection
        /// </summary>
        public List<IDrawer> InternalDrawers { get; protected set; }       

        /// <summary>
        /// Initialize drawer, create managed resources
        /// </summary>
        /// <param name="renderTarget">render target</param>
        /// <param name="assetProvider">provides assets</param>
        public void Initialize(RenderTarget renderTarget, IAssetProvider assetProvider)
        {
            foreach (IDrawer drawer in this.InternalDrawers)
            {
                drawer.Initialize(renderTarget, assetProvider);
            }            
        }

        /// <summary>
        /// Begin rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        public void RenderBegin(RenderTarget renderTarget, VisualState visualState)
        {
            // recalculate game area
            this.GameDimensions = new RectangleF(0, 0, visualState.GridLayer.Width * visualState.GridSize, visualState.GridLayer.Height * visualState.GridSize);
            this.FocusPoint = this.GameCenter;

            renderTarget.BeginDraw();
            renderTarget.Transform = Matrix3x2.Translation(-this.FocusPoint.X, -this.FocusPoint.Y) * Matrix3x2.Scale(1.0f, 1.0f) * Matrix3x2.Translation(this.WindowCenter.X, this.WindowCenter.Y);
            foreach (IDrawer drawer in this.InternalDrawers)
            {
                drawer.RenderBegin(renderTarget, visualState);
            }
        }

        /// <summary>
        /// Do the rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        public void Render(RenderTarget renderTarget, VisualState visualState)
        {
            foreach (IDrawer drawer in this.InternalDrawers)
            {
                drawer.Render(renderTarget, visualState);
            }
        }

        /// <summary>
        /// End rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        public void RenderEnd(RenderTarget renderTarget, VisualState visualState)
        {
            foreach (IDrawer drawer in this.InternalDrawers)
            {
                drawer.RenderEnd(renderTarget, visualState);
            }

            renderTarget.EndDraw();
        }

        /// <summary>
        /// Dispose drawer, release managed resources
        /// </summary>
        public void Dispose()
        {
            foreach (IDrawer drawer in this.InternalDrawers)
            {
                drawer.Dispose();
            }
        }
    }
}
