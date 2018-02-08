using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ReactChatDemo.Models;
using ReactChatDemo.User;

namespace ReactChatDemo.Hubs
{
    public class HubWithPresence : Hub
    {
        private IUserTracker _userTracker;

        public HubWithPresence(IUserTracker userTracker)
        {
            _userTracker = userTracker;
            _userTracker.UserJoined += OnUsersJoined;
            _userTracker.UserLeft += OnUsersLeft;

        }

        public async Task<IEnumerable<UserDetails>> GetUsersOnline()
        {
            return await _userTracker.UsersOnline();
        }

        public virtual void OnUsersJoined(UserDetails user)
        {
        }

        public virtual void OnUsersLeft(UserDetails user)
        {
        }

    }
}
