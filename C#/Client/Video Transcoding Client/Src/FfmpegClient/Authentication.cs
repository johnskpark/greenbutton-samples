using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegClient
{
    internal static class Authentication
    {
        internal static AuthenticationHeaderValue CreateBasicAuthHeader(string username, string password)
        {
            var byteArray = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password));
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
