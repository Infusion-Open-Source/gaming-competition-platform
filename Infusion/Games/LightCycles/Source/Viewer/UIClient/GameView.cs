namespace Infusion.Gaming.LightCycles.UIClient
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Infusion.Gaming.LightCycles.UIClient.Assets;
    using Infusion.Gaming.LightCycles.UIClient.Drawers;
    using SlimDX.SampleFramework;
    using SlimDX.SampleFramework.Rendering;

    /// <summary>
    /// Game view control
    /// </summary>
    public class GameView : BaseView
    {
        /// <summary>
        /// path with assets
        /// </summary>
        private const string AssetsPath = @"Assets";

        /// <summary>
        /// sync root for visual state reference passing 
        /// </summary>
        private readonly object visualStateSyncRoot = new object();

        /// <summary>
        /// sync root for settings reference passing 
        /// </summary>
        private readonly object settingsSyncRoot = new object();

        /// <summary>
        /// internal playback settings
        /// </summary>
        private PlaybackSettings playbackSettings;

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
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        /// <param name="playbackSettings">playback settings</param>
        public GameView(PlaybackSettings playbackSettings)
        {
            if (playbackSettings == null)
            {
                throw new ArgumentNullException("playbackSettings");
            }

            this.playbackSettings = playbackSettings;
        }

        /// <summary>
        /// Gets or sets a value indicating whether view has been initialized
        /// </summary>
        public bool IsInitialized { get; protected set; }

        /// <summary>
        /// Gets playback settings
        /// </summary>
        public PlaybackSettings PlaybackSettings
        {
            get 
            {
                PlaybackSettings settigsCopy;
                lock (this.settingsSyncRoot)
                {
                    settigsCopy = this.playbackSettings.Clone();
                }

                return settigsCopy;
            }
        }

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
                WindowWidth = 1280,
                WindowHeight = 800,
                WindowTitle = "LightCycles"
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
            this.visualstateDrawer = new VisualStateDrawer(
                new List<IDrawer> 
                {
                    new BackgroundDrawer(),
                    new ObstaclesDrawer(),
                    new GridDrawer(),
                    new TrailsDrawer(),
                    new PlayersDrawer(),
                    new UserInterfaceDrawer()
                },
                new RectangleF(0, 0, settings.Width, settings.Height));

            this.visualstateDrawer.Initialize(Context2D.RenderTarget, this.assetProvider);

            lock (this.visualStateSyncRoot)
            {
                this.IsInitialized = true;
            }

            this.SetHotkeys();
        }

        /// <summary>
        /// Sets up hotkeys
        /// </summary>
        protected void SetHotkeys()
        {
            this.BaseControl.KeyUp += delegate(object sender, KeyEventArgs args)
                {
                    PlaybackSettings newSettigs = this.playbackSettings.Clone();
                    if (args.KeyCode == Keys.Left)
                    {
                        newSettigs.PanX -= 1;
                    } 
                    else if (args.KeyCode == Keys.Right)
                    {
                        newSettigs.PanX += 1;
                    }
                    else if (args.KeyCode == Keys.Up)
                    {
                        newSettigs.PanY -= 1;
                    }
                    else if (args.KeyCode == Keys.Down)
                    {
                        newSettigs.PanY += 1;
                    }
                    else if (args.KeyCode == Keys.PageUp)
                    {
                        newSettigs.Zoom -= 1;
                    }
                    else if (args.KeyCode == Keys.PageDown)
                    {
                        newSettigs.Zoom += 1;
                    }
                    else if (args.KeyCode == Keys.Home)
                    {
                        newSettigs.Zoom -= 10;
                    }
                    else if (args.KeyCode == Keys.End) 
                    {
                        newSettigs.Zoom = 1.0f;
                        newSettigs.PanX = 0;
                        newSettigs.PanY = 0;
                    }
                    else if (args.KeyCode == Keys.S)
                    {
                        newSettigs.WaitOnStart = !newSettigs.WaitOnStart; 
                        newSettigs.SpaceHitsCount += 1;
                    }
                    else if (args.KeyCode == Keys.E)
                    {
                        newSettigs.WaitOnEnd = !newSettigs.WaitOnEnd; 
                        newSettigs.SpaceHitsCount += 1;
                    }
                    else if (args.KeyCode == Keys.T)
                    {
                        newSettigs.WaitOnTurn = !newSettigs.WaitOnTurn; 
                        newSettigs.SpaceHitsCount += 1;
                    }
                    else if (args.KeyCode == Keys.N)
                    {
                        newSettigs.ShowNames = !newSettigs.ShowNames;
                    }
                    else if (args.KeyCode == Keys.F)
                    {
                        newSettigs.FollowMode = !newSettigs.FollowMode; 
                        if (newSettigs.FollowMode)
                        {
                            newSettigs.TurnOnFollowing();
                        }
                        else
                        {
                            newSettigs.TurnOffFollowing();
                        }
                    }
                    else if (args.KeyCode == Keys.OemCloseBrackets)
                    {
                        newSettigs.FollowedPlayerIndex++;
                    }
                    else if (args.KeyCode == Keys.OemOpenBrackets)
                    {
                        newSettigs.FollowedPlayerIndex--;
                    }
                    else if (args.KeyCode == Keys.Oemcomma)
                    {
                        newSettigs.TurnDelayTime += 10;
                    }
                    else if (args.KeyCode == Keys.OemPeriod)
                    {
                        newSettigs.TurnDelayTime -= 10;
                    }
                    else if (args.KeyCode == Keys.Space)
                    {
                        newSettigs.SpaceHitsCount += 1;
                    }
                    else if (args.KeyCode == Keys.Back)
                    {
                        newSettigs.BackHitsCount += 1;
                    }

                    if (newSettigs.TurnDelayTime < 10)
                    {
                        newSettigs.TurnDelayTime = 10;
                    }
                    
                    lock (this.settingsSyncRoot)
                    {
                        this.playbackSettings = newSettigs;
                    }
                };
        }

        /// <summary>
        /// On begin rendering
        /// </summary>
        protected override void OnRenderBegin()
        {
            this.MoveVisualStateToDrawing();
            if (this.visualstateDrawer != null && this.visualStateToDraw != null)
            {
                this.visualstateDrawer.RenderBegin(Context2D.RenderTarget, this.visualStateToDraw, this.PlaybackSettings);
            }
        }
        
        /// <summary>
        /// Render control
        /// </summary>
        protected override void OnRender()
        {
            if (this.visualstateDrawer != null && this.visualStateToDraw != null)
            {
                this.visualstateDrawer.Render(Context2D.RenderTarget, this.visualStateToDraw, this.PlaybackSettings);
            }
        }
        
        /// <summary>
        /// End control rendering
        /// </summary>
        protected override void OnRenderEnd()
        {
            if (this.visualstateDrawer != null && this.visualStateToDraw != null)
            {
                this.visualstateDrawer.RenderEnd(Context2D.RenderTarget, this.visualStateToDraw, this.PlaybackSettings);
            }
        }
    }
}
