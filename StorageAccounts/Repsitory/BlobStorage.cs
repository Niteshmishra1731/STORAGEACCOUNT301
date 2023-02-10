using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Management.Storage.Fluent.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using BlobProperties = Azure.Storage.Blobs.Models.BlobProperties;

namespace StorageAccounts.Repsitory
{
    public class BlobStorage
    {
        static string connectionstring = "DefaultEndpointsProtocol=https;AccountName=iostorageaccount1;AccountKey=A+OSulaNAKg9dZDafp9jBozqzJ5BUHlgzJRUL7XC1IB0Z6bza6wCLzWy8dBwMczwwq45FFF3QTb++AStVKoBqw==;EndpointSuffix=core.windows.net";

        public static async Task CreateBlob(string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionstring, blobName);
                await container.CreateAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            public static async Task DeleteBlob(string blobName)
            {
                if(string.IsNullOrEmpty(blobName))
                {
                    throw new ArgumentNullException("enter blob name");
                }
                try
                {
                    BlobContainerClient container = new BlobContainerClient(connectionstring, blobName);
                    await container.DeleteAsync();
                }
                catch(Exception ex)
                {
                    throw ex;
            }
        }
        public static async Task DeleteBlobContent(string blobName, string file)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionstring, blobName);
                await container.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
        public static async Task<BlobProperties> UpdateBlobContent(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                string fileName = Path.GetTempFileName();
                BlobContainerClient container = new BlobContainerClient(connectionstring, blobName);
                BlobClient blob = container.GetBlobClient(file);
                await blob.UploadAsync(fileName);
                BlobProperties prop = await blob.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> getBlobContent(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentException("enter blob name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionstring,blobName);
                BlobClient blob = container.GetBlobClient(file);
                BlobProperties prop = await blob.GetPropertiesAsync();
                return prop;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<string>> GetBlob(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentException("enter blob name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionstring, blobName);
                List<string> names = new List<string>();
               await foreach(BlobItem a in container.GetBlobsAsync())
                {
                    names.Add(a.Name);
                }
                return names;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> DownloadBlob(string blobName,string file)
        {
            try
            {
                string path = @"C:\Users\Nitesh mishra\STORAGEACCOUNT\StorageAccounts\Downloads\" + blobName;
                BlobContainerClient container = new BlobContainerClient(connectionstring, blobName);
                BlobClient client = container.GetBlobClient(file);
                await client.DownloadToAsync(path);
                BlobProperties prop= await client.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

