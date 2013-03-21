using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public class JobDetail
    {
        [System.Runtime.Serialization.DataMember]
        public string Id { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Type { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int PercentComplete { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Settings { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Status { get; set; }
        [System.Runtime.Serialization.DataMember]
        public DateTime SubmissionTime { get; set; }
        [System.Runtime.Serialization.DataMember]
        public DateTime StartTime { get; set; }
        [System.Runtime.Serialization.DataMember]
        public DateTime CompletionTime { get; set; }
        [System.Runtime.Serialization.DataMember]
        public double CappedPrice { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int InstanceCount { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int TaskCount { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string OutputFileName { get; set; }
        [System.Runtime.Serialization.DataMember]
        public LinkRelation PreviewLink { get; set; }
        [System.Runtime.Serialization.DataMember]
        public LinkRelation OutputLink { get; set; }
        [System.Runtime.Serialization.DataMember]
        public LinkRelation TaskListLink { get; set; }
    }
}
