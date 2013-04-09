namespace UIClient
{
    using System.Collections.Generic;
    using SlimDX.SampleFramework;
    using SlimDX.SampleFramework.Rendering;
    using UIClient.Assets;
    using UIClient.Drawers;

    /// <summary>
    /// Game view control
    /// </summary>
    public class GameView : BaseView
    {
        /// <summary>
        /// path with assets
        /// </summary>
        private const string AssetsPath = @"..\..\..\Assets";

        /// <summary>
        /// sync root for visual state reference passing 
        /// </summary>
        private readonly object visualStateSyncRoot = new object();

        /// <summary>
        /// game visual state
        /// </summary>
        private Data.VisualState visualState;

        /// <summary>
        /// game visual state used for drawing
        /// </summary>
        private Data.VisualState visualStateToDraw;

        /// <summary>
        /// game visual state drawer
        /// </summary>
        private IDrawer visualstateDrawer;

        /// <summary>
        /// provider of game assets
        /// </summary>
        private IAssetProvider assetProvider;
        
        /// <summary>
        /// Update reference of current game visual state
        /// </summary>
        /// <param name="newVisualState">visual state of the game</param>
        public void UpdateVisualState(Data.VisualState newVisualState)
        {
            lock (this.visualStateSyncRoot)
            {
                this.visualState = newVisualState;
            }
        }
        
        /// <summary>
        /// Moves game visual state reference to drawing thread
        /// </summary>
        public void MoveVisualStateToDrawing()
        {
            lock (this.visualStateSyncRoot)
            {
                if (this.visualState != null)
                {
                    this.visualStateToDraw = this.visualState;
                    this.visualState = null;
                }
            }
        }

        /// <summary>
        /// Dispose resources
        /// </summary>
        /// <param name="disposeManagedResources">should dispose managed resources</param>
        protected override void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                if (this.visualstateDrawer != null)
                {
                    this.visualstateDrawer.Dispose();
                }
            }

            base.Dispose(disposeManagedResources);
        }

        /// <summary>
        /// Configure view
        /// </summary>
        /// <returns>view configuration</returns>
        protected override ViewConfiguration OnConfigure()
        {
            return new ViewConfiguration()
            {
                WindowWidth = 1575,
                WindowHeight = 675,
                WindowTitle = "LightCycles UI"
            };
        }

        /// <summary>
        /// Initialize view
        /// </summary>
        protected override void OnInitialize()
        {
            DeviceSettings2D settings = new DeviceSettings2D
            {
                Width = WindowWidth,
                Height = WindowHeight
            };

            this.InitializeDevice(settings);
            this.assetProvider = new AssetProvider(AssetsPath);
            this.visualstateDrawer = new VisualStateDrawer(new List<IDrawer> 
            {
                new BackgroundDrawer(),
                new ObstaclesDrawer(),
                new GridDrawer(),
                new TrailsDrawer(),
                new PlayersDrawer(),
                new UserInterfaceDrawer()
            });

            this.visualstateDrawer.Initialize(Context2D.RenderTarget, this.assetProvider);
        }

        /// <summary>
        /// On begin rendering
        /// </summary>
        protected override void OnRenderBegin()
        {
            this.MoveVisualStateToDrawing();
            if (this.visualstateDrawer != null)
            {
                this.visualstateDrawer.RenderBegin(Context2D.RenderTarget, this.visualStateToDraw);
            }
        }
        
        /// <summary>
        /// Render control
        /// </summary>
        protected override void OnRender()
        {
            if (this.visualstateDrawer != null)
            {
                this.visualstateDrawer.Render(Context2D.RenderTarget, this.visualStateToDraw);
            }
        }
        
        /// <summary>
        /// End control rendering
        /// </summary>
        protected override void OnRenderEnd()
        {
            if (this.visualstateDrawer != null)
            {
                this.visualstateDrawer.RenderEnd(Context2D.RenderTarget, this.visualStateToDraw);
            }
        }
    }
}
