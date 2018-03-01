using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ReactChatDemo.Services
{
    public class ChatMessageTableEntity : TableEntity
    {
        public ChatMessageTableEntity(string key)
        {
            PartitionKey = "chatmessages";
            RowKey = key;
        }

        public ChatMessageTableEntity() { }

        public string Message { get; set; }

        public string Sender { get; set; }
    }
}
