
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Company.Function
{
    public static class SignalRInfo
    {
        [FunctionName("SignalRInfo")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,"get","post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "SignalRHubItems")] SignalRConnectionInfo connectionInfo,
            ILogger log)
        {
            return new OkObjectResult(connectionInfo);
        }
    }
}