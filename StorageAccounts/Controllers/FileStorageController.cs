using Microsoft.AspNetCore.Mvc;
using StorageAccounts.Repsitory;

namespace StorageAccounts.Controllers;

public class FileStorageController : Controller
{
    [HttpPost("CreateFile")]
    public async Task CreateFile(string fileName)
    {
        await FileStorage.CreateFile(fileName);
    }
    [HttpPost("CreateDirectory")]
    public async Task CreateDirectory(string directoryName,string fileName)
    {
        await FileStorage.CreateDirectory(directoryName, fileName);
    }
    [HttpPut("UploadFile")]
    public async Task UploadFile(IFormFile file,string directoryName,string fileShareName)
    {
        await FileStorage.CreateDirectory(directoryName, fileShareName);
    }
    [HttpDelete("DeleteDirectory")]
    public async Task DeleteDirectory(string directoryName,string fileShareName)
    {
        await FileStorage.DeleteDirectory(directoryName, fileShareName);   
    }
    [HttpDelete("DeleteFile")]
    public async Task DeleteFile(string directoryName,string fileShareName,string fileName)
    {
        await FileStorage.DeleteFile(directoryName, fileShareName, fileName);
    }
    [HttpGet("GetAllFiles")]
    public async Task<List<string>>GetAllFiles(string directoryName,string fileShareName)
    {
        var data=await FileStorage.GetAllFiles(directoryName,fileShareName);
        return data;
    }
    [HttpGet("DownloadFile")]
    public async Task DownloadFile(string directoryName,string fileShareName,string fileName)
    {
        await FileStorage.DownloadFile(directoryName,fileShareName,fileName);
    }
}
