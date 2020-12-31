//-----------------------------------------------------------------------
// <copyright file="Soap12BodyBuilder.cs">
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

namespace ChannelAdam.Soap.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Abstractions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BodyBuilder", Justification = "As designed")]
    public class Soap12BodyBuilder : ISoap12BodyBuilder
    {
        #region Private Fields

        private XElement? bodyElement;
        private readonly ISoap12EnvelopeBuilder envelopeBuilder;

        #endregion Private Fields

        #region Internal Constructors

        internal Soap12BodyBuilder(ISoap12EnvelopeBuilder envelopeBuilder)
        {
            this.envelopeBuilder = envelopeBuilder;
        }

        #endregion Internal Constructors

        #region Private Properties

        private XElement BodyElement
        {
            get
            {
                return this.bodyElement ??= Soap12Maker.CreateSoapBody();
            }
        }

        #endregion Private Properties

        #region Public Methods

        public ISoap12EnvelopeBuilder AddEntry(XContainer fromNode)
        {
            this.ValidateBodyForAddingAnEntry(fromNode);

            this.BodyElement.Add(fromNode);
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder AddEntry(string bodyXml)
        {
            var entry = XElement.Parse(bodyXml);

            this.ValidateBodyForAddingAnEntry(entry);

            this.BodyElement.Add(entry);
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder AddEntry(object toSerialise)
        {
            return this.AddEntry(ObjectXmlSerialiser.SerialiseObject(toSerialise, string.Empty, string.Empty));
        }

        public ISoap12EnvelopeBuilder AddEntry(object toSerialise, string toElementName, string toElementNamespace)
        {
            return this.AddEntry(ObjectXmlSerialiser.SerialiseObject(toSerialise, toElementName, toElementNamespace));
        }

        public XContainer? Build()
        {
            return this.bodyElement;
        }

        public ISoap12EnvelopeBuilder SetCustomSoapEncoding(string soapEncodingNamespace)
        {
            Soap12Maker.SetSoapEncodingAttribute(this.BodyElement, soapEncodingNamespace);
            return this.envelopeBuilder;
        }

        /// <summary>
        /// Specify the details about a fault.
        /// </summary>
        /// <param name="code">The fault code. <see cref="Soap12FaultCode"/>.</param>
        /// <param name="reason">The fault reason.</param>
        /// <returns>The SOAP 1.2 Envelope Builder.</returns>
        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, string reason)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, reason));
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, string? subCode, string reason)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, subCode, reason));
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, subCodeNamespace, subCode, reason));
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, IEnumerable<XContainer>? detailEntries)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, subCodeNamespace, subCode, reason, detailEntries));
            return this.envelopeBuilder;
        }

        /// <summary>
        /// Specify the details about a fault.
        /// </summary>
        /// <param name="code">The fault code. <see cref="Soap12FaultCode"/>.</param>
        /// <param name="subCodeNamespace">The namespace of the fault sub-code.</param>
        /// <param name="subCode">The fault sub-code.</param>
        /// <param name="reason">The reason for the fault.</param>
        /// <param name="reasonXmlLanguage">The xml:lang language for the text in the fault.</param>
        /// <returns>The SOAP 1.2 Envelope Builder.</returns>
        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage));
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, IEnumerable<XContainer>? detailEntries)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, detailEntries));
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node));
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node, string? role)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, role));
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace? subCodeNamespace, string? subCode, string reason, string reasonXmlLanguage, string? node, string? role, IEnumerable<XContainer>? detailEntries)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap12Maker.CreateSoapFault(code, subCodeNamespace, subCode, reason, reasonXmlLanguage, node, role, detailEntries));
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetStandardSoapEncoding()
        {
            Soap12Maker.SetStandardSoapEncodingAttribute(this.BodyElement);
            return this.envelopeBuilder;
        }

        #endregion Public Methods

        #region Private Methods

        private void ValidateBodyForAddingAnEntry(XContainer entry)
        {
            if (Soap12Maker.HasFault(this.bodyElement))
            {
                throw new InvalidOperationException("Cannot add the body entry because a fault has already been specified.");
            }

            if (Soap12Maker.HasFault(entry))
            {
                this.ValidateBodyForSettingAFault();
            }
        }

        private void ValidateBodyForSettingAFault()
        {
            if (this.bodyElement?.HasElements == true)
            {
                throw new InvalidOperationException("Cannot set a fault because the body already has an entry - either by previously adding an entry or by setting a fault.");
            }
        }

        #endregion Private Methods
    }
}
