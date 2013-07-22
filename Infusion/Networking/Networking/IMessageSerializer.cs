using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Infusion.Networking
{
    /// <summary>
    /// Provides message serialization / deserialization generic routines
    /// </summary>
    public interface IMessageSerializer
    {
        /// <summary>
        /// Serialize message to string
        /// </summary>
        /// <param name="message">message to be serialized</param>
        /// <param name="messageType">message type</param>
        /// <returns>message as string</returns>
        string Serialize(IMessage message, Type messageType);

        /// <summary>
        /// Deserialize message from string
        /// </summary>
        /// <param name="data">data with serialized message</param>
        /// <param name="messageType">message type</param>
        /// <returns>deserialized message</returns>
        IMessage Deserialize(string data, Type messageType);
    }
}
