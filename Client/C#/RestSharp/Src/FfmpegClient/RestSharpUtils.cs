using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace FfmpegClient
{
    internal static class RestSharpUtils
    {
        internal static object GetHeader(this IRestResponse response, string headerName)
        {
            var header = response.Headers.FirstOrDefault(h => h.Name.Equals(headerName, StringComparison.OrdinalIgnoreCase));
            if (header == null)
            {
                return null;
            }
            return header.Value;
        }

        private static string[] SplitTrim(this string str, char ch)
        {
            return str.Split(ch).Select(s => s.Trim()).ToArray();
        }

        internal static string ParseFilename(this string contentDispositionHeader)
        {
            if (contentDispositionHeader == null)
            {
                return null;
            }

            var parts = contentDispositionHeader.SplitTrim(';')
                                                .Select(s => s.SplitTrim('='));

            var filenamePart = parts.FirstOrDefault(p => p.Length == 2 && p[0].Equals("filename", StringComparison.OrdinalIgnoreCase));

            if (filenamePart == null)
            {
                return null;
            }

            return filenamePart[1];
        }
    }
}
