//-----------------------------------------------------------------------
// <copyright file="ISoap11BodyBuilder.cs">
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

    public interface ISoap11BodyBuilder
    {
        ISoap11EnvelopeBuilder AddEntry(string bodyXml);

        ISoap11EnvelopeBuilder AddEntry(XContainer fromNode);

        ISoap11EnvelopeBuilder AddEntry(object toSerialise);

        ISoap11EnvelopeBuilder AddEntry(object toSerialise, string toElementName, string toElementNamespace);

        ISoap11EnvelopeBuilder SetFault(Soap11FaultCode code, string faultString);

        ISoap11EnvelopeBuilder SetFault(Soap11FaultCode code, string faultString, string faultActor);

        ISoap11EnvelopeBuilder SetFault(Soap11FaultCode code, string faultString, string faultActor, IEnumerable<XContainer> detailEntries);

        ISoap11EnvelopeBuilder SetStandardSoapEncoding();

        ISoap11EnvelopeBuilder SetCustomSoapEncoding(string soapEncodingNamespace);
    }
}
