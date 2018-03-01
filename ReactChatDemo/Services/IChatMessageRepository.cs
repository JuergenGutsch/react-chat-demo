using ReactChatDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactChatDemo.Services
{
    public interface IChatMessageRepository
    {
        Task AddMessage(ChatMessage message);
        Task<IEnumerable<ChatMessage>> GetTopMessages(int number = 100);
    }
}
