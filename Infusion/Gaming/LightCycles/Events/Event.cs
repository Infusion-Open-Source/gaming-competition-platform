
namespace Infusion.Gaming.LightCycles.Events
{
    using System;

    /// <summary>
    ///     The base class for event.
    /// </summary>
    public abstract class Event
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Event" /> class.
        /// </summary>
        protected Event()
        {
            this.TimeStamp = DateTime.Now.Ticks;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the time stamp.
        /// </summary>
        public long TimeStamp { get; protected set; }

        #endregion
    }
}