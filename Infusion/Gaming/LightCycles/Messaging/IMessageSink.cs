namespace Infusion.Gaming.LightCycles.Messaging
{
    /// <summary>
    /// Game message sink interface
    /// </summary>
    public interface IMessageSink
    {
        /// <summary>
        /// Flush message into a sink
        /// </summary>
        /// <param name="message">message to be flushed</param>
        void Flush(string message);
    }
}
