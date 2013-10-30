namespace Infusion.Gaming.LightCycles.Logs
{
    using System.Collections.Generic;

    /// <summary>
    /// Game logger set
    /// </summary>
    public class LoggerSet : List<ILog>, ILog
    {
        /// <summary>
        /// Log message 
        /// </summary>
        /// <param name="message">message to be logged</param>
        public void Write(string message)
        {
            foreach (var logger in this)
            {
                logger.Write(message);
            }
        }
    }
}
