//-----------------------------------------------------------------------
// <copyright file="NamespaceConstants.cs">
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

    public static class NamespaceConstants
    {
        #region Public Properties

        public static XNamespace None
        {
            get { return XNamespace.None; }
        }

        public static XNamespace WebServicesAddressing
        {
            get { return "http://www.w3.org/2005/08/addressing"; }
        }

        public static XNamespace Xml
        {
            get { return XNamespace.Xml; }
        }

        public static XNamespace Xmlns
        {
            get { return XNamespace.Xmlns; }
        }

        public static XNamespace XmlSchema
        {
            get { return "http://www.w3.org/2001/XMLSchema"; }
        }

        public static XNamespace XmlSchemaInstance
        {
            get { return "http://www.w3.org/2001/XMLSchema-instance"; }
        }

        #endregion Public Properties
    }
}
