using Microsoft.AspNetCore.SignalR;
using ReactChatDemo.Models;
using ReactChatDemo.Services;
using ReactChatDemo.User;
using System.Threading.Tasks;

namespace ReactChatDemo.Hubs
{
    public class ChatHub : HubWithPresence
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService, IUserTracker userTracker)
            : base(userTracker)
        {
            _chatService = chatService;
        }

        public async Task AddMessage(string message)
        {
            var username = Context.User.Identity.Name;
            var chatMessage = await _chatService.CreateNewMessage(username, message);
            // Call the MessageAdded method to update clients.
            await Clients.All.SendAsync("MessageAdded", chatMessage);
        }

        public override async void OnUsersJoined(UserDetails user)
        {
            await Clients.All.SendAsync("UsersJoined", user);
        }

        public override async void OnUsersLeft(UserDetails user)
        {
            await Clients.All.SendAsync("UsersLeft", user);
        }
    }
}
