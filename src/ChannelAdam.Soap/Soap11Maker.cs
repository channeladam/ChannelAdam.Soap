//-----------------------------------------------------------------------
// <copyright file="Soap11Maker.cs">
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

        #region CreateSoapEnvelopeWithFault

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapEnvelopeWithFault(Soap11FaultCode code, string faultString)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, faultString)));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapEnvelopeWithFault(Soap11FaultCode code, string faultString, string faultActor)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, faultString, faultActor)));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapEnvelopeWithFault(Soap11FaultCode code, string faultString, string faultActor, IEnumerable<XContainer> detailEntries)
        {
            return CreateSoapEnvelope(CreateSoapBody(CreateSoapFault(code, faultString, faultActor, detailEntries)));
        }

        #endregion CreateSoapEnvelopeWithFault

        #region CreateSoapBodyWithFault

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapBodyWithFault(Soap11FaultCode code, string faultString)
        {
            return CreateSoapBody(CreateSoapFault(code, faultString));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapBodyWithFault(Soap11FaultCode code, string faultString, string faultActor)
        {
            return CreateSoapBody(CreateSoapFault(code, faultString, faultActor));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapBodyWithFault(Soap11FaultCode code, string faultString, string faultActor, IEnumerable<XContainer> detailEntries)
        {
            return CreateSoapBody(CreateSoapFault(code, faultString, faultActor, detailEntries));
        }

        #endregion CreateSoapBodyWithFault

        #region CreateSoapFault

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapFault(Soap11FaultCode code, string faultString)
        {
            return CreateSoapFault(code, faultString, null, null);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapFault(Soap11FaultCode code, string faultString, string faultActor)
        {
            return CreateSoapFault(code, faultString, faultActor, null);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "As per the SOAP specification.")]
        public static XElement CreateSoapFault(Soap11FaultCode code, string faultString, string faultActor, IEnumerable<XContainer> detailEntries)
        {
            var fault = new XElement(Soap11Constants.SoapEnvelopeNamespace + "Fault");

            fault.Add(new XElement(XNamespace.None + "faultcode", $"{NamespacePrefixConstants.SoapEnvelope}:{code.ToString()}"));
            fault.Add(new XElement(XNamespace.None + "faultstring", faultString));

            if (!string.IsNullOrWhiteSpace(faultActor))
            {
                fault.Add(new XElement(XNamespace.None + "faultactor", faultActor));
            }

            if (detailEntries != null && detailEntries.Any())
            {
                var detail = new XElement(XNamespace.None + "detail", null);
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
            return CreateSoapBody((XContainer)null);
        }

        public static XElement CreateSoapBody(string fromXml)
        {
            return CreateSoapBody(XElement.Parse(fromXml));
        }

        public static XElement CreateSoapBody(object toSerialise)
        {
            return CreateSoapBody(ObjectXmlSerialiser.SerialiseObject(toSerialise, null, null));
        }

        public static XElement CreateSoapBody(object toSerialise, string toElementName, string toElementNamespace)
        {
            return CreateSoapBody(ObjectXmlSerialiser.SerialiseObject(toSerialise, toElementName, toElementNamespace));
        }

        public static XElement CreateSoapBody(object toSerialise, string toElementName, string toElementNamespace, XmlWriterSettings xmlWriterSettings)
        {
            return CreateSoapBody(ObjectXmlSerialiser.SerialiseObject(toSerialise, toElementName, toElementNamespace, xmlWriterSettings));
        }

        public static XElement CreateSoapBody(XContainer fromNode)
        {
            return new XElement(Soap11Constants.SoapEnvelopeNamespace + "Body", fromNode);
        }

        #endregion CreateSoapBody

        #region CreateSoapHeader

        public static XElement CreateSoapHeader()
        {
            return new XElement(Soap11Constants.SoapEnvelopeNamespace + "Header", null);
        }

        public static XElement CreateSoapHeaderActionBlock(string action)
        {
            var element = new XElement(NamespaceConstants.WebServicesAddressing + "Action", action);
            SetWebServicesAddressingNamespaceAttribute(element);
            return element;
        }

        #endregion CreateSoapHeader

        #region CreateSoapEnvelope

        public static XElement CreateSoapEnvelope()
        {
            return new XElement(Soap11Constants.SoapEnvelopeNamespace + "Envelope", CreateSoapEnvelopeAttribute());
        }

        public static XElement CreateSoapEnvelope(string fromSoapBodyXml)
        {
            return CreateSoapEnvelope(XElement.Parse(fromSoapBodyXml));
        }

        public static XElement CreateSoapEnvelope(string fromSoapHeaderXml, string fromSoapBodyXml)
        {
            return CreateSoapEnvelope(XElement.Parse(fromSoapHeaderXml), XElement.Parse(fromSoapBodyXml));
        }

        public static XElement CreateSoapEnvelope(XContainer fromSoapBodyNode)
        {
            return CreateSoapEnvelope(CreateSoapHeader(), fromSoapBodyNode);
        }

        public static XElement CreateSoapEnvelope(XContainer fromSoapHeaderNode, XContainer fromSoapBodyNode)
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

        public static bool HasFault(XContainer node)
        {
            return node?.Descendants(Soap11Constants.SoapEnvelopeNamespace + "Fault").Any() == true;
        }

        #endregion Public Methods

        #region Private Methods

        private static XAttribute CreateSoapEnvelopeAttribute()
        {
            return new XAttribute(XNamespace.Xmlns + NamespacePrefixConstants.SoapEnvelope, Soap11Constants.SoapEnvelopeNamespace);
        }

        #endregion Private Methods
    }
}
