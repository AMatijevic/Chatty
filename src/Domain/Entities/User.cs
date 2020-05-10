using Chatty.Domain.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Chatty.Domain.Entities
{
    public class User : BaseEntity
    {
        private User() { }

        public User(string nickName)
        {
            Nickname = nickName;
            Username = nickName;
            Password = Guid.NewGuid().ToString();
        }

        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Nickname { get; protected set; }
        //...

        public IEnumerable<Event> Events => new ReadOnlyCollection<Event>(_events);
        private readonly List<Event> _events = new List<Event>();
    }
}
