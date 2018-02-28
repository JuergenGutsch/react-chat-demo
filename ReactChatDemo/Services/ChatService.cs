using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using ReactChatDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactChatDemo.Services
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private const string accountName = "reactchatdemo";
        private const string accountKey = "hxBCWoyJaOdI0QbtaL2neMCQo+2QH9RjtjAiSlWh/2DLlRZOhnurDG8KFmaCHW/SJWkQGXPhmQkecPp+RP2rgA==";
        private const string tableName = "reactchatmessages";

        private readonly CloudTableClient _tableClient;

        public ChatMessageRepository()
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
            _tableClient = storageAccount.CreateCloudTableClient();

        }

        // https://docs.microsoft.com/en-us/azure/cosmos-db/table-storage-how-to-use-dotnet
        public async Task<List<ChatMessageTableEntity>> GetTopMessages(int number = 100)
        {
            var table = _tableClient.GetTableReference(tableName);

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            var query = new TableQuery<ChatMessageTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "chatmessages"))
                .Take(number);

            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            return result.Results;
        }

        // https://docs.microsoft.com/en-us/azure/cosmos-db/table-storage-how-to-use-dotnet
        public async Task<ChatMessageTableEntity> AddMessage(string message, string sender)
        {
            var table = _tableClient.GetTableReference(tableName);

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            var chatMessage = new ChatMessageTableEntity(Guid.NewGuid())
            {
                Message = message,
                Sender = sender
            };

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(chatMessage);

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);

            return chatMessage;
        }

    }

    public class ChatMessageTableEntity : TableEntity
    {
        public ChatMessageTableEntity(Guid key)
        {
            PartitionKey = "chatmessages";
            RowKey = key.ToString("X");
        }

        public ChatMessageTableEntity() { }

        public string Message { get; set; }

        public string Sender { get; set; }
    }

    public interface IChatMessageRepository
    {
    }

    public class ChatService : IChatService
    {
        private IDictionary<int, ChatMessage> messages;

        public ChatService()
        {
            var date = DateTime.Now;
            messages = new Dictionary<int, ChatMessage>{
                { 1, new ChatMessage{ Id = 1, Date = date.AddMinutes(-8).AddSeconds(-8), Message = "Hey", Sender = "Mo" } },
                { 2, new ChatMessage{ Id = 2, Date = date.AddMinutes(-7).AddSeconds(-7), Message = "Hello Mo!", Sender = "Pete" } },
                { 3, new ChatMessage{ Id = 3, Date = date.AddMinutes(-6).AddSeconds(-6), Message = "Hi Mo :-)", Sender = "Joe" } },
                { 4, new ChatMessage{ Id = 4, Date = date.AddMinutes(-5).AddSeconds(-5), Message = "Hey Pete and Joe, how are you?", Sender = "Mo" } },
                { 5, new ChatMessage{ Id = 5, Date = date.AddMinutes(-4).AddSeconds(-4), Message = "fine, thanks", Sender = "Joe" } },
                { 6, new ChatMessage{ Id = 6, Date = date.AddMinutes(-3).AddSeconds(-3), Message = "well... could be better :-/", Sender = "Pete" } },
                { 7, new ChatMessage{ Id = 7, Date = date.AddMinutes(-2).AddSeconds(-2), Message = "what's up, pete?", Sender = "Mary" } },
                { 8, new ChatMessage{ Id = 8, Date = date.AddMinutes(-1).AddSeconds(-1), Message = "nothing special. that's the reason... ;-)", Sender = "Pete" } }
            };
        }

        public ChatMessage CreateNewMessage(string senderName, string message)
        {
            var newId = messages.Count + 1;
            var chatMessage = new ChatMessage
            {
                Id = newId,
                Date = DateTime.Now,
                Sender = senderName,
                Message = message
            };
            messages.Add(newId, chatMessage);
            return chatMessage;
        }

        public ICollection<ChatMessage> GetAllInitially()
        {
            return messages.Values;
        }
    }
}
