using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    public class SubmittedJob
    {
        public Guid JobId { get; set; }
        public LinkRelation Link { get; set; }
    }
}
