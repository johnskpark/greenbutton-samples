using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public class JobSubmission
    {
        public JobSubmission()
        {
            Priority = JobPriority.Medium;
            RequiredFiles = new List<RequiredFile>();
            Parameters = new List<JobParameter>();
        }

        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Type { get; set; }
        [System.Runtime.Serialization.DataMember]
        public JobPriority Priority { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<RequiredFile> RequiredFiles { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int InstanceCount { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<JobParameter> Parameters { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Settings { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string JobFile { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string CommercialAgreementId { get; set; }
    }
}
