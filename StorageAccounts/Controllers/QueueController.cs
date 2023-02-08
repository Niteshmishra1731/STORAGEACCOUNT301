using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Queues.Models;


namespace StorageAccounts.Controllers;

[ApiController]
[Route("[controller]")]

public class QueueController : ControllerBase
{
    [HttpPost]
    [Route("AddQueue")]
    public async Task<string> AddQueue(string queueName)
    {
        await StorageAccounts.Repsitory.Queue.CreateQueue(queueName);
        return null;
    }
    [HttpPut("InsertMessage")]
    public async Task<string> InsertMessage(string queueName, string msg)
    {
        await Repsitory.Queue.InsertMessage(queueName, msg);
        return null;
    }
    [HttpPut("PeekMessage")]
    public async Task<PeekedMessage[]> PeekMessage(string queueName)
    {
        var data = await Repsitory.Queue.PeekMessage(queueName);
        return data;
    }
    [HttpPut("UpdateMessage")]
    public async Task<string> UpdateMessage(string queueName, string msg)
    {
        await Repsitory.Queue.UpdateMessage(queueName, msg);
        return null;
    }
    [HttpDelete("DequeueMessage")]
    public async Task<string> DequeueMessage(string queueName)
    {
        await Repsitory.Queue.DequeueMessage(queueName);
        return null;
    }
    [HttpDelete("DeleteQueue")]
    public async Task<string> DeleteQueue(string queueName)
    {
        await Repsitory.Queue.DeleteQueue(queueName);
        return null;
    }

}
