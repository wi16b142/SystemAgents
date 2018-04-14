﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [DataContract]
    public class CoreMessage
    {
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Data { get; set; }
    }
}
