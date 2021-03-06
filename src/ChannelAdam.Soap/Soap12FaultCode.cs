﻿//-----------------------------------------------------------------------
// <copyright file="Soap12FaultCode.cs">
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
    public enum Soap12FaultCode
    {
        /// <summary>
        /// Sender fault.
        /// </summary>
        Sender = 0,

        /// <summary>
        /// Receiver fault.
        /// </summary>
        Receiver,

            /// <summary>
        /// Version mismatch fault.
        /// </summary>
        VersionMismatch,

        /// <summary>
        /// Must understand fault.
        /// </summary>
        MustUnderstand,

        /// <summary>
        /// Data encoding unknown fault.
        /// </summary>
        DataEncodingUnknown
    }
}