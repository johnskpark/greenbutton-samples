using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    public class JobSubmission
    {
        public JobSubmission()
        {
            Priority = JobPriority.Medium;
            RequiredFiles = new List<RequiredFile>();
            Parameters = new List<JobParameter>();
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public JobPriority Priority { get; set; }
        public List<RequiredFile> RequiredFiles { get; set; }
        public int InstanceCount { get; set; }
        public List<JobParameter> Parameters { get; set; }
        public string Settings { get; set; }
        public string JobFile { get; set; }
        public string CommercialAgreementId { get; set; }
    }
}
