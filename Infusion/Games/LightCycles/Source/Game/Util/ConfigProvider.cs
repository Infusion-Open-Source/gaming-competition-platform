namespace Infusion.Gaming.LightCycles.Util
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Generic XML config reader and writer
    /// </summary>
    /// <typeparam name="T">Type to read or write</typeparam>
    public class ConfigProvider<T>
        where T : class
    {
        /// <summary>
        /// Save object as XML into file
        /// </summary>
        /// <param name="obj">object to be saved</param>
        /// <param name="fileName">file to which data should be saved</param>
        public void Save(T obj, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
                writer.Close();
            }
        }

        /// <summary>
        /// Loads object from XML file
        /// </summary>
        /// <param name="fileName">file with object data</param>
        /// <returns>deserialized object</returns>
        public T Load(string fileName)
        {
            T result = null;
            using (StreamReader reader = new StreamReader(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(reader);
                reader.Close();
            }

            return result;
        }
    }
}
