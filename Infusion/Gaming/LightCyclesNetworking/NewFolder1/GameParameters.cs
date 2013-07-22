namespace Infusion.Gaming.LightCyclesNetworking.NewFolder1
{
    public class GameParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameParameters" /> class.
        /// </summary>
        public GameParameters()
        {
            this.TurnTime = 100;
            this.DelayOnStart = 3;
            this.DelayOnEnd = 3;
        }
        
        /// <summary>
        /// Gets or sets game turn delay time (in milliseconds)
        /// </summary>
        public int TurnTime { get; set; }

        /// <summary>
        /// Gets or sets game delay on start (in seconds)
        /// </summary>
        public int DelayOnStart { get; set; }

        /// <summary>
        /// Gets or sets game delay on end (in seconds)
        /// </summary>
        public int DelayOnEnd { get; set; }
    }
}
