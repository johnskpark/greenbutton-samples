using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public class SubmittedJob
    {
        [System.Runtime.Serialization.DataMember]
        public Guid JobId { get; set; }
        [System.Runtime.Serialization.DataMember]
        public LinkRelation Link { get; set; }
    }
}
