using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Threading.Tasks;

namespace demogit
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri orgUrl = new Uri("https://dev.azure.com/EquipoGit2/"); //Organization URL
            String personalAccessToken = "kvp7nvvly5tax5aucj2ruwxkzcyszbk4qxr5ce6qagvcrcn62lbq"; //PersonalAccessToken
            int workItemId = 3; //ID of a work item, for example: 12

            // Create a connection
            VssConnection connection = new VssConnection(orgUrl, new VssBasicCredential(string.Empty, personalAccessToken));

            //Show details a work item
            ShowWorkItemDetails(connection, workItemId).Wait();

        }

        static private async Task ShowWorkItemDetails(VssConnection connection, int workItemId)
        {
            // Get an instance of the work item tracking client
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                //Get the specified work item
                WorkItem workitem = await witClient.GetWorkItemAsync(workItemId);

                //output the work item's field values
                foreach (var field in workitem.Fields)
                {
                    Console.WriteLine(" {0}: {1}", field.Key, field.Value);

                }
            }
            catch (AggregateException aex)
            {
                VssServiceException vssex = aex.InnerException as VssServiceException;
                if (vssex != null)
                {
                    Console.WriteLine(vssex.Message);
                }
            }
        }
    }
}

