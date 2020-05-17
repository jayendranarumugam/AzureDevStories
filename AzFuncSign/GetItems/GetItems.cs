using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Company.Function
{
    public static class GetItems
    {
        [FunctionName("GetItems")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,"get","post")] HttpRequest req,
            [CosmosDB("MyProductDB", "Items", ConnectionStringSetting = "AzureWebJobsCosmosDBConnectionString")]
                IEnumerable<object> updatedItems,
            ILogger log)
        {
            try{
                return new OkObjectResult(updatedItems);
            }
            catch(Exception e){
                log.LogError(e.Message.ToString());
            }
             return new OkObjectResult(updatedItems);
            
        }
    }
}