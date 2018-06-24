//-----------------------------------------------------------------------
// <copyright file="Soap12HeaderBuilder.cs">
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

namespace ChannelAdam.Soap.Internal
{
    using System;
    using System.Xml.Linq;

    using Abstractions;

    public class Soap12HeaderBuilder : ISoap12HeaderBuilder
    {
        #region Private Fields

        private readonly ISoap12EnvelopeBuilder envelopeBuilder;

        private XElement headerElement;

        #endregion Private Fields

        #region Internal Constructors

        internal Soap12HeaderBuilder(ISoap12EnvelopeBuilder envelopeBuilder)
        {
            this.envelopeBuilder = envelopeBuilder;
        }

        #endregion Internal Constructors

        #region Private Properties

        private XElement HeaderElement
        {
            get
            {
                return this.headerElement ?? (this.headerElement = Soap12Maker.CreateSoapHeader());
            }
        }

        #endregion Private Properties

        #region Public Methods

        public ISoap12EnvelopeBuilder AddAction(string action)
        {
            XContainer actionBlock = Soap12Maker.CreateSoapHeaderActionBlock(action);
            this.HeaderElement.Add(actionBlock);
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder AddBlock(XContainer headerBlock)
        {
            this.HeaderElement.Add(headerBlock);
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder AddBlock(string headerBlockXml)
        {
            var entry = XElement.Parse(headerBlockXml);
            this.HeaderElement.Add(entry);
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder AddBlock(object toSerialise)
        {
            return this.AddBlock(ObjectXmlSerialiser.SerialiseObject(toSerialise, null, null));
        }

        public ISoap12EnvelopeBuilder AddBlock(object toSerialise, string toElementName, string toElementNamespace)
        {
            return this.AddBlock(ObjectXmlSerialiser.SerialiseObject(toSerialise, toElementName, toElementNamespace));
        }

        public XContainer Build()
        {
            return this.headerElement;
        }

        public ISoap12EnvelopeBuilder SetCustomSoapEncoding(string soapEncodingNamespace)
        {
            Soap12Maker.SetSoapEncodingAttribute(this.HeaderElement, soapEncodingNamespace);
            return this.envelopeBuilder;
        }

        public ISoap12EnvelopeBuilder SetStandardSoapEncoding()
        {
            Soap12Maker.SetSoapEncodingAttribute(this.HeaderElement, Soap12Constants.SoapEncodingStandardNamespace.NamespaceName);
            return this.envelopeBuilder;
        }

        #endregion Public Methods
    }
}
