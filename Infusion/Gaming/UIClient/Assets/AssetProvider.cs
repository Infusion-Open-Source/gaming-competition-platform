namespace UIClient.Assets
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using SlimDX;
    using SlimDX.Direct2D;

    /// <summary>
    /// Basic assets provider 
    /// </summary>
    public class AssetProvider : IAssetProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetProvider"/> class.
        /// </summary>
        /// <param name="assetsPath">assets root path</param>
        public AssetProvider(string assetsPath)
        {
            if (!Directory.Exists(assetsPath))
            {
                throw new DirectoryNotFoundException();
            }

            this.AssetsPath = assetsPath;
        }

        /// <summary>
        /// Gets or sets assets path
        /// </summary>
        public string AssetsPath { get; protected set; }

        /// <summary>
        /// Load direct 2D bitmap from the file
        /// </summary>
        /// <param name="fileName">name of the file to load</param>
        /// <param name="renderTarget">rendering target</param>
        /// <returns>direct 2D bitmap</returns>
        public SlimDX.Direct2D.Bitmap LoadBitmap(string fileName, RenderTarget renderTarget)
        {
            SlimDX.Direct2D.Bitmap result;
            using (System.Drawing.Bitmap drawBitmap = new System.Drawing.Bitmap(Path.Combine(this.AssetsPath, fileName)))
            {
                this.ReplaceColor(drawBitmap, Color.FromArgb(255, 0, 0, 0), Color.FromArgb(0, 0, 0, 0));
                result = this.LoadBitmap(drawBitmap, renderTarget);
            }

            return result;
        }

        /// <summary>
        /// Load bitmap from System.Drawing bitmap
        /// </summary>
        /// <param name="drawingBitmap">system bitmap to load</param>
        /// <param name="renderTarget">rendering target</param>
        /// <returns>direct 2D bitmap</returns>
        public SlimDX.Direct2D.Bitmap LoadBitmap(System.Drawing.Bitmap drawingBitmap, RenderTarget renderTarget)
        {
            SlimDX.Direct2D.Bitmap result;

            // Lock the gdi resource
            BitmapData drawingBitmapData = drawingBitmap.LockBits(new Rectangle(0, 0, drawingBitmap.Width, drawingBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Prepare loading the image from gdi resource
            using (DataStream dataStream = new DataStream(drawingBitmapData.Scan0, drawingBitmapData.Stride * drawingBitmapData.Height, true, false))
            {
                SlimDX.Direct2D.BitmapProperties properties = new SlimDX.Direct2D.BitmapProperties();
                properties.PixelFormat = new SlimDX.Direct2D.PixelFormat(SlimDX.DXGI.Format.B8G8R8A8_UNorm, SlimDX.Direct2D.AlphaMode.Premultiplied);

                // Load the image from the gdi resource
                result = new SlimDX.Direct2D.Bitmap(renderTarget, new Size(drawingBitmap.Width, drawingBitmap.Height), dataStream, drawingBitmapData.Stride, properties);
                dataStream.Close();
            }
            
            // Unlock the gdi resource
            drawingBitmap.UnlockBits(drawingBitmapData);
            return result;
        }

        /// <summary>
        /// Replaces one color in bitmap with another
        /// </summary>
        /// <param name="bitmap">bitmap on which color should be replaced</param>
        /// <param name="colorToReplace">color to be replaced</param>
        /// <param name="color">new color</param>
        public void ReplaceColor(System.Drawing.Bitmap bitmap, Color colorToReplace, Color color)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    if (bitmap.GetPixel(x, y) == colorToReplace)
                    {
                        bitmap.SetPixel(x, y, color);
                    }
                }
            }
        }
    }
}
