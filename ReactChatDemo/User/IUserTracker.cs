using System;
using System.Collections.Generic;
using ReactChatDemo.Models;

namespace ReactChatDemo.User
{
    public interface IUserTracker
    {
        IEnumerable<UserDetails> UsersOnline();
        void AddUser(string sid, string name);
        void RemoveUser(string sid);

        event Action<UserDetails> UserJoined;
        event Action<UserDetails> UserLeft;
    }
}
