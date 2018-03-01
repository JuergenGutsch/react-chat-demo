using ReactChatDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactChatDemo.Services
{

    public class ChatService : IChatService
    {
        private readonly IChatMessageRepository _repository;

        public ChatService(IChatMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ChatMessage> CreateNewMessage(string senderName, string message)
        {
            var chatMessage = new ChatMessage(Guid.NewGuid())
            {
                Sender = senderName,
                Message = message
            };
            await _repository.AddMessage(chatMessage);

            return chatMessage;
        }

        public async Task<IEnumerable<ChatMessage>> GetAllInitially()
        {
            return await _repository.GetTopMessages();
        }
    }
}
