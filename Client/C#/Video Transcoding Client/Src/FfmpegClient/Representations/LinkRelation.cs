using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public class LinkRelation
    {
        [System.Runtime.Serialization.DataMember]
        public string Rel { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Href { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Title { get; set; }
    }
}
