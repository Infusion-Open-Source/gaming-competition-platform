namespace Infusion.Gaming.LightCycles.Events
{
    using System;
    using System.Text;

    /// <summary>
    /// The tick event.
    /// </summary>
    public class TickEvent : Event
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TickEvent"/> class.
        /// </summary>
        /// <param name="turn">
        /// Number of game turn.
        /// </param>
        public TickEvent(int turn)
        {
            if (turn < 0)
            {
                throw new ArgumentOutOfRangeException("turn");
            }

            this.Turn = turn;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets game turn.
        /// </summary>
        public int Turn { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>
        /// String representation of an object.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Turn {0} starts", this.Turn);
            return builder.ToString();
        }

        #endregion
    }
}