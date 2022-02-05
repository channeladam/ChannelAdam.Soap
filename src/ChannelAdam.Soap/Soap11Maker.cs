//-----------------------------------------------------------------------
// <copyright file="Soap11Maker.cs">
//     Copyright (c) 2016-2022 Adam Craven. All rights reserved.
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
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Helps make SOAP 1.1 messages.
    /// </summary>
    /// <remarks>
    /// See https://www.w3.org/TR/2000/NOTE-SOAP-20000508/.
    /// </remarks>
    public static class Soap11Maker
    {
        #region Public Methods

#pragma warning disable CA1720 // IdentifiersShouldNotContainTypeNames - but, this is as per the SOAP specification...

        #region CreateSoapEnvelopeWithFault

        public static XElement CreateSoapEnvelopeWithFault(Soap11FaultCode code, string faultString) =>
            CreateSoapEnvelopeWithFault(code, faultString, faultActor: null, detailEntries: null, envelopeNamespacePrefix: null);

        public static XElement CreateSoapEnvelopeWithFault(Soap11FaultCode code, string faultString, string? faultActor) =>
            CreateSoapEnvelopeWithFault(code, faultString, faultActor, detailEntries: null, envelopeNamespacePrefix: null);

        public static XElement CreateSoapEnvelopeWithFault(Soap11FaultCode code, string faultString, string? faultActor, string? envelopeNamespacePrefix) =>
            CreateSoapEnvelopeWithFault(code, faultString, faultActor, detailEntries: null, envelopeNamespacePrefix);

        public static XElement CreateSoapEnvelopeWithFault(Soap11FaultCode code, string faultString, string? faultActor, IEnumerable<XContainer>? detailEntries) =>
            CreateSoapEnvelopeWithFault(code, faultString, faultActor, detailEntries, envelopeNamespacePrefix: null);

        public static XElement CreateSoapEnvelopeWithFault(Soap11FaultCode code, string faultString, string? faultActor, IEnumerable<XContainer>? detailEntries, string? envelopeNamespacePrefix) =>
            CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, faultString, faultActor, detailEntries, envelopeNamespacePrefix)));

        #endregion CreateSoapEnvelopeWithFault

        #region CreateSoapBodyWithFault

        public static XElement CreateSoapBodyWithFault(Soap11FaultCode code, string faultString) =>
            CreateSoapBodyWithFault(code, faultString, faultActor: null, detailEntries: null, envelopeNamespacePrefix: null);

        public static XElement CreateSoapBodyWithFault(Soap11FaultCode code, string faultString, string? faultActor) =>
            CreateSoapBodyWithFault(code, faultString, faultActor, detailEntries: null, envelopeNamespacePrefix: null);

        public static XElement CreateSoapBodyWithFault(Soap11FaultCode code, string faultString, string? faultActor, string? envelopeNamespacePrefix) =>
            CreateSoapBodyWithFault(code, faultString, faultActor, detailEntries: null, envelopeNamespacePrefix);

        public static XElement CreateSoapBodyWithFault(Soap11FaultCode code, string faultString, string? faultActor, IEnumerable<XContainer>? detailEntries) =>
            CreateSoapBodyWithFault(code, faultString, faultActor, detailEntries, envelopeNamespacePrefix: null);

        public static XElement CreateSoapBodyWithFault(Soap11FaultCode code, string faultString, string? faultActor, IEnumerable<XContainer>? detailEntries, string? envelopeNamespacePrefix) =>
            CreateSoapBody(CreateSoapFault(code, faultString, faultActor, detailEntries, envelopeNamespacePrefix));

        #endregion CreateSoapBodyWithFault

        #region CreateSoapFault

        public static XElement CreateSoapFault(Soap11FaultCode code, string faultString) =>
            CreateSoapFault(code, faultString, faultActor: null, detailEntries: null, envelopeNamespacePrefix: null);

        public static XElement CreateSoapFault(Soap11FaultCode code, string faultString, string? faultActor) =>
            CreateSoapFault(code, faultString, faultActor, detailEntries: null, envelopeNamespacePrefix: null);

        public static XElement CreateSoapFault(Soap11FaultCode code, string faultString, string? faultActor, string? envelopeNamespacePrefix) =>
            CreateSoapFault(code, faultString, faultActor, detailEntries: null, envelopeNamespacePrefix);

        public static XElement CreateSoapFault(Soap11FaultCode code, string faultString, string? faultActor, IEnumerable<XContainer>? detailEntries, string? envelopeNamespacePrefix)
        {
            var fault = new XElement(Soap11Constants.SoapEnvelopeNamespace + "Fault");

            fault.Add(new XElement(XNamespace.None + "faultcode", $"{envelopeNamespacePrefix ?? NamespacePrefixConstants.SoapEnvelope}:{code}"));
            fault.Add(new XElement(XNamespace.None + "faultstring", faultString));

            if (!string.IsNullOrWhiteSpace(faultActor))
            {
                fault.Add(new XElement(XNamespace.None + "faultactor", faultActor));
            }

            if (detailEntries?.Any() == true)
            {
                var detail = new XElement(XNamespace.None + "detail");
                fault.Add(detail);

                foreach (var detailEntry in detailEntries)
                {
                    detail.Add(detailEntry);
                }
            }

            return fault;
        }

        #endregion CreateSoapFault

#pragma warning restore CA1720 // IdentifiersShouldNotContainTypeNames - but, this is as per the SOAP specification...

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
            return new XElement(Soap11Constants.SoapEnvelopeNamespace + "Body", fromNode);
        }

        #endregion CreateSoapBody

        #region CreateSoapHeader

        public static XElement CreateSoapHeader()
        {
            return new XElement(Soap11Constants.SoapEnvelopeNamespace + "Header");
        }

        public static XElement CreateSoapHeaderActionBlock(string action)
        {
            var element = new XElement(NamespaceConstants.WebServicesAddressing + "Action", action);
            SetWebServicesAddressingNamespaceAttribute(element);
            return element;
        }

        #endregion CreateSoapHeader

        #region CreateSoapEnvelope

        public static XElement CreateSoapEnvelope() => CreateSoapEnvelopeWithNamespacePrefix(null);

        public static XElement CreateSoapEnvelopeWithNamespacePrefix(string? envelopeNamespacePrefix) =>
            new(Soap11Constants.SoapEnvelopeNamespace + "Envelope", CreateSoapEnvelopeAttribute(envelopeNamespacePrefix));

        public static XElement CreateSoapEnvelope(string fromSoapBodyXml) =>
            CreateSoapEnvelope(XElement.Parse(fromSoapBodyXml), envelopeNamespacePrefix: null);

        public static XElement CreateSoapEnvelope(string fromSoapHeaderXml, string fromSoapBodyXml) =>
            CreateSoapEnvelope(fromSoapHeaderXml, fromSoapBodyXml, envelopeNamespacePrefix: null);

        public static XElement CreateSoapEnvelope(string fromSoapHeaderXml, string fromSoapBodyXml, string? envelopeNamespacePrefix) =>
            CreateSoapEnvelope(XElement.Parse(fromSoapHeaderXml), XElement.Parse(fromSoapBodyXml), envelopeNamespacePrefix);

        public static XElement CreateSoapEnvelope(XContainer? fromSoapBodyNode) =>
            CreateSoapEnvelope(fromSoapBodyNode, envelopeNamespacePrefix: null);

        public static XElement CreateSoapEnvelope(XContainer? fromSoapBodyNode, string? envelopeNamespacePrefix) =>
            CreateSoapEnvelope(CreateSoapHeader(), fromSoapBodyNode, envelopeNamespacePrefix);

        public static XElement CreateSoapEnvelope(XContainer? fromSoapHeaderNode, XContainer? fromSoapBodyNode) =>
            CreateSoapEnvelope(fromSoapHeaderNode, fromSoapBodyNode, envelopeNamespacePrefix: null);

        public static XElement CreateSoapEnvelope(XContainer? fromSoapHeaderNode, XContainer? fromSoapBodyNode, string? envelopeNamespacePrefix)
        {
            var envelope = CreateSoapEnvelopeWithNamespacePrefix(envelopeNamespacePrefix);

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
            SetSoapEncodingAttribute(node, Soap11Constants.SoapEncodingStandardNamespace.NamespaceName);
        }

        public static void SetSoapEncodingAttribute(XElement node, string soapEncodingNamespace)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            node.SetAttributeValue(Soap11Constants.SoapEnvelopeNamespace + "soapEncoding", soapEncodingNamespace);
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
            return node?.Descendants(Soap11Constants.SoapEnvelopeNamespace + "Fault").Any() == true;
        }

        #endregion Public Methods

        #region Private Methods

        private static XAttribute CreateSoapEnvelopeAttribute(string? envelopeNamespacePrefix)
        {
            return new XAttribute(XNamespace.Xmlns + (envelopeNamespacePrefix ?? NamespacePrefixConstants.SoapEnvelope), Soap11Constants.SoapEnvelopeNamespace);
        }

        #endregion Private Methods
    }
}
