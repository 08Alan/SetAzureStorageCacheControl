# Set Azure Storage CacheControl

## 您可以利用Azure Storage Explorer或Portal進行單個檔案的設置
## 但超多檔案時必須使用Azure SDK 或 Azure PowerShell進行
## 範例程式提供

- Azure SDKs:v2.9
- Visual Studio 2017
- update:20170519

```
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
```

```
//Nuget install
PM>Install-Package WindowsAzure.Storage
PM>Install-Package Microsoft.WindowsAzure.ConfigurationManager
```

## 主要程式碼

```
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
```
