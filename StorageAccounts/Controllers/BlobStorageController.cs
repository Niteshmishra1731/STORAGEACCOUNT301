using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;




namespace StorageAccounts.Controllers;


    [ApiController]
    [Route("[controller]")]
    public class BlobStorageController : ControllerBase
    {
        [HttpPost("AddBlob")]
        public async Task<string> AddBlob(string blobName)
        {
            await Repsitory.BlobStorage.CreateBlob(blobName);
            return null;
        }
        [HttpDelete("DeleteBlob")]
        public async Task<string> DeleteBlob(string blobName)
        {
            await Repsitory.BlobStorage.DeleteBlob(blobName);
            return null;
        }
        [HttpDelete("DelteBlobContent")]
        public async Task<string> DeleteBlobContent(string blobName, string file)
        {
            await Repsitory.BlobStorage.DeleteBlobContent(blobName, file);
            return null;
        }
        [HttpPut("UpdateBlobContent")]
        public async Task<BlobProperties> UpdateBlobContent(string blobName, string file)
        {
            await Repsitory.BlobStorage.UpdateBlobContent(blobName, file);
            return null;

        }
        [HttpGet("GetBlobContent")]
        public async Task<BlobProperties> GetBlobContent(string blobName, string file)
        {
            var data = await Repsitory.BlobStorage.getBlobContent(blobName, file);
            return data;
        }
        [HttpGet("GetBlob")]
        public async Task<List<string>> GetBlob(string blobName, string file)
        {
            var data = await Repsitory.BlobStorage.GetBlob(blobName, file);
            return data;
        }
        [HttpGet("DownloadBlobContent")]
        public async Task<BlobProperties> DownloadBlob(string blobName, string file)
        {
            var data = await Repsitory.BlobStorage.DownloadBlob(blobName, file);
            return data;
        }



    }


