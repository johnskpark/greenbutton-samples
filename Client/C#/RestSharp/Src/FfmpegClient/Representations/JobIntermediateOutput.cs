using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    public class JobIntermediateOutput
    {
        public int? TaskId { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public LinkRelation Link { get; set; }
    }
}
