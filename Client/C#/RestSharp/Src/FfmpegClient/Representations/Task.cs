using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    public class Task
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CompletionTime { get; set; }
        public string InstanceId { get; set; }
        public string DeploymentId { get; set; }
        public int CoreCount { get; set; }
        public long ChargeTime { get; set; }
        public long NonChargeTime { get; set; }
        public List<JobIntermediateOutput> Outputs { get; set; }
    }
}
