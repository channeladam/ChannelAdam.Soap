//-----------------------------------------------------------------------
// <copyright file="Soap12Maker.cs">
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Helps make SOAP 1.2 messages.
    /// </summary>
    /// <remarks>
    /// See https://www.w3.org/TR/soap12-part1/.
    /// </remarks>
    public static class Soap12Maker
    {
        #region Public Methods

        #region CreateSoapEnvelopeWithFault

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, string reason)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, reason)));
        }

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, string? subCode, string reason)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, subCode, reason)));
        }

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason)));
        }

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage)));
        }

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, null, null)));
        }

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node, string? role)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, role, null)));
        }

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, IEnumerable<XContainer>? detailEntries)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, null, null, detailEntries)));
        }

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, IEnumerable<XContainer>? detailEntries)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, null, null, detailEntries)));
        }

        public static XElement CreateSoapEnvelopeWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node, string? role, IEnumerable<XContainer>? detailEntries)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, role, detailEntries)));
        }

        #endregion

        #region CreateSoapBodyWithFault

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, string reason)
        {
            return CreateSoapBody(CreateSoapFault(code, reason));
        }

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, string? subCode, string reason)
        {
            return CreateSoapBody(CreateSoapFault(code, subCode, reason));
        }

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason)
        {
            return CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason));
        }

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage)
        {
            return CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage));
        }

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node)
        {
            return CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, null, null));
        }

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node, string? role)
        {
            return CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, role, null));
        }

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, IEnumerable<XContainer>? detailEntries)
        {
            return CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, null, null, detailEntries));
        }

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, IEnumerable<XContainer>? detailEntries)
        {
            return CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, null, null, detailEntries));
        }

        public static XElement CreateSoapBodyWithFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node, string? role, IEnumerable<XContainer>? detailEntries)
        {
            return CreateSoapBody(CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, role, detailEntries));
        }

        #endregion CreateSoapBodyWithFault

        #region CreateSoapFault

        public static XElement CreateSoapFault(Soap12FaultCode code, string reason)
        {
            return CreateSoapFault(code, null, null, reason);
        }

        public static XElement CreateSoapFault(Soap12FaultCode code, string? subCode, string reason)
        {
            return CreateSoapFault(code, (XNamespace?)null, subCode, reason, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, null, null, null);
        }

        public static XElement CreateSoapFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason)
        {
            return CreateSoapFault(code, subCodeNamespace, subCode, reason, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, null, null, null);
        }

        public static XElement CreateSoapFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage)
        {
            return CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, null, null, null);
        }

        public static XElement CreateSoapFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node)
        {
            return CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, null, null);
        }

        public static XElement CreateSoapFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node, string? role)
        {
            return CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, role, null);
        }

        public static XElement CreateSoapFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, IEnumerable<XContainer>? detailEntries)
        {
            return CreateSoapFault(code, subCodeNamespace, subCode, reason, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, null, null, detailEntries);
        }

        public static XElement CreateSoapFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, IEnumerable<XContainer>? detailEntries)
        {
            return CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, null, null, detailEntries);
        }

        public static XElement CreateSoapFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node, string? role, IEnumerable<XContainer>? detailEntries)
        {
            var fault = new XElement(Soap12Constants.SoapEnvelopeNamespace + "Fault");

            var codeElements = new List<XElement>
            {
                new XElement(Soap12Constants.SoapEnvelopeNamespace + "Value", $"{NamespacePrefixConstants.SoapEnvelope}:{code}")
            };

            if (!string.IsNullOrWhiteSpace(subCode))
            {
                XAttribute? subCodeNamespaceAttribute = null;
                string? subCodeValue = subCode;

                if (subCodeNamespace != null)
                {
                    subCodeNamespaceAttribute = new XAttribute(XNamespace.Xmlns + "sc", subCodeNamespace.NamespaceName);
                    subCodeValue = $"sc:{subCode}";
                }

                if (subCodeNamespaceAttribute is null)
                {
                    codeElements.Add(
                        new XElement(
                            Soap12Constants.SoapEnvelopeNamespace + "Subcode",
                            new XElement(Soap12Constants.SoapEnvelopeNamespace + "Value", subCodeValue)));
                }
                else
                {
                    codeElements.Add(
                        new XElement(
                            Soap12Constants.SoapEnvelopeNamespace + "Subcode",
                            new XElement(Soap12Constants.SoapEnvelopeNamespace + "Value", subCodeNamespaceAttribute, subCodeValue)));
                }
            }

            fault.Add(new XElement(Soap12Constants.SoapEnvelopeNamespace + "Code", codeElements));

            fault.Add(
                new XElement(
                    Soap12Constants.SoapEnvelopeNamespace + "Reason",
                    new XElement(
                        Soap12Constants.SoapEnvelopeNamespace + "Text",
                        new XAttribute(NamespaceConstants.Xml + "lang", reasonXmlLanguage),
                        reason)));

            if (!string.IsNullOrWhiteSpace(node))
            {
                fault.Add(new XElement(Soap12Constants.SoapEnvelopeNamespace + "Node", node));
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                fault.Add(new XElement(Soap12Constants.SoapEnvelopeNamespace + "Role", role));
            }

            if (detailEntries?.Any() == true)
            {
                var detail = new XElement(Soap12Constants.SoapEnvelopeNamespace + "Detail");
                fault.Add(detail);

                foreach (var detailEntry in detailEntries)
                {
                    detail.Add(detailEntry);
                }
            }

            return fault;
        }

        #endregion CreateSoapFault

        #region CreateSoapBody

        public static XElement CreateSoapBody()
        {
            return CreateSoapBody((XContainer?)null);
        }

        public static XElement CreateSoapBody(string fromXml)
        {
            return CreateSoapBody(XElement.Parse(fromXml));
        }

        public static XElement CreateSoapBody(object toSerialise)
        {
            return CreateSoapBody(ObjectXmlSerialiser.SerialiseObject(toSerialise, string.Empty, string.Empty));
        }

        public static XElement CreateSoapBody(object toSerialise, string toElementName, string toElementNamespace)
        {
            return CreateSoapBody(ObjectXmlSerialiser.SerialiseObject(toSerialise, toElementName, toElementNamespace));
        }

        public static XElement CreateSoapBody(object toSerialise, string toElementName, string toElementNamespace, XmlWriterSettings xmlWriterSettings)
        {
            return CreateSoapBody(ObjectXmlSerialiser.SerialiseObject(toSerialise, toElementName, toElementNamespace, xmlWriterSettings));
        }

        public static XElement CreateSoapBody(XContainer? fromNode)
        {
            return new XElement(Soap12Constants.SoapEnvelopeNamespace + "Body", fromNode);
        }

        #endregion CreateSoapBody

        #region CreateSoapHeader

        public static XElement CreateSoapHeader()
        {
            return new XElement(Soap12Constants.SoapEnvelopeNamespace + "Header");
        }

        public static XElement CreateSoapHeaderActionBlock(string action)
        {
            var element = new XElement(NamespaceConstants.WebServicesAddressing + "Action", action);
            SetWebServicesAddressingNamespaceAttribute(element);
            return element;
        }

        #endregion CreatSoapHeader

        #region CreateSoapEnvelope

        public static XElement CreateSoapEnvelope()
        {
            var envelope = new XElement(Soap12Constants.SoapEnvelopeNamespace + "Envelope", CreateSoapEnvelopeAttribute());
            SetXmlNamespaceAttribute(envelope);
            return envelope;
        }

        public static XElement CreateSoapEnvelope(string fromSoapBodyXml)
        {
            return CreateSoapEnvelope(XElement.Parse(fromSoapBodyXml));
        }

        public static XElement CreateSoapEnvelope(string fromSoapHeaderXml, string fromSoapBodyXml)
        {
            return CreateSoapEnvelope(XElement.Parse(fromSoapHeaderXml), XElement.Parse(fromSoapBodyXml));
        }

        public static XElement CreateSoapEnvelope(XContainer? fromSoapBodyNode)
        {
            return CreateSoapEnvelope(CreateSoapHeader(), fromSoapBodyNode);
        }

        public static XElement CreateSoapEnvelope(XContainer? fromSoapHeaderNode, XContainer? fromSoapBodyNode)
        {
            var envelope = CreateSoapEnvelope();

            if (fromSoapHeaderNode != null)
            {
                envelope.Add(fromSoapHeaderNode);
            }

            if (fromSoapBodyNode != null)
            {
                envelope.Add(fromSoapBodyNode);
            }

            return envelope;
        }

        public static void SetStandardSoapEncodingAttribute(XElement node)
        {
            SetSoapEncodingAttribute(node, Soap12Constants.SoapEncodingStandardNamespace.NamespaceName);
        }

        public static void SetSoapEncodingAttribute(XElement node, string soapEncodingNamespace)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            node.SetAttributeValue(Soap12Constants.SoapEnvelopeNamespace + "soapEncoding", soapEncodingNamespace);
        }

        public static void SetWebServicesAddressingNamespaceAttribute(XElement node)
        {
            SetWebServicesAddressingNamespaceAttribute(node, NamespacePrefixConstants.WebServicesAddressing);
        }

        public static void SetWebServicesAddressingNamespaceAttribute(XElement node, string attributeName)
        {
            node.SetAttributeValue(XNamespace.Xmlns + attributeName, NamespaceConstants.WebServicesAddressing);
        }

        public static void SetXmlNamespaceAttribute(XElement node)
        {
            SetXmlNamespaceAttribute(node, NamespacePrefixConstants.Xml);
        }

        public static void SetXmlNamespaceAttribute(XElement node, string attributeName)
        {
            node.SetAttributeValue(XNamespace.Xmlns + attributeName, NamespaceConstants.Xml);
        }

        public static void SetXmlSchemaNamespaceAttribute(XElement node)
        {
            SetXmlSchemaNamespaceAttribute(node, NamespacePrefixConstants.XmlSchema);
        }

        public static void SetXmlSchemaNamespaceAttribute(XElement node, string attributeName)
        {
            node.SetAttributeValue(XNamespace.Xmlns + attributeName, NamespaceConstants.XmlSchema);
        }

        public static void SetXmlSchemaInstanceNamespaceAttribute(XElement node)
        {
            SetXmlSchemaInstanceNamespaceAttribute(node, NamespacePrefixConstants.XmlSchemaInstance);
        }

        public static void SetXmlSchemaInstanceNamespaceAttribute(XElement node, string attributeName)
        {
            node.SetAttributeValue(XNamespace.Xmlns + attributeName, NamespaceConstants.XmlSchemaInstance);
        }

        #endregion CreateSoapEnvelope

        public static bool HasFault(XContainer? node)
        {
            return node?.Descendants(Soap12Constants.SoapEnvelopeNamespace + "Fault").Any() == true;
        }

        #endregion Public Methods

        #region Private Methods

        private static XAttribute CreateSoapEnvelopeAttribute()
        {
            return new XAttribute(XNamespace.Xmlns + NamespacePrefixConstants.SoapEnvelope, Soap12Constants.SoapEnvelopeNamespace);
        }

        #endregion Private Methods
    }
}
