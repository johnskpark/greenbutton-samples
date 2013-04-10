using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FfmpegClient.Representations;
using Task = System.Threading.Tasks.Task;

namespace FfmpegClient
{
    public class Client
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly string _baseAddress = ConfigurationManager.AppSettings["WebServiceHost"];
        private readonly string _username = ConfigurationManager.AppSettings["Username"];
        private readonly string _password = ConfigurationManager.AppSettings["Password"];

        public Client()
        {
            _httpClient.BaseAddress = new Uri(_baseAddress);
            _httpClient.DefaultRequestHeaders.Add("x-gb-version", "1.0");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = Authentication.CreateBasicAuthHeader(_username, _password);
        }

        public async Task UploadFile(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                DateTime timestamp = File.GetLastWriteTimeUtc(filePath);
                StreamContent content = new StreamContent(stream, bufferSize: 1024);
                long length = stream.Length;

                var uploadFileData = Formatting.CreateMultiPartFormDataContent(fileName, _username, timestamp, content, length);

                var response = await _httpClient.PutAsync("/api/files/" + HttpUtility.UrlEncode(fileName), uploadFileData);

                EnsureSucceeded(response);
            }
        }

        public async Task<string> SubmitJob(string jobName, string[] filePaths)
        {
            JobSubmission jobSubmission = new JobSubmission
            {
                Name = jobName,
                Type = "FfmpegSample",
                RequiredFiles = filePaths.Select(f => new RequiredFile { Name = Path.GetFileName(f), Timestamp = File.GetLastWriteTimeUtc(f) }).ToList(),
                JobFile = Path.GetFileName(filePaths[0]),  // not actually used in this case, but the API requires a job file
            };

            var response = await _httpClient.PostAsJsonAsync("/api/jobs", jobSubmission);

            EnsureSucceeded(response);

            var result = await response.Content.ReadAsAsync<SubmittedJob>();
            return result.Link.Href;
        }

        public async Task<JobCompletionState> GetJobCompletionState(string jobUrl)
        {
            var response = await _httpClient.GetAsync(jobUrl);

            EnsureSucceeded(response);

            var job = await response.Content.ReadAsAsync<JobDetail>();

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

        public async Task<IReadOnlyList<string>> GetTaskOutputUrls(string taskListUrl)
        {
            var response = await _httpClient.GetAsync(taskListUrl);

            EnsureSucceeded(response);

            var result = await response.Content.ReadAsAsync<TaskList>();
            var taskOutputUrls = result.Tasks
                                       .SelectMany(t => t.Outputs.Where(o => o.IsTaskOutput()).Select(o => o.Link.Href))
                                       .ToList()
                                       .AsReadOnly();
            return taskOutputUrls;
        }

        public async Task Download(string url)
        {
            var response = await _httpClient.GetAsync(url);

            EnsureSucceeded(response);

            var destinationPath = Path.Combine(Environment.CurrentDirectory, response.Content.Headers.ContentDisposition.FileName);
            using (var stm = File.Create(destinationPath))
            {
                await response.Content.CopyToAsync(stm);
            }
        }

        private static async void EnsureSucceeded(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsAsync<Error>();
                var message = error.Message;
                if (error.ValidationErrors != null && error.ValidationErrors.Any())
                {
                    message = message + Environment.NewLine + String.Join(Environment.NewLine, error.ValidationErrors.Select(e => e.Name + ": " + e.Message));
                }
                Console.WriteLine("Failed with " + response.StatusCode + " " + response.ReasonPhrase + ": " + message);
                throw new WebException(message);
            }
        }
    }
}
