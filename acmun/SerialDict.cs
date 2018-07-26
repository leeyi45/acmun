using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace leeyi45.acmun
{
    [Serializable]
    public class SerialDict<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SerialDict()
        {
            KeySerializer = new XmlSerializer(typeof(TKey));
            ValueSerializer = new XmlSerializer(typeof(TValue));
        }

        private string ItemTagName = "Item";
        private string KeyTag = "Key";
        private string ValueTag = "Value";

        private XmlSerializer KeySerializer;
        private XmlSerializer ValueSerializer;

        /// <summary>
        /// Gets the XML schema for the XML serialization.
        /// </summary>
        /// <returns>An XML schema for the serialized object.</returns>
        public XmlSchema GetSchema() => null;

        /// <summary>
        /// Deserializes the object from XML.
        /// </summary>
        /// <param name="reader">The XML representation of the object.</param>
        public void ReadXml(XmlReader reader)
        {

            var wasEmpty = reader.IsEmptyElement;

            reader.Read();
            if (wasEmpty) return;

            try
            {
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    ReadItem(reader);
                    reader.MoveToContent();
                }
            }
            finally { reader.ReadEndElement(); }
        }

        /// <summary>
        /// Serializes this instance to XML.
        /// </summary>
        /// <param name="writer">The XML writer to serialize to.</param>
        public void WriteXml(XmlWriter writer)
        {
            foreach (var keyValuePair in this)
            {
                WriteItem(writer, keyValuePair);
            }
        }

        /// <summary>
        /// Deserializes the dictionary item.
        /// </summary>
        /// <param name="reader">The XML representation of the object.</param>
        private void ReadItem(XmlReader reader)
        {
            reader.ReadStartElement(ItemTagName);
            try { Add(ReadKey(reader), ReadValue(reader)); }

            finally { reader.ReadEndElement(); }
        }

        /// <summary>
        /// Deserializes the dictionary item's key.
        /// </summary>
        /// <param name="reader">The XML representation of the object.</param>
        /// <returns>The dictionary item's key.</returns>
        private TKey ReadKey(XmlReader reader)
        {
            reader.ReadStartElement(this.KeyTag);
            try
            {
                return (TKey)KeySerializer.Deserialize(reader);
            }
            finally { reader.ReadEndElement(); }
        }

        /// <summary>
        /// Deserializes the dictionary item's value.
        /// </summary>
        /// <param name="reader">The XML representation of the object.</param>
        /// <returns>The dictionary item's value.</returns>
        private TValue ReadValue(XmlReader reader)
        {
            reader.ReadStartElement(ValueTag);
            try
            {
                return (TValue)ValueSerializer.Deserialize(reader);
            }
            finally
            {
                reader.ReadEndElement();
            }
        }

        /// <summary>
        /// Serializes the dictionary item.
        /// </summary>
        /// <param name="writer">The XML writer to serialize to.</param>
        /// <param name="keyValuePair">The key/value pair.</param>
        private void WriteItem(XmlWriter writer, KeyValuePair<TKey, TValue> keyValuePair)
        {
            

            writer.WriteStartElement(ItemTagName);
            try
            {
                WriteKey(writer, keyValuePair.Key);
                WriteValue(writer, keyValuePair.Value);
            }
            finally
            {
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Serializes the dictionary item's key.
        /// </summary>
        /// <param name="writer">The XML writer to serialize to.</param>
        /// <param name="key">The dictionary item's key.</param>
        private void WriteKey(XmlWriter writer, TKey key)
        {
            writer.WriteStartElement(KeyTag);
            try
            {
                KeySerializer.Serialize(writer, key);
            }
            finally
            {
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Serializes the dictionary item's value.
        /// </summary>
        /// <param name="writer">The XML writer to serialize to.</param>
        /// <param name="value">The dictionary item's value.</param>
        private void WriteValue(XmlWriter writer, TValue value)
        {
            writer.WriteStartElement(ValueTag);
            try
            {
                ValueSerializer.Serialize(writer, value);
            }
            finally
            {
                writer.WriteEndElement();
            }
        }
    }

    public class XmlKeyAttribute : Attribute
    {
        public XmlKeyAttribute(string keyname) => KeyName = keyname;

        public string KeyName { get; set; }
    }

    public class XmlValueAttribute : Attribute
    {
        public XmlValueAttribute(string valuename) => ValueName = valuename;

        public string ValueName { get; set; }
    }
}
