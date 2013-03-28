using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    public class JobDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PercentComplete { get; set; }
        public string Settings { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CompletionTime { get; set; }
        public double CappedPrice { get; set; }
        public int InstanceCount { get; set; }
        public int TaskCount { get; set; }
        public string OutputFileName { get; set; }
        public LinkRelation PreviewLink { get; set; }
        public LinkRelation OutputLink { get; set; }
        public LinkRelation TaskListLink { get; set; }
    }
}
