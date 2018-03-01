using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ReactChatDemo.Models;
using ReactChatDemo.User;

namespace ReactChatDemo.Hubs
{
    public abstract class HubWithPresence : Hub
    {
        private IUserTracker _userTracker;

        public HubWithPresence(IUserTracker userTracker)
        {
            _userTracker = userTracker;
            _userTracker.UserJoined += OnUsersJoined;
            _userTracker.UserLeft += OnUsersLeft;
        }

        public virtual async void OnUsersJoined(UserDetails user) { }

        public virtual async void OnUsersLeft(UserDetails user) { }
    }
}
