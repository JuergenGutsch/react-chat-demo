using ReactChatDemo.Models;
using System;
using System.Collections.Generic;

namespace ReactChatDemo.Services
{
    public class ChatService : IChatService
    {
        private IDictionary<int, ChatMessage> messages;

        public ChatService()
        {
            var date = DateTime.Now;
            messages = new Dictionary<int, ChatMessage>{
                { 1, new ChatMessage{ Id = 1, Date = date.AddMinutes(-8).AddSeconds(-8), Message = "Hey", Sender = "Mo" } },
                { 2, new ChatMessage{ Id = 2, Date = date.AddMinutes(-7).AddSeconds(-7), Message = "Hallo Mo!", Sender = "Peter" } },
                { 3, new ChatMessage { Id = 3, Date = date.AddMinutes(-6).AddSeconds(-6), Message = "Salli Mo :-)", Sender = "Juergen" } },
                { 4, new ChatMessage{ Id = 4, Date = date.AddMinutes(-5).AddSeconds(-5), Message = "Hey Peter und Juergen, wie läufts", Sender = "Mo" } },
                { 5, new ChatMessage{ Id = 5, Date = date.AddMinutes(-4).AddSeconds(-4), Message = "gut, danke", Sender = "Juergen" } },
                { 6, new ChatMessage{ Id = 6, Date = date.AddMinutes(-3).AddSeconds(-3), Message = "naja... könnte besser sein :-/", Sender = "Peter" } },
                { 7, new ChatMessage{ Id = 7, Date = date.AddMinutes(-2).AddSeconds(-2), Message = "was ist den los, peter?", Sender = "Marion" } },
                { 8, new ChatMessage{ Id = 8, Date = date.AddMinutes(-1).AddSeconds(-1), Message = "nichts. das is es ja eben... ;-)", Sender = "Peter" } }
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
