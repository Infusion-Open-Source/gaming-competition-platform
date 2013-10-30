namespace Infusion.Gaming.LightCycles.UIClient
{
    /// <summary>
    /// Playback settings class
    /// </summary>
    public class PlaybackSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybackSettings"/> class.
        /// </summary>
        public PlaybackSettings()
        {
            this.ShowNames = false;
            this.WaitOnStart = false;
            this.DelayOnStart = 5000;
            this.WaitOnEnd = false;
            this.DelayOnEnd = 5000;
            this.WaitOnTurn = false;
            this.TurnDelayTime = 100;
            this.FollowMode = false;
            this.FollowedPlayerIndex = 0;

            this.Zoom = 1.0f;
            this.PanX = 0;
            this.PanY = 0;

            this.SpaceHitsCount = 0;
            this.BackHitsCount = 0;
        }

        /// <summary>
        /// Gets or sets a value indicating whether show names
        /// </summary>
        public bool ShowNames { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should wait on start
        /// </summary>
        public bool WaitOnStart { get; set; }

        /// <summary>
        /// Gets or sets delay on start
        /// </summary>
        public int DelayOnStart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should wait on game end
        /// </summary>
        public bool WaitOnEnd { get; set; }

        /// <summary>
        /// Gets or sets delay on end
        /// </summary>
        public int DelayOnEnd { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should wait on end of turn
        /// </summary>
        public bool WaitOnTurn { get; set; }

        /// <summary>
        /// Gets or sets turn delay time
        /// </summary>
        public int TurnDelayTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is in follow mode
        /// </summary>
        public bool FollowMode { get; set; }

        /// <summary>
        /// Gets or sets followed player index
        /// </summary>
        public int FollowedPlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets current zoom level
        /// </summary>
        public float Zoom { get; set; }

        /// <summary>
        /// Gets or sets current X panning
        /// </summary>
        public float PanX { get; set; }

        /// <summary>
        /// Gets or sets current Y panning
        /// </summary>
        public float PanY { get; set; }

        /// <summary>
        /// Gets or sets number of space hits
        /// </summary>
        public int SpaceHitsCount { get; set; }

        /// <summary>
        /// Gets or sets number of back hits
        /// </summary>
        public int BackHitsCount { get; set; }

        /// <summary>
        /// Clone settings
        /// </summary>
        /// <returns>settings clone</returns>
        public PlaybackSettings Clone()
        {
            PlaybackSettings clone = new PlaybackSettings();
            clone.ShowNames = this.ShowNames;
            clone.WaitOnStart = this.WaitOnStart;
            clone.DelayOnStart = this.DelayOnStart;
            clone.WaitOnEnd = this.WaitOnEnd;
            clone.DelayOnEnd = this.DelayOnEnd;
            clone.WaitOnTurn = this.WaitOnTurn;
            clone.TurnDelayTime = this.TurnDelayTime;
            clone.FollowMode = this.FollowMode;
            clone.FollowedPlayerIndex = this.FollowedPlayerIndex;
            clone.Zoom = this.Zoom;
            clone.PanX = this.PanX;
            clone.PanY = this.PanY;
            clone.SpaceHitsCount = this.SpaceHitsCount;
            clone.BackHitsCount = this.BackHitsCount;
            return clone;
        }

        /// <summary>
        /// Turn on follow mode
        /// </summary>
        public void TurnOnFollowing()
        {
            this.Zoom = -15.0f;
            this.PanX = 0;
            this.PanY = 0;
            this.FollowMode = true;
            this.FollowedPlayerIndex = 0;
            this.ShowNames = true;
        }

        /// <summary>
        /// Turn off follow mode
        /// </summary>
        public void TurnOffFollowing()
        {
            this.Zoom = 1.0f;
            this.PanX = 0;
            this.PanY = 0;
            this.FollowMode = false;
            this.FollowedPlayerIndex = 0;
        }
    }
}
