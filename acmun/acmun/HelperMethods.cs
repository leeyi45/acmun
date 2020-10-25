using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace acmun
{
    static class HelperMethods
    {
        /// <summary>
        /// Wrapper method to deserialize an object from an XML file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="path">File path</param>
        /// <returns>Deserialized object</returns>
        public static T DeserializeXMLFromFile<T>(string path)
        {
            using var fileStream = File.OpenRead(path);
            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(fileStream);
        }

        /// <summary>
        /// Wrapper method to serialize an object to an XML file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="path">File path</param>
        /// <param name="obj">Object to serialize</param>
        public static void SerializeToXMLFile<T>(string path, T obj)
        {
            using var fileStream = File.OpenWrite(path);
            var serializer = new XmlSerializer(typeof(T));

            serializer.Serialize(fileStream, obj);
        }
    }
}
