using Microsoft.AspNetCore.SignalR;
using ReactChatDemo.Services;
using System.Threading.Tasks;

namespace ReactChatDemo.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task AddMessage(string message)
        {
            var username = Context.User.Identity.Name;
            var chatMessage = await _chatService.CreateNewMessage(username, message);
            // Call the MessageAdded method to update clients.
            await Clients.All.InvokeAsync("MessageAdded", chatMessage);
        }
    }
}
