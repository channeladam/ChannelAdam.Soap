//-----------------------------------------------------------------------
// <copyright file="Soap11BodyBuilder.cs">
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

namespace ChannelAdam.Soap.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Abstractions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BodyBuilder", Justification = "As designed")]
    public class Soap11BodyBuilder : ISoap11BodyBuilder
    {
        #region Private Fields

        private XElement? bodyElement;
        private readonly ISoap11EnvelopeBuilder envelopeBuilder;

        #endregion Private Fields

        #region Internal Constructors

        internal Soap11BodyBuilder(ISoap11EnvelopeBuilder envelopeBuilder)
        {
            this.envelopeBuilder = envelopeBuilder;
        }

        #endregion Internal Constructors

        #region Private Properties

        private XElement BodyElement
        {
            get
            {
                return this.bodyElement ??= Soap11Maker.CreateSoapBody();
            }
        }

        #endregion Private Properties

        #region Public Methods

        public ISoap11EnvelopeBuilder AddEntry(XContainer fromNode)
        {
            this.ValidateBodyForAddingAnEntry(fromNode);

            this.BodyElement.Add(fromNode);
            return this.envelopeBuilder;
        }

        public ISoap11EnvelopeBuilder AddEntry(string bodyXml)
        {
            var entry = XElement.Parse(bodyXml);

            this.ValidateBodyForAddingAnEntry(entry);

            this.BodyElement.Add(entry);
            return this.envelopeBuilder;
        }

        public ISoap11EnvelopeBuilder AddEntry(object toSerialise)
        {
            return this.AddEntry(ObjectXmlSerialiser.SerialiseObject(toSerialise, string.Empty, string.Empty));
        }

        public ISoap11EnvelopeBuilder AddEntry(object toSerialise, string toElementName, string toElementNamespace)
        {
            return this.AddEntry(ObjectXmlSerialiser.SerialiseObject(toSerialise, toElementName, toElementNamespace));
        }

        public XContainer? Build()
        {
            return this.bodyElement;
        }

        public ISoap11EnvelopeBuilder SetCustomSoapEncoding(string soapEncodingNamespace)
        {
            Soap11Maker.SetSoapEncodingAttribute(this.BodyElement, soapEncodingNamespace);
            return this.envelopeBuilder;
        }

        /// <summary>
        /// Specify the details about a fault.
        /// </summary>
        /// <param name="code">The fault code. <see cref="Soap11FaultCode"/>.</param>
        /// <param name="faultString">The fault string.</param>
        /// <returns>The SOAP 1.1 Envelope Builder.</returns>
        public ISoap11EnvelopeBuilder SetFault(Soap11FaultCode code, string faultString) =>
            SetFault(code, faultString, faultActor: null, detailEntries: null);

        /// <summary>
        /// Specify the details about a fault.
        /// </summary>
        /// <param name="code">The fault code. <see cref="Soap11FaultCode"/>.</param>
        /// <param name="faultString">The fault string.</param>
        /// <param name="faultActor">The fault actor.</param>
        /// <returns>The SOAP 1.1 Envelope Builder.</returns>
        public ISoap11EnvelopeBuilder SetFault(Soap11FaultCode code, string faultString, string? faultActor) =>
            SetFault(code, faultString, faultActor, detailEntries: null);

        /// <summary>
        /// Specify the details about a fault.
        /// </summary>
        /// <param name="code">The fault code. <see cref="Soap11FaultCode"/>.</param>
        /// <param name="faultString">The fault string.</param>
        /// <param name="faultActor">The fault actor.</param>
        /// <param name="detailEntries">A collection of fault detail XML entries.</param>
        /// <returns>The SOAP 1.1 Envelope Builder.</returns>
        public ISoap11EnvelopeBuilder SetFault(Soap11FaultCode code, string faultString, string? faultActor, IEnumerable<XContainer>? detailEntries)
        {
            this.ValidateBodyForSettingAFault();

            this.BodyElement.Add(Soap11Maker.CreateSoapFault(code, faultString, faultActor, detailEntries, this.envelopeBuilder.NamespacePrefix));
            return this.envelopeBuilder;
        }

        public ISoap11EnvelopeBuilder SetStandardSoapEncoding()
        {
            Soap11Maker.SetSoapEncodingAttribute(this.BodyElement, Soap11Constants.SoapEncodingStandardNamespace.NamespaceName);
            return this.envelopeBuilder;
        }

        #endregion Public Methods

        #region Private Methods

        private void ValidateBodyForAddingAnEntry(XContainer entry)
        {
            if (Soap11Maker.HasFault(this.bodyElement))
            {
                throw new InvalidOperationException("Cannot add the body entry because a fault has already been specified.");
            }

            if (Soap11Maker.HasFault(entry))
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
