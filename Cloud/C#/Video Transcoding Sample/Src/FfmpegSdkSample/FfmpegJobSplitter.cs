using System;
using System.Collections.Generic;
using System.Configuration;
using GreenButton.Cloud;

namespace FfmpegSdkSample
{
    public class FfmpegJobSplitter : JobSplitter
    {
        protected override IEnumerable<TaskSpecifier> Split(IJob job, JobSplitSettings settings)
        {
            // Iterate through each of the video files that have been passed in
            // and create a task for each one.
            foreach (var file in job.Files)
            {
                yield return new TaskSpecifier
                {
                    RequiredFiles = new[] { file },  // The single video file for this iteration so this task will process that one video
                    Parameters = job.Parameters,
                };
            }
        }
    }
}
