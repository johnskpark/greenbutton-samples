using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public class Error
    {
        [System.Runtime.Serialization.DataMember]
        public string Message { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string ExceptionType { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string ExceptionMessage { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string StackTrace { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<ValidationError> ValidationErrors { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Namespace = "")]  // required only if using XML
    public class ValidationError
    {
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Message { get; set; }
    }
}
