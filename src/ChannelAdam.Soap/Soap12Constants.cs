//-----------------------------------------------------------------------
// <copyright file="Soap12Constants.cs">
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
    using System.Xml.Linq;

    public static class Soap12Constants
    {
        #region Public Fields

        public static readonly string ContentType = "application/soap+xml";

        #endregion Public Fields

        #region Public Properties

        public static XNamespace SoapEncodingNoneNamespace
        {
            get { return "http://www.w3.org/2003/05/soap-encoding/encoding/none"; }
        }

        public static XNamespace SoapEncodingStandardNamespace
        {
            get { return "http://www.w3.org/2003/05/soap-encoding"; }
        }

        public static XNamespace SoapEnvelopeNamespace
        {
            get { return "http://www.w3.org/2003/05/soap-envelope"; }
        }

        public static XNamespace SoapRoleNextNamespace
        {
            get { return "http://www.w3.org/2003/05/soap-envelope/role/next"; }
        }

        public static XNamespace SoapRoleNoneNamespace
        {
            get { return "http://www.w3.org/2003/05/soap-envelope/role/none"; }
        }

        public static XNamespace SoapRoleUltimateReceiverNamespace
        {
            get { return "http://www.w3.org/2003/05/soap-envelope/role/ultimateReceiver"; }
        }

        #endregion Public Properties
    }
}
