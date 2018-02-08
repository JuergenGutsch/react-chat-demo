using ReactChatDemo.Models;
using System.Collections.Generic;

namespace ReactChatDemo.Services
{
    public interface IChatService
    {
        ICollection<ChatMessage> GetAllInitially();
        ChatMessage CreateNewMessage(string senderName, string message);
    }
}