using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using StorageAccounts.DTO_s;
using StorageAccounts.Repsitory;

namespace StorageAccounts.Controllers;
[ApiController]
[Route("[controller]")]

public class TableStorageController : ControllerBase
{
    [HttpPost("AddTable")]
    public async Task<string> AddTable(string tableName)
    {
        await StorageAccounts.Repsitory.TableStorage.AddTable(tableName);
        return null;
    }
    [HttpPut("UpdateTable")]
    public async Task<Details> UpdateTable(Details employee, string tableName)
    {
        var data = await Repsitory.TableStorage.UpdateTable(employee, tableName);
        return data;
    }
    [HttpGet("GetTableData")]
    public async Task<Details> GetTableData(string tableName, string partitionKey, string rowKey)
    {
        var data = await TableStorage.GetTableData(tableName, partitionKey, rowKey);
        return data;
    }
    [HttpGet("GetTable")]
    public async Task<TableClient> GetTable(string tableName)
    {
        var data = await TableStorage.GetTable(tableName);
        return data;
    }
    [HttpDelete("DeleteTableData")]
    public async Task DeleteTableData(string tableName, string partitionKey, string rowKey)
    {
        await Repsitory.TableStorage.DeleteTableData(tableName, partitionKey, rowKey);
        return;
    }
    [HttpDelete("DeleteTable")]
    public async Task DeleteTable(string tableName)
    {
        await TableStorage.DeleteTable(tableName);
    }
}


