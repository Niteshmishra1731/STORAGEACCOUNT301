using StorageAccounts.DTO_s;
using Azure.Data.Tables;

namespace StorageAccounts.Repsitory
{
    public class TableStorage
    {
        static string connectionstring = "DefaultEndpointsProtocol=https;AccountName=niteshm003;AccountKey=eDXUiQHVgG+2k+OKWQPJ9GHQ9NdIzelQAHAkP7R2Xq5yMPQ0ot52crqfX0E+KumlVXip2Pso+6v3+ASt85Sm0g==;EndpointSuffix=core.windows.net";
        public static async Task AddTable(string tableName)
        {
            var data = new TableServiceClient(connectionstring);
            var client = data.GetTableClient(tableName);
            await client.CreateIfNotExistsAsync();

        }
        public static async Task<Details> UpdateTable(Details employee,string tableName)
        {
            var data = new TableServiceClient(connectionstring);
            var client = data.GetTableClient(tableName);
            await client.UpsertEntityAsync(employee);
            return null;
        }
        public static async Task<Details> GetTableData(string tableName,string partitionKey,string rowKey)
        {
            var data = new TableServiceClient(connectionstring);
            var client = data.GetTableClient(tableName);
            var tableData = await client.GetEntityAsync<Details>(partitionKey, rowKey);
            return tableData;
        }
        public static async Task<TableClient> GetTable(string tableName)
        {
            var data = new TableServiceClient(connectionstring);
            var client = data.GetTableClient(tableName);
            return client;
        }
        public static async Task DeleteTableData(string tableName,string partitionKey,string rowKey)
        {
            var data = new TableServiceClient(connectionstring);
            var client=data.GetTableClient(tableName);
            await client.DeleteEntityAsync(partitionKey, rowKey);
            return; 

        }
        public static async Task DeleteTable(string tableName)
        {
            var data = new TableServiceClient(connectionstring);
            await data.DeleteTableAsync(tableName);

        }

    }
}
