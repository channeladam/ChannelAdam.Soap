//-----------------------------------------------------------------------
// <copyright file="ObjectXmlSerialiser.cs">
//     Copyright (c) 2016-2021 Adam Craven. All rights reserved.
// </copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

namespace ChannelAdam.Soap
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public static class ObjectXmlSerialiser
    {
        #region Private Fields

        private static readonly ConcurrentDictionary<Tuple<Type, string, string>, XmlSerializer> SerialiserCache = new();

        #endregion Private Fields

        #region Public Methods

        public static string SerialiseObject(object toSerialise, string toElementName, string toElementNamespace)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true
            };

            return SerialiseObject(toSerialise, toElementName, toElementNamespace, settings);
        }

        public static string SerialiseObject(object toSerialise, string toElementName, string toElementNamespace, XmlWriterSettings xmlWriterSettings)
        {
            if (toSerialise == null)
            {
                throw new ArgumentNullException(nameof(toSerialise));
            }

            var objectType = toSerialise.GetType();

            XmlSerializer serialiser = GetOrAddXmlSerialiserFromCache(objectType, toElementName, toElementNamespace);

            var sb = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(sb, xmlWriterSettings))
            {
                serialiser.Serialize(xmlWriter, toSerialise);
            }

            return sb.ToString();
        }

        #endregion Public Methods

        #region Private Methods

        private static XmlSerializer GetOrAddXmlSerialiserFromCache(Type objectType, string toElementName, string toElementNamespace)
        {
            /*
             * The XmlSerializer(Type, XmlAttributeOverrides) ctor leaks memory, so cache it per unique input values: 
             *      https://msdn.microsoft.com/en-us/library/system.xml.serialization.xmlserializer.aspx
             *     "To increase performance, the XML serialization infrastructure dynamically generates assemblies to serialize and deserialize specified types. 
             *      The infrastructure finds and reuses those assemblies. This behavior occurs only when using the following constructors:
             *          XmlSerializer.XmlSerializer(Type)
             *          XmlSerializer.XmlSerializer(Type, String)
             *      If you use any of the other constructors, multiple versions of the same assembly are generated and never unloaded, 
             *      which results in a memory leak and poor performance. The easiest solution is to use one of the previously mentioned two constructors. 
             *      Otherwise, you must cache the assemblies..."
            */
            return SerialiserCache.GetOrAdd(
                Tuple.Create(objectType, toElementName, toElementNamespace),
                _ =>
                {
                    var newRootAttribute = CreateXmlRootAttribute(objectType, toElementName, toElementNamespace);
                    XmlAttributeOverrides xmlAttributeOverrides = CreateXmlAttributeOverrides(objectType, newRootAttribute);
                    return new XmlSerializer(objectType, xmlAttributeOverrides);
                });
        }

        private static XmlAttributeOverrides CreateXmlAttributeOverrides(Type objectType, XmlRootAttribute newRootAttribute)
        {
            var xmlAttributeOverrides = new XmlAttributeOverrides();
            var xmlAttributes = new XmlAttributes
            {
                XmlRoot = newRootAttribute
            };
            xmlAttributeOverrides.Add(objectType, xmlAttributes);

            return xmlAttributeOverrides;
        }

        private static XmlRootAttribute CreateXmlRootAttribute(Type objectType, string elementName, string? xmlNamespace = null)
        {
            var newRootAttribute = new XmlRootAttribute(elementName);

            if (string.IsNullOrWhiteSpace(xmlNamespace))
            {
                xmlNamespace = GetNamespaceFromExistingXmlAttribute(objectType);
            }

            newRootAttribute.Namespace = xmlNamespace;

            return newRootAttribute;
        }

        private static string? GetNamespaceFromExistingXmlAttribute(Type objectType)
        {
            string? result = null;

            var customAtts = objectType.GetCustomAttributes(false);

            var rootAtts = customAtts.OfType<XmlRootAttribute>();
            if (rootAtts.Any())
            {
                var att = (XmlRootAttribute)rootAtts.First();
                result = att.Namespace;
            }
            else
            {
                var xmlTypeAtts = customAtts.OfType<XmlTypeAttribute>();
                if (xmlTypeAtts.Any())
                {
                    var att = (XmlTypeAttribute)xmlTypeAtts.First();
                    result = att.Namespace;
                }
            }

            return result;
        }

        #endregion Private Methods
    }
}