using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public enum JobPriority
    {
        [System.Runtime.Serialization.EnumMember]
        Low,
        [System.Runtime.Serialization.EnumMember]
        Medium,
        [System.Runtime.Serialization.EnumMember]
        High,
        [System.Runtime.Serialization.EnumMember]
        Urgent,
    }
}
