using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public class Task
    {
        [System.Runtime.Serialization.DataMember]
        public string Id { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Status { get; set; }
        [System.Runtime.Serialization.DataMember]
        public DateTime StartTime { get; set; }
        [System.Runtime.Serialization.DataMember]
        public DateTime CompletionTime { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string InstanceId { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string DeploymentId { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int CoreCount { get; set; }
        [System.Runtime.Serialization.DataMember]
        public long ChargeTime { get; set; }
        [System.Runtime.Serialization.DataMember]
        public long NonChargeTime { get; set; }
        [System.Runtime.Serialization.DataMember]
        public IList<JobIntermediateOutput> Outputs { get; set; }
    }
}
