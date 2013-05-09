namespace Infusion.Gaming.LightCycles.UIClient.Drawers
{
    using System.Drawing;
    using Infusion.Gaming.LightCycles.UIClient.Assets;
    using Infusion.Gaming.LightCycles.UIClient.Data;
    using Infusion.Gaming.LightCycles.UIClient.Data.Visuals;
    using SlimDX;
    using SlimDX.Direct2D;
    using SlimDX.DirectWrite;

    /// <summary>
    /// Draws user interface layer
    /// </summary>
    public class UserInterfaceDrawer : IDrawer
    {
        /// <summary>
        /// Transform matrix of a render target
        /// </summary>
        private Matrix3x2 transformMatrix;

        /// <summary>
        /// DirectWrite factory for creation of rendered text
        /// </summary>
        private SlimDX.DirectWrite.Factory factory;

        /// <summary>
        /// brush to draw bike
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
            this.factory = new SlimDX.DirectWrite.Factory(SlimDX.DirectWrite.FactoryType.Isolated);
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
            this.transformMatrix = renderTarget.Transform;
            renderTarget.Transform = Matrix3x2.Identity;

            foreach (IVisual visual in visualState.UserInterfaceLayer)
            {
                VisualText visualText = visual as VisualText;
                if (visualText != null)
                {
                    this.solidBrush.Color = visualText.Color;
                    this.solidBrush.Opacity = visualText.Opacity;
                    
                    renderTarget.DrawText(
                        visualText.Text,
                        new TextFormat(this.factory, "Courier", FontWeight.ExtraBold, SlimDX.DirectWrite.FontStyle.Normal, FontStretch.Normal, visualText.FontSize, string.Empty),
                        new RectangleF(visualText.Location.X, visualText.Location.Y, 1000, 200),
                        this.solidBrush, 
                        DrawTextOptions.Clip, 
                        MeasuringMethod.GdiClassic);
                }

                Mask mask = visual as Mask;
                if (mask != null)
                {
                    this.solidBrush.Color = mask.Color;
                    this.solidBrush.Opacity = mask.Opacity;
                    
                    renderTarget.FillRectangle(this.solidBrush, mask.Rectangle);
                }
            }

            renderTarget.Transform = this.transformMatrix;
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
            this.factory.Dispose();
        }
    }
}
