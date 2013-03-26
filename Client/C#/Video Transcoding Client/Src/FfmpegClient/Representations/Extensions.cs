using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient.Representations
{
    internal static class Extensions
    {
        internal static bool IsComplete(this JobDetail job)
        {
            return job.StatusEquals("Complete");
        }

        internal static bool IsInProgress(this JobDetail job)
        {
            return job.StatusEquals("InProgress")
                || job.StatusEquals("NotStarted");
        }

        private static bool StatusEquals(this JobDetail job, string status)
        {
            return status.EqualsIgnoreCase(job.Status);
        }

        private static bool EqualsIgnoreCase(this string s1, string s2)
        {
            return String.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        internal static bool IsTaskOutput(this JobIntermediateOutput taskOutput)
        {
            return "TaskOutput".EqualsIgnoreCase(taskOutput.Kind);
        }
    }
}
