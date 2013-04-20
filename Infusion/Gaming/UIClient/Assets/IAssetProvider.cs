namespace UIClient.Assets
{
    using SlimDX.Direct2D;

    /// <summary>
    /// Provides graphical assets
    /// </summary>
    public interface IAssetProvider
    {
        /// <summary>
        /// Load direct 2D bitmap from the file
        /// </summary>
        /// <param name="fileName">name of the file to load</param>
        /// <param name="renderTarget">rendering target</param>
        /// <returns>direct 2D bitmap</returns>
        SlimDX.Direct2D.Bitmap LoadBitmap(string fileName, RenderTarget renderTarget);

        /// <summary>
        /// Load bitmap from System.Drawing bitmap
        /// </summary>
        /// <param name="drawingBitmap">system bitmap to load</param>
        /// <param name="renderTarget">rendering target</param>
        /// <returns>direct 2D bitmap</returns>
        SlimDX.Direct2D.Bitmap LoadBitmap(System.Drawing.Bitmap drawingBitmap, RenderTarget renderTarget);
    }
}
