namespace Infusion.Gaming.LightCycles.Model
{
    using System;

    /// <summary>
    /// The game exception.
    /// </summary>
    [Serializable]
    public class GameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameException" /> class.
        /// </summary>
        public GameException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        public GameException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        /// <param name="internalExcpetion">
        /// The internal exception.
        /// </param>
        public GameException(string message, Exception internalExcpetion)
            : base(message, internalExcpetion)
        {
        }
    }
}