using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenButton.Cloud;

namespace FfmpegSdkSample
{
    public static class ApplicationDefinition
    {
        public static readonly CloudApplication FfmpegApplication = new ParallelCloudApplication
        {
            ApplicationName = "FfmpegSample",
            JobType = "FfmpegSample",
            JobSplitterType = typeof(FfmpegJobSplitter),
            TaskProcessorType = typeof(FfmpegTaskProcessor),
        };
    }
}
