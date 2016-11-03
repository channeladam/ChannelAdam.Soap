//-----------------------------------------------------------------------
// <copyright file="Soap11HeaderBuilder.cs">
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
    using System.Xml.Linq;

    using Abstractions;

    public class Soap11HeaderBuilder : ISoap11HeaderBuilder
    {
        #region Private Fields

        private readonly ISoap11EnvelopeBuilder envelopeBuilder;

        private XElement headerElement;

        #endregion Private Fields

        #region Internal Constructors

        internal Soap11HeaderBuilder(ISoap11EnvelopeBuilder envelopeBuilder)
        {
            this.envelopeBuilder = envelopeBuilder;
        }

        #endregion Internal Constructors

        #region Private Properties

        private XElement HeaderElement
        {
            get
            {
                if (this.headerElement == null)
                {
                    this.headerElement = Soap11Maker.CreateSoapHeader();
                }

                return this.headerElement;
            }
        }

        #endregion Private Properties

        #region Public Methods

        public ISoap11EnvelopeBuilder AddAction(string action)
        {
            XContainer actionBlock = Soap11Maker.CreateSoapHeaderActionBlock(action);
            this.HeaderElement.Add(actionBlock);
            return this.envelopeBuilder;
        }

        public ISoap11EnvelopeBuilder AddBlock(XContainer headerBlock)
        {
            this.HeaderElement.Add(headerBlock);
            return this.envelopeBuilder;
        }

        public XContainer Build()
        {
            return this.headerElement;
        }

        public ISoap11EnvelopeBuilder SetCustomSoapEncoding(string soapEncodingNamespace)
        {
            Soap11Maker.SetSoapEncodingAttribute(this.HeaderElement, soapEncodingNamespace);
            return this.envelopeBuilder;
        }

        public ISoap11EnvelopeBuilder SetStandardSoapEncoding()
        {
            Soap11Maker.SetSoapEncodingAttribute(this.HeaderElement, Soap11Constants.SoapEncodingStandardNamespace.NamespaceName);
            return this.envelopeBuilder;
        }

        #endregion Public Methods
    }
}
