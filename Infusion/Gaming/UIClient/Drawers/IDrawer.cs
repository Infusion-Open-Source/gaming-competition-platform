namespace UIClient.Drawers
{
    using System;
    using SlimDX.Direct2D;
    using UIClient.Assets;
    using UIClient.Data;

    /// <summary>
    /// common interface for layer drawers
    /// </summary>
    public interface IDrawer : IDisposable
    {
        /// <summary>
        /// Begin rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        void RenderBegin(RenderTarget renderTarget, VisualState visualState);

        /// <summary>
        /// Do the rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        void Render(RenderTarget renderTarget, VisualState visualState);

        /// <summary>
        /// End rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        void RenderEnd(RenderTarget renderTarget, VisualState visualState);

        /// <summary>
        /// Initialize drawer, create managed resources
        /// </summary>
        /// <param name="renderTarget">render target</param>
        /// <param name="assetProvider">provides assets</param>
        void Initialize(RenderTarget renderTarget, IAssetProvider assetProvider);
    }
}
