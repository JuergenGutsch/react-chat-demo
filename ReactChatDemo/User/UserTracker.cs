using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactChatDemo.Models;

namespace ReactChatDemo.User
{
    public class UserTracker : IUserTracker
    {
        public event Action<UserDetails> UserJoined;
        public event Action<UserDetails> UserLeft;

        private ICollection<UserDetails> joinedUsers = new List<UserDetails>();

        public void AddUser(string sid, string name)
        {
            if (!joinedUsers.Any(x => x.Id == sid))
            {
                var user = new UserDetails
                {
                    Id = sid,
                    Name = name
                };
                joinedUsers.Add(user);
                UserJoined?.Invoke(user);
            }
        }

        public void RemoveUser(string sid)
        {
            var user = joinedUsers.FirstOrDefault(x => x.Id == sid);
            if (user != null)
            {
                joinedUsers.Remove(user);
                UserLeft?.Invoke(user);
            }
        }

        public IEnumerable<UserDetails> UsersOnline()
        {
            return joinedUsers;
        }
    }
}
