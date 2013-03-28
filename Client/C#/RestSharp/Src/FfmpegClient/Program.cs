using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FfmpegClient
{
    class Program
    {
        private static readonly Client _client = new Client();

        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify at least one filename on the command line");
                PauseIfDebug();
                return 1;
            }

            // Don't worry about self-signed SSL certificates - useful
            // for prototyping and development.
            ServicePointManager.ServerCertificateValidationCallback = (_1, _2, _3, _4) => true;

            var filePaths = args.Select(f => Path.Combine(Environment.CurrentDirectory, f)).ToList();

            ExecuteWorkload(filePaths);

            PauseIfDebug();
            return 0;
        }

        private static void ExecuteWorkload(List<string> filePaths)
        {
            // Simple command line batch execution client:
            // * Upload the files to work on
            // * Kick off the job
            // * Poll until the job ends
            // * If the job succeeded, download the results

            Parallel.ForEach(filePaths, Upload);

            var jobUrl = SubmitJob("Ffmpeg test", filePaths);

            var completionState = JobCompletionState.InProgress;

            while (completionState == JobCompletionState.InProgress)
            {
                completionState = GetJobCompletionState(jobUrl);
                Console.WriteLine("Job status: " + completionState);
                Thread.Sleep(2000);
            }

            if (completionState.Succeeded)
            {
                var taskOutputUrls = GetTaskOutputUrls(completionState.TaskListUrl);
                var jobOutputUrl = completionState.JobOutputUrl;
                var outputUrls = taskOutputUrls.Concat(new[] { jobOutputUrl });

                Parallel.ForEach(outputUrls, Download);
            }
        }

        // Tracing wrappers for calls in the Client class

        private static void Upload(string filePath)
        {
            Console.WriteLine("Uploading file " + filePath + "...");
            _client.UploadFile(filePath);
            Console.WriteLine("Finished uploading file " + filePath);
        }

        private static string SubmitJob(string jobName, IEnumerable<string> filePaths)
        {
            Console.Write("Submitting job " + jobName + "... ");
            var jobUrl = _client.SubmitJob(jobName, filePaths.ToArray());
            Console.WriteLine("done");
            return jobUrl;
        }

        private static JobCompletionState GetJobCompletionState(string jobUrl)
        {
            return _client.GetJobCompletionState(jobUrl);
        }

        private static IReadOnlyList<string> GetTaskOutputUrls(string taskListUrl)
        {
            return _client.GetTaskOutputUrls(taskListUrl);
        }

        private static void Download(string url)
        {
            Console.WriteLine("Downloading " + url + "... ");
            _client.Download(url);
            Console.WriteLine("Finished downloading " + url);
        }

        // Helper to stop the program exiting immediately when running it from Visual
        // Studio -- gives us time to view the console output.

        [Conditional("DEBUG")]
        private static void PauseIfDebug()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
