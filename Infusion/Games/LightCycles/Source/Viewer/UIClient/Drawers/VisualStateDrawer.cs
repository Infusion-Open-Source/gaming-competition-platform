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
        /// <param name="settings">playback settings</param>
        public void RenderBegin(RenderTarget renderTarget, VisualState visualState, PlaybackSettings settings)
        {
            // recalculate game area
            this.GameDimensions = new RectangleF(0, 0, visualState.GridLayer.Width * visualState.GridSize, visualState.GridLayer.Height * visualState.GridSize);
            this.FocusPoint = this.GameCenter;

            PointF focus = this.FocusPoint;
            if (settings.FollowMode)
            {
                PointF bike = this.GetBikeLocation(visualState, settings.FollowedPlayerIndex);
                if (bike != PointF.Empty)
                {
                    focus = new PointF(bike.X * visualState.GridSize, bike.Y * visualState.GridSize);
                }
                else
                {
                    settings.TurnOffFollowing();
                }
            }

            renderTarget.BeginDraw();            
            float scaleFactor = -0.1f * settings.Zoom;
            PointF focusPoint = new PointF(-(focus.X + (settings.PanX * visualState.GridSize)), -(focus.Y + (settings.PanY * visualState.GridSize)));
            PointF scalePoint = new PointF(1.0f + scaleFactor, 1.0f + scaleFactor);
            PointF windowTranslationPoint = new PointF(this.WindowCenter.X, this.WindowCenter.Y);
            renderTarget.Transform = Matrix3x2.Translation(focusPoint.X, focusPoint.Y) * Matrix3x2.Scale(scalePoint.X, scalePoint.Y) * Matrix3x2.Translation(windowTranslationPoint.X, windowTranslationPoint.Y);
            foreach (IDrawer drawer in this.InternalDrawers)
            {
                drawer.RenderBegin(renderTarget, visualState, settings);
            }
        }

        /// <summary>
        /// Do the rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        /// <param name="settings">playback settings</param>
        public void Render(RenderTarget renderTarget, VisualState visualState, PlaybackSettings settings)
        {
            foreach (IDrawer drawer in this.InternalDrawers)
            {
                drawer.Render(renderTarget, visualState, settings);
            }
        }

        /// <summary>
        /// End rendering
        /// </summary>
        /// <param name="renderTarget">rendering target</param>
        /// <param name="visualState">visual state of the game</param>
        /// <param name="settings">playback settings</param>
        public void RenderEnd(RenderTarget renderTarget, VisualState visualState, PlaybackSettings settings)
        {
            foreach (IDrawer drawer in this.InternalDrawers)
            {
                drawer.RenderEnd(renderTarget, visualState, settings);
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

        /// <summary>
        /// Gets a bike
        /// </summary>
        /// <param name="visualState">visual state of game</param>
        /// <param name="index">bike index</param>
        /// <returns>bike location</returns>
        private PointF GetBikeLocation(VisualState visualState, int index)
        {
            List<GameIdentity> ids = new List<GameIdentity>(visualState.PlayersLocations.Keys);
            if (ids.Count > 0)
            {
                index = index % ids.Count;
                return visualState.PlayersLocations[ids[index]];
            }

            return PointF.Empty;
        }
    }
}
