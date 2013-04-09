namespace UIClient.Drawers
{
    using System;
    using System.Collections.Generic;
    using SlimDX;
    using SlimDX.Direct2D;
    using UIClient.Assets;
    using UIClient.Data;

    /// <summary>
    /// Draws visual state
    /// </summary>
    public class VisualStateDrawer : IDrawer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStateDrawer" /> class.
        /// </summary>
        /// <param name="internalDrawers">set of internal drawers</param>
        public VisualStateDrawer(IEnumerable<IDrawer> internalDrawers)
        {
            if (internalDrawers == null)
            {
                throw new ArgumentNullException("internalDrawers");
            }

            this.InternalDrawers = new List<IDrawer>(internalDrawers);
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
            renderTarget.BeginDraw();
            renderTarget.Transform = Matrix3x2.Identity;
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
