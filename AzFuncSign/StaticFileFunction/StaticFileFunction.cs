using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;
using System.Web;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Product.Function
{
    public static class StaticFileFunction
    {
        const string staticFilesFolder = "www";
        static string defaultPage = String.IsNullOrEmpty(GetEnvironmentVariable("DEFAULT_PAGE")) ?
    "index.html" : GetEnvironmentVariable("DEFAULT_PAGE");

        [FunctionName("StaticFileFunction")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var filePath = GetFilePath(req, log);

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(filePath, FileMode.Open);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType =new MediaTypeHeaderValue(GetMimeType(filePath));
                return response;
            }
            catch (Exception e)
            {
                string name = e.Message.ToString();
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        private static string GetScriptPath()
            => Path.Combine(GetEnvironmentVariable("HOME"), @"site\wwwroot");

        private static string GetEnvironmentVariable(string name)
            => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        private static string GetFilePath(HttpRequest req, ILogger log)
        {
            var path = req.Query["file"];

            var staticFilesPath = Path.GetFullPath(Path.Combine(GetScriptPath(), staticFilesFolder));
            var fullPath = Path.GetFullPath(Path.Combine(staticFilesPath, path));

            if (!IsInDirectory(staticFilesPath, fullPath))
            {
                throw new ArgumentException("Invalid path");
            }

            var isDirectory = Directory.Exists(fullPath);
            if (isDirectory)
            {
                fullPath = Path.Combine(fullPath, defaultPage);
            }

            return fullPath;
        }

        private static bool IsInDirectory(string parentPath, string childPath)
        {
            var parent = new DirectoryInfo(parentPath);
            var child = new DirectoryInfo(childPath);

            var dir = child;
            do
            {
                if (dir.FullName == parent.FullName)
                {
                    return true;
                }
                dir = dir.Parent;
            } while (dir != null);

            return false;
        }

        private static string GetMimeType(string filePath)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            provider.TryGetContentType(filePath, out contentType);
            return contentType;
        }
    }
}
