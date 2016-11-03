//-----------------------------------------------------------------------
// <copyright file="ISoap12BodyBuilder.cs">
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

namespace ChannelAdam.Soap.Abstractions
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public interface ISoap12BodyBuilder
    {
        ISoap12EnvelopeBuilder AddEntry(string bodyXml);

        ISoap12EnvelopeBuilder AddEntry(XContainer fromNode);

        ISoap12EnvelopeBuilder AddEntry(object toSerialise);

        ISoap12EnvelopeBuilder AddEntry(object toSerialise, string toElementName, string toElementNamespace);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, string reason);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, string subCode, string reason);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace subCodeNamespace, string subCode, string reason);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace subCodeNamespace, string subCode, string reason, string reasonXmlLanguage);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace subCodeNamespace, string subCode, string reason, string reasonXmlLanguage, string node);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace subCodeNamespace, string subCode, string reason, string reasonXmlLanguage, string node, string role);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace subCodeNamespace, string subCode, string reason, IEnumerable<XContainer> detailEntries);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace subCodeNamespace, string subCode, string reason, string reasonXmlLanguage, IEnumerable<XContainer> detailEntries);

        ISoap12EnvelopeBuilder SetFault(Soap12FaultCode code, XNamespace subCodeNamespace, string subCode, string reason, string reasonXmlLanguage, string node, string role, IEnumerable<XContainer> detailEntries);

        ISoap12EnvelopeBuilder SetStandardSoapEncoding();

        ISoap12EnvelopeBuilder SetCustomSoapEncoding(string soapEncodingNamespace);
    }
}
