//-----------------------------------------------------------------------
// <copyright file="Soap11EnvelopeBuilder.cs">
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
    using System.Xml.Linq;

    using Abstractions;

    public class Soap11EnvelopeBuilder : ISoap11EnvelopeBuilder
    {
        #region Private Fields

        private Soap11BodyBuilder? bodyBuilder;
        private Soap11HeaderBuilder? headerBuilder;
        private string? envelopeNamespacePrefix;

        #endregion Private Fields

        #region Internal Constructors

        internal Soap11EnvelopeBuilder()
        {
        }

        #endregion Internal Constructors

        #region Public Properties

        public string? NamespacePrefix => this.envelopeNamespacePrefix;

        public ISoap11BodyBuilder WithBody => this.bodyBuilder ??= new Soap11BodyBuilder(this);

        public ISoap11HeaderBuilder WithHeader => this.headerBuilder ??= new Soap11HeaderBuilder(this);

        #endregion Public Properties

        #region Public Methods

        public ISoap11EnvelopeBuilder SetNamespacePrefix(string prefix)
        {
            this.envelopeNamespacePrefix = prefix;
            return this;
        }

        public XContainer Build()
        {
            return Soap11Maker.CreateSoapEnvelope(this.headerBuilder?.Build(), this.bodyBuilder?.Build(), this.envelopeNamespacePrefix);
        }

        #endregion Public Methods
    }
}
