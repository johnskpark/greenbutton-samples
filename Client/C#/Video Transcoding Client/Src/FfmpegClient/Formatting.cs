using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient
{
    // Helper class for working with multipart/form-data requests, such
    // as PUT /api/files/filename.

    internal static class Formatting
    {
        public static MultipartFormDataContent CreateMultiPartFormDataContent(string fileName, string username, DateTime fileTimestamp, StreamContent fileContent, long streamLength, string contentType = "application/octet-stream")
        {
            var timeStamp = fileTimestamp.ToString("o");

            return new MultipartFormDataContent
            {
                { new StringContent(fileName), "OriginalFilePath" },
                { new StringContent(username), "OwnedBy" },
                { new StringContent(streamLength.ToString(CultureInfo.InvariantCulture)), "ContentLength" },
                { new StringContent(contentType), "ContentType" },
                { new StringContent(username), "LastModifiedBy" },
                { new StringContent(timeStamp), "LastModifiedTime" },
                { fileContent, "Filename", fileName },  // TODO: rename?
            };
        }
    }
}
