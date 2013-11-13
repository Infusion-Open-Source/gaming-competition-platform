namespace Infusion.Gaming.LightCycles.Exceptions
{
    using System;

    /// <summary>
    /// The game configuration exception.
    /// </summary>
    [Serializable]
    public class GameConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConfigurationException" /> class.
        /// </summary>
        public GameConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameConfigurationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        public GameConfigurationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameConfigurationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        /// <param name="internalExcpetion">
        /// The internal exception.
        /// </param>
        public GameConfigurationException(string message, Exception internalExcpetion)
            : base(message, internalExcpetion)
        {
        }
    }
}