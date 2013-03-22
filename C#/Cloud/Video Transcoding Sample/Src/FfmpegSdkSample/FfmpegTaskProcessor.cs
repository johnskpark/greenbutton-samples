using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using GreenButton.Cloud;

namespace FfmpegSdkSample
{
    public class FfmpegTaskProcessor : ParallelTaskProcessor
    {
        protected override TaskProcessResult RunExternalTaskProcess(ITask task, TaskExecutionSettings settings)
        {
            // Get the one and only asset/file associated with this particular task
            // (the job splitter will have created a task for each video file passed in).
            var inputFile = task.RequiredFiles[0].Name;

            // As this example just gets ffmpeg to rewrite the input file, we will
            // use the same file extension as the input file.
            // The output file will be written as {TaskId}.{extension} e.g. 1.mp4
            var outputFile = string.Concat(task.TaskId.ToString(), Path.GetExtension(inputFile));

            // Add the name of the application exe to the drive letter/path of the mounted VHD.
            var externalProgramPath = ExecutablePath("ffmpeg.exe");

            // We aren't going to tell ffmepg to do any transcoding or compression or anything,
            // but you could imagine that user defined settings could be passed in with the job
            // when it was submitted.  The job splitter could then set these in turn against each
            // of the tasks it created.  Then the task processor could use those settings to
            // set up particular arguments for ffmpeg.

            //NOTE: usage: ffmpeg [options] [[infile options] -i infile]... {[outfile options] outfile}...
            var externalProgramArguments = String.Format("-i \"{0}\" \"{1}\"", inputFile, outputFile);

            // Run ffmpeg
            var ffmpegResult = new ExternalProcess
            {
                CommandPath = externalProgramPath,
                Arguments = externalProgramArguments,
                WorkingDirectory = LocalStoragePath,
            }.Run();

            // Return the output file that has been created.
            return TaskProcessResult.FromExternalProcessResult(ffmpegResult, outputFile);
        }

        protected override JobResult RunExternalMergeProcess(ITask mergeTask, TaskExecutionSettings settings)
        {
            // We're not doing any merging in this case so just return an empty file.
            var outputFile = LocalPath("JobOutput.txt");
            File.WriteAllText(outputFile, String.Empty);
            return new JobResult { OutputFile = outputFile };
        }
    }
}