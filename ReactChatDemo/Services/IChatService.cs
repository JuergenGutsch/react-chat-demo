using ReactChatDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactChatDemo.Services
{
    public interface IChatService
    {
        Task<IEnumerable<ChatMessage>> GetAllInitially();
        Task<ChatMessage> CreateNewMessage(string senderName, string message);
    }
}