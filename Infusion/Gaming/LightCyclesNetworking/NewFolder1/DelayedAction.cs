using System;

namespace Infusion.Gaming.LightCyclesNetworking.NewFolder1
{
    /// <summary>
    /// Delays given action to until specified start time
    /// </summary>
    public class DelayedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelayedAction" /> class.
        /// </summary>
        /// <param name="actionStartTime">start time of delayed action</param>
        public DelayedAction(DateTime actionStartTime)
        {
            if(actionStartTime <= DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("actionStartTime");
            }

            this.ActionStartTime = actionStartTime;
        }

        /// <summary>
        /// Trys to run delayed action
        /// </summary>
        /// <param name="action">action to be invoked</param>
        /// <returns>whether action were invoked</returns>
        public bool Run(Action action)
        {
            if (this.ActionStartTime <= DateTime.Now)
            {
                this.ActionStartTime = DateTime.MinValue;
                action.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets or sets delayed action start time
        /// </summary>
        public DateTime ActionStartTime { get; protected set; }

        /// <summary>
        /// Gets number of seconds till action start
        /// </summary>
        public int SecondsToStart
        {
            get
            {
                return (int)this.ActionStartTime.Subtract(DateTime.Now).TotalSeconds;
            }
        }
    }
}
