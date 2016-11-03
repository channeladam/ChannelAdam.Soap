//-----------------------------------------------------------------------
// <copyright file="Soap12EnvelopeBuilder.cs">
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

    public class Soap12EnvelopeBuilder : ISoap12EnvelopeBuilder
    {
        #region Private Fields

        private Soap12BodyBuilder bodyBuilder;
        private Soap12HeaderBuilder headerBuilder;

        #endregion Private Fields

        #region Internal Constructors

        internal Soap12EnvelopeBuilder()
        {
        }

        #endregion Internal Constructors

        #region Public Properties

        public ISoap12BodyBuilder WithBody
        {
            get
            {
                if (this.bodyBuilder == null)
                {
                    this.bodyBuilder = new Soap12BodyBuilder(this);
                }

                return this.bodyBuilder;
            }
        }

        public ISoap12HeaderBuilder WithHeader
        {
            get
            {
                if (this.headerBuilder == null)
                {
                    this.headerBuilder = new Soap12HeaderBuilder(this);
                }

                return this.headerBuilder;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public XContainer Build()
        {
            return Soap12Maker.CreateSoapEnvelope(this.headerBuilder?.Build(), this.bodyBuilder?.Build());
        }

        #endregion Public Methods
    }
}
