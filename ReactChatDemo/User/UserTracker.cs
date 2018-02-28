using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ReactChatDemo.Models;

namespace ReactChatDemo.User
{
    public class UserTracker : IUserTracker
    {
        public event Action<UserDetails> UserJoined;
        public event Action<UserDetails> UserLeft;

        private ICollection<UserDetails> joinedUsers = new List<UserDetails>();

        public void AddUser(HubConnectionContext connection, UserDetails userDetails)
        {
            userDetails.ConnectionId = connection.ConnectionId;

            if (!joinedUsers.Any(x => x.Id == userDetails.Id))
            {
                joinedUsers.Add(userDetails);
                UserJoined?.Invoke(userDetails);
            }
        }

        public void RemoveUser(HubConnectionContext connection)
        {
            var connectionId = connection.ConnectionId;

            var user = joinedUsers.FirstOrDefault(x => x.ConnectionId == connectionId);
            if (user != null)
            {
                joinedUsers.Remove(user);
                UserLeft?.Invoke(user);
            }
        }

        public Task<ICollection<UserDetails>> UsersOnline()
        {
            return Task.FromResult(joinedUsers);
        }
    }
}
