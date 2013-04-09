namespace UIClient.Drawers
{
    using SlimDX;
    using SlimDX.Direct2D;
    using UIClient.Assets;
    using UIClient.Data;

    /// <summary>
    /// Draws background layer
    /// </summary>
    public class BackgroundDrawer : IDrawer
    {
        /// <summary>
        /// color of the background
        /// </summary>
        private readonly Color4 backgroundColor = new Color4(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// background drawer brush
        /// </summary>
        private SolidColorBrush solidBrush;
        
        /// <summary>
        /// Initialize drawer, create managed resources
        /// </summary>
        /// <param name="renderTarget">render target</param>
        /// <param name="assetProvider">provides assets</param>
        public void Initialize(RenderTarget renderTarget, IAssetProvider assetProvider)
        {
            this.solidBrush = new SolidColorBrush(renderTarget, this.backgroundColor);
        }

        /// <summary>
        /// Begin rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        public void RenderBegin(RenderTarget renderTarget, VisualState visualState)
        {
            renderTarget.Clear(this.backgroundColor);
        }

        /// <summary>
        /// Do the rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        public void Render(RenderTarget renderTarget, VisualState visualState)
        {
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
