namespace Infusion.Gaming.LightCycles.Messaging
{
    /// <summary>
    /// Dummy message sink, does nothing
    /// </summary>
    public class DummySink : IMessageSink
    {
        /// <summary>
        /// Flush message into a sink
        /// </summary>
        /// <param name="message">message to be flushed</param>
        public void Flush(string message)
        {
        }
    }
}