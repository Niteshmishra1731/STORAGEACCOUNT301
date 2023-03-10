using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.Azure.Management.AppService.Fluent.Models;
using Microsoft.Rest;

namespace StorageAccounts.Repsitory
{
    public class FileStorage
    {
        public static string connectionstring = "DefaultEndpointsProtocol=https;AccountName=storageaccount301111;AccountKey=8yIJCwH1qFzARUJ6CLlZmPr+BJUJI/dkrRoM/PxYRSt5aw+6rdrW8Gdyk3G0xPsL3xHn8Um9kjl4+AStRnnTPg==;EndpointSuffix=core.windows.net";
        static ShareServiceClient shareServiceClient = null;
        public static async Task CreateFile(string fileName)
        {
            try
            {
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient=shareServiceClient.GetShareClient(fileName);
                await serviceClient.CreateIfNotExistsAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task CreateDirectory(string directoryName,string fileName)
        {
            try
            {
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileName);
                ShareDirectoryClient rootDir = serviceClient.GetRootDirectoryClient();
                ShareDirectoryClient fileDir = rootDir.GetSubdirectoryClient(directoryName);
                await fileDir.CreateIfNotExistsAsync();             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task UploadFile(IFormFile file, string directoryName,string fileShareName)
        {
            string fileName = file.FileName;
            shareServiceClient = new ShareServiceClient(connectionstring);
            var serviceClient = shareServiceClient.GetShareClient(fileShareName);
            var directory = serviceClient.GetDirectoryClient(directoryName);
            var fileStorage= directory.GetFileClient(fileName);
            await using (var data = file.OpenReadStream())
            {
                await fileStorage.CreateAsync(data.Length);
                await fileStorage.UploadAsync(data);
            }
        }
        public static async Task DeleteDirectory(string directoryName,string fileShareName)
        {
            shareServiceClient = new ShareServiceClient(connectionstring);
            var serviceClient = shareServiceClient.GetShareClient(fileShareName);
            var dir=serviceClient.GetDirectoryClient(directoryName);
            await dir.DeleteAsync();
        }
        public static async Task DeleteFile(string directoryName,string fileShareName,string fileName)
        {
            shareServiceClient = new ShareServiceClient(connectionstring);
            var serviceClient=shareServiceClient.GetShareClient(fileShareName);
            var dir = serviceClient.GetDirectoryClient(directoryName);
            var file=dir.GetFileClient(fileName);
            await file.DeleteAsync();
        }
        public static async Task DeleteFolder(string fileName)
        {
            shareServiceClient = new ShareServiceClient(connectionstring);
            var serviceClient=shareServiceClient.GetShareClient(fileName);
            await serviceClient.DeleteIfExistsAsync();
        }
        public static async Task<List<string>> GetAllFiles(string directoryName,string fileShareName)
        {
            shareServiceClient = new ShareServiceClient(connectionstring);
            var serviceClient = shareServiceClient.GetShareClient(fileShareName);
            var files = serviceClient.GetDirectoryClient(directoryName);
            var Directory=serviceClient.GetDirectoryClient(directoryName);
            List<string> name = new List<string>();
            await foreach(ShareFileItem file in Directory.GetFilesAndDirectoriesAsync())
            {
                name.Add(file.Name);  
            }
            return name;
        }
        public static async Task DownloadFile(string directoryName,string fileShareName,string fileName)
        {
            string path = @"C:\Users\Nitesh mishra\STORAGEACCOUNT\StorageAccounts\Downloads\" + fileName;
            shareServiceClient=new ShareServiceClient(connectionstring);
            var serviceClient = shareServiceClient.GetShareClient(fileShareName);
            var dir = serviceClient.GetDirectoryClient(directoryName);
            var file = dir.GetFileClient(fileName);
            ShareFileDownloadInfo dwnlod = await file.DownloadAsync();
            using (FileStream stream = File.OpenWrite(path))
            {
                await dwnlod.Content.CopyToAsync(stream);
            }
        }

    }
}
