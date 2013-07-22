namespace Infusion.Gaming.LightCycles.Logs
{
    /// <summary>
    /// Game log interface
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Log message 
        /// </summary>
        /// <param name="message">message to be logged</param>
        void Write(string message);
    }
}
