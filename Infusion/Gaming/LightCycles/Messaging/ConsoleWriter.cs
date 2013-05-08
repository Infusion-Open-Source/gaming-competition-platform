namespace Infusion.Gaming.LightCycles.Messaging
{
    using System;

    /// <summary>
    /// Game state console writer - outputs state of the game into console window
    /// </summary>
    public class GameStateConsoleWriter : IMessageSink
    {
        /// <summary>
        /// Flush message into a sink
        /// </summary>
        /// <param name="message">message to be flushed</param>
        public void Flush(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.Out.WriteLine(message);
            }
        }
    }
}