namespace Infusion.Gaming.LightCycles.Logs
{
    using System;

    /// <summary>
    /// Game logs console writer
    /// </summary>
    public class ConsoleOutput : ILog
    {
        /// <summary>
        /// Log message 
        /// </summary>
        /// <param name="message">message to be logged</param>
        public void Write(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.Out.WriteLine(message);
            }
        }
    }
}