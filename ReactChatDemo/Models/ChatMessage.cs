using System;

namespace ReactChatDemo.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
    }
}
