namespace Infusion.Gaming.LightCycles.Messaging
{
    using System.Collections.Generic;

    /// <summary>
    /// Game state sink interface
    /// </summary>
    public class MessageSinkSet : List<IMessageSink>, IMessageSink
    {
        /// <summary>
        /// Flush message into a sink
        /// </summary>
        /// <param name="message">message to be flushed</param>
        public void Flush(string message)
        {
            foreach (var sink in this)
            {
                sink.Flush(message);
            }
        }
    }
}
