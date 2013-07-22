using Infusion.Gaming.LightCyclesCommon.Networking;

namespace Infusion.Gaming.LightCycles.UIClient
{
    using System;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.UIClient.Data;
    using Infusion.Gaming.LightCyclesCommon;
    using Infusion.Gaming.LightCyclesCommon.Messaging;

    /// <summary>
    /// Listens to game broadcasts and updates game view accordingly
    /// </summary>
    public class BroadcastListener : BroadcastListenerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastListener" /> class.
        /// </summary>
        /// <param name="view">game view</param>
        public BroadcastListener(GameView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.View = view;
        }

        /// <summary>
        /// Gets or sets game view
        /// </summary>
        public GameView View { get; protected set; }

        /// <summary>
        /// Process broadcast message and create command message
        /// </summary>
        /// <param name="messageIn">incoming broadcast message</param>
        protected override void Process(GameStateMessage messageIn)
        {
            var visualStateBuilder = new VisualStateBuilder(this.BroadcastListener.Tag, new RectangleF(0, 0, this.View.WindowWidth, this.View.WindowHeight));
            var state = visualStateBuilder.BuildVisualState(messageIn);
            if (state != null)
            {
                this.View.UpdateVisualState(state);
            }
        }
    }
}
