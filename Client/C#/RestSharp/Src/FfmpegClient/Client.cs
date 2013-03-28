using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RestSharp;
using FfmpegClient.Representations;
using Task = System.Threading.Tasks.Task;

namespace FfmpegClient
{
    public class Client
    {
        private readonly RestClient _restClient = new RestClient();

        private readonly string _baseAddress = ConfigurationManager.AppSettings["WebServiceHost"];
        private readonly string _username = ConfigurationManager.AppSettings["Username"];
        private readonly string _password = ConfigurationManager.AppSettings["Password"];

        public Client()
        {
            _restClient.AddDefaultHeader("x-gb-version", "1.0");
            _restClient.AddDefaultHeader("Accept", "application/json");
            _restClient.Authenticator = new HttpBasicAuthenticator(_username, _password);
        }

        public void UploadFile(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                DateTime timestamp = File.GetLastWriteTimeUtc(filePath);
                long length = stream.Length;

                var request = new RestRequest(_baseAddress + "/api/files/{name}", Method.PUT);
                request.AddUrlSegment("name", fileName);

                request.AddParameter("OriginalFilePath", filePath, ParameterType.GetOrPost);
                request.AddParameter("OwnedBy", _username, ParameterType.GetOrPost);
                request.AddParameter("ContentLength", length, ParameterType.GetOrPost);
                request.AddParameter("ContentType", "application/octet-stream", ParameterType.GetOrPost);
                request.AddParameter("LastModifiedBy", _username, ParameterType.GetOrPost);
                request.AddParameter("LastModifiedTime", timestamp.ToString("o"), ParameterType.GetOrPost);
                request.AddFile("Filename", filePath);

                var response = _restClient.Execute(request);

                EnsureSucceeded(response);
            }
        }

        public string SubmitJob(string jobName, string[] filePaths)
        {
            JobSubmission jobSubmission = new JobSubmission
            {
                Name = jobName,
                Type = "FfmpegSample",
                RequiredFiles = filePaths.Select(f => new RequiredFile { Name = Path.GetFileName(f), Timestamp = File.GetLastWriteTimeUtc(f) }).ToList(),
                JobFile = Path.GetFileName(filePaths[0]),  // not actually used in this case, but the API requires a job file
            };

            var request = new RestRequest(_baseAddress + "/api/jobs", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(jobSubmission);

            IRestResponse<SubmittedJob> response = _restClient.Execute<SubmittedJob>(request);

            EnsureSucceeded(response);

            return response.Data.Link.Href;
        }

        public JobCompletionState GetJobCompletionState(string jobUrl)
        {
            var response = _restClient.Execute<JobDetail>(new RestRequest(jobUrl, Method.GET));

            EnsureSucceeded(response);

            var job = response.Data;

            if (job.IsInProgress())
            {
                return JobCompletionState.InProgress;
            }
            else if (job.IsComplete())
            {
                return JobCompletionState.Success(job.OutputLink.Href, job.TaskListLink.Href);
            }
            else
            {
                return JobCompletionState.Failed;
            }
        }

        public IReadOnlyList<string> GetTaskOutputUrls(string taskListUrl)
        {
            var response = _restClient.Execute<TaskList>(new RestRequest(taskListUrl, Method.GET));

            EnsureSucceeded(response);

            var taskOutputUrls = response.Data
                                         .Tasks
                                         .SelectMany(t => t.Outputs.Where(o => o.IsTaskOutput()).Select(o => o.Link.Href))
                                         .ToList()
                                         .AsReadOnly();
            return taskOutputUrls;
        }

        public void Download(string url)
        {
            var response = _restClient.Execute(new RestRequest(url, Method.GET));

            EnsureSucceeded(response);

            var destinationPath = Path.Combine(Environment.CurrentDirectory, response.GetHeader("Content-Disposition").ToString().ParseFilename());
            using (var stm = File.Create(destinationPath))
            {
                stm.Write(response.RawBytes, 0, response.RawBytes.Length);
            }
        }

        private static void EnsureSucceeded(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                throw new WebException(response.ErrorMessage, response.ErrorException);
            }

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted)
            {
                return;
            }

            var error = response.Content;
            Console.WriteLine("Failed with " + response.StatusCode + " " + response.StatusDescription + ": " + error);
            throw new WebException(error);
        }
    }
}
