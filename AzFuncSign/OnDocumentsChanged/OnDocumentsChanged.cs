using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Threading.Tasks;

namespace Company.Function
{
    public static class OnDocumentsChanged
    {
        [FunctionName("OnDocumentsChanged")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "MyProductDB",
            collectionName: "Items",
            ConnectionStringSetting = "AzureWebJobsCosmosDBConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]
             IEnumerable<object> updatedItems,
            [SignalR(HubName = "SignalRHubItems")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            foreach(var item in updatedItems)
            {
                await signalRMessages.AddAsync(new SignalRMessage
                {
                    Target = "SignalRHubUpdatedItems",
                    Arguments = new[] { item }
                });
            }
        }
    }
}
