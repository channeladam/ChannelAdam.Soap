//-----------------------------------------------------------------------
// <copyright file="ObjectXmlSerialiser.cs">
//     Copyright (c) 2016 Adam Craven. All rights reserved.
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
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;

    public static class ObjectXmlSerialiser
    {
        #region Public Methods

        public static string SerialiseObject(object toSerialise, string toElementName, string toElementNamespace)
        {
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };

            return SerialiseObject(toSerialise, toElementName, toElementNamespace, settings);
        }

        public static string SerialiseObject(object toSerialise, string toElementName, string toElementNamespace, XmlWriterSettings xmlWriterSettings)
        {
            string result = null;
            StringWriter stringWriter = null;

            var objectType = toSerialise.GetType();
            var newRootAttribute = CreateXmlRootAttribute(objectType, toElementName, toElementNamespace);
            XmlAttributeOverrides xmlAttributeOverrides = CreateXmlAttributeOverrides(objectType, newRootAttribute);

            var serialiser = new XmlSerializer(objectType, xmlAttributeOverrides);

            try
            {
                stringWriter = new StringWriter();

                using (var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
                {
                    serialiser.Serialize(xmlWriter, toSerialise);
                }

                result = stringWriter.ToString();
            }
            finally
            {
                if (stringWriter != null)
                {
                    stringWriter.Dispose();
                }
            }

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static XmlRootAttribute CreateXmlRootAttribute(Type objectType, string elementName, string xmlNamespace = null)
        {
            var newRootAttribute = new XmlRootAttribute(elementName);

            if (string.IsNullOrWhiteSpace(xmlNamespace))
            {
                xmlNamespace = GetNamespaceFromExistingXmlAttribute(objectType);
            }

            newRootAttribute.Namespace = xmlNamespace;

            return newRootAttribute;
        }

        private static string GetNamespaceFromExistingXmlAttribute(Type objectType)
        {
            string result = null;

            var customAtts = objectType.GetCustomAttributes(false);

            var rootAtts = customAtts.OfType<XmlRootAttribute>();
            if (rootAtts.Count() > 0)
            {
                var att = (XmlRootAttribute)rootAtts.First();
                result = att.Namespace;
            }
            else
            {
                var xmlTypeAtts = customAtts.OfType<XmlTypeAttribute>();
                if (xmlTypeAtts.Count() > 0)
                {
                    var att = (XmlTypeAttribute)xmlTypeAtts.First();
                    result = att.Namespace;
                }
            }

            return result;
        }

        private static XmlAttributeOverrides CreateXmlAttributeOverrides(Type objectType, XmlRootAttribute newRootAttribute)
        {
            var xmlAttributeOverrides = new XmlAttributeOverrides();
            var xmlAttributes = new XmlAttributes();
            xmlAttributes.XmlRoot = newRootAttribute;
            xmlAttributeOverrides.Add(objectType, xmlAttributes);

            return xmlAttributeOverrides;
        }

        #endregion Private Methods
    }
}
