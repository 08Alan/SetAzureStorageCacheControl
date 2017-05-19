using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types

namespace SetStorageCacheControl
{
    class Program
    {
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=youraccountname;AccountKey=my64key;EndpointSuffix=core.windows.net";

        static void Main()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("downloadfile");

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, true))
            {
                try
                {
                    
                    string myItemEndPoint = item.Uri.ToString().Replace("https://YourStorageAccountName.blob.core.windows.net/YourBlobServiceContainerName/", "");
                    CloudBlob blob = container.GetBlobReference(myItemEndPoint);
                    blob.Properties.CacheControl = "public,max-age=18000";//3600/H
                    blob.SetProperties();
                    Console.WriteLine(myItemEndPoint);
                }
                catch (Exception)
                {
                    Console.WriteLine("can't find");
                }
            }
        }
    }

}
