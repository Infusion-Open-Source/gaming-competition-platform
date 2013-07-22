using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Infusion.Networking
{
    /// <summary>
    /// Provides message serialization / deserialization generic routines
    /// </summary>
    public class MessageXmlSerializer : IMessageSerializer
    {
        /// <summary>
        /// Serialize message to string
        /// </summary>
        /// <param name="message">message to be serialized</param>
        /// <param name="messageType">message type</param>
        /// <returns>message as string</returns>
        public string Serialize(IMessage message, Type messageType)
        {
            StringBuilder messageBuilder = new StringBuilder();
            using (TextWriter writer = new StringWriter(messageBuilder))
            {
                XmlSerializer serializer = new XmlSerializer(messageType);
                serializer.Serialize(writer, message);
            }

            return messageBuilder.ToString();
        }

        /// <summary>
        /// Deserialize message from string
        /// </summary>
        /// <param name="data">data with serialized message</param>
        /// <param name="messageType">message type</param>
        /// <returns>deserialized message</returns>
        public IMessage Deserialize(string data, Type messageType)
        {
            IMessage result;
            using (TextReader reader = new StringReader(data))
            {
                XmlSerializer serializer = new XmlSerializer(messageType);
                result = (IMessage)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
