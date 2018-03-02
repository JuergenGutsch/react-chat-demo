using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using ReactChatDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactChatDemo.Services
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly string _tableName;
        private readonly CloudTableClient _tableClient;
        private readonly IConfiguration _configuration;

        public ChatMessageRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            var accountName = configuration.GetValue<string>("accountName");
            var accountKey = configuration.GetValue<string>("accountKey");
            _tableName = _configuration.GetValue<string>("tableName");

            var storageCredentials = new StorageCredentials(accountName, accountKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            _tableClient = storageAccount.CreateCloudTableClient();
        }

        public async Task<IEnumerable<ChatMessage>> GetTopMessages(int number = 100)
        {
            var table = _tableClient.GetTableReference(_tableName);

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            string filter = TableQuery.GenerateFilterCondition(
                "PartitionKey",
                QueryComparisons.Equal,
                "chatmessages");

            var query = new TableQuery<ChatMessageTableEntity>()
                .Where(filter)
                .Take(number);

            var entities = await table.ExecuteQuerySegmentedAsync(query, null);

            var result = entities.Results.Select(entity =>
                new ChatMessage
                {
                    Id = entity.RowKey,
                    Date = entity.Timestamp,
                    Message = entity.Message,
                    Sender = entity.Sender
                });

            return result;
        }

        public async Task AddMessage(ChatMessage message)
        {
            var table = _tableClient.GetTableReference(_tableName);

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            var chatMessage = new ChatMessageTableEntity(message.Id)
            {
                Message = message.Message,
                Sender = message.Sender
            };

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(chatMessage);

            // Execute the insert operation.
            var result = await table.ExecuteAsync(insertOperation);
        }

    }
}
