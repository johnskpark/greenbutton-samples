using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public class JobParameter
    {
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Value { get; set; }
    }
}
