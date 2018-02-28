using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ReactChatDemo.Models;

namespace ReactChatDemo.User
{
    public interface IUserTracker
    {
        Task<ICollection<UserDetails>> UsersOnline();
        void AddUser(HubConnectionContext connection, UserDetails userDetails);
        void RemoveUser(HubConnectionContext connection);

        event Action<UserDetails> UserJoined;
        event Action<UserDetails> UserLeft;
    }
}
