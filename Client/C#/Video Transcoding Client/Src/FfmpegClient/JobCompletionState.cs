using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient
{
    public class JobCompletionState
    {
        public static readonly JobCompletionState InProgress = new JobCompletionState(false, "InProgress");

        public static readonly JobCompletionState Failed = new JobCompletionState(false, "Failed");

        public static JobCompletionState Success(string jobUutputUrl, string taskListUrl)
        {
            return new JobCompletionState(true, "Succeeded") { JobOutputUrl = jobUutputUrl, TaskListUrl = taskListUrl };
        }

        private JobCompletionState(bool succeeded, string displayName)
        {
            Succeeded = succeeded;
            DisplayName = displayName;
        }

        public bool Succeeded { get; private set; }
        public string DisplayName { get; private set; }
        public string JobOutputUrl { get; private set; }
        public string TaskListUrl { get; private set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
