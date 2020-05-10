using Chatty.Domain.Common;
using Chatty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Chatty.Domain.Entities
{
    public class Chat : BaseEntity
    {
        private Chat() { }

        public Chat(string subject, ChatType type = ChatType.Message)
        {
            Subject = subject;
            Type = type;
        }

        public string Subject { get; protected set; }
        public ChatType Type { get; protected set; }
        public DateTime? Start { get; protected set; }
        public DateTime? End { get; protected set; }

        public IEnumerable<User> Participants => new ReadOnlyCollection<User>(_participants);
        private readonly List<User> _participants = new List<User>();

        public IEnumerable<Event> Events => new ReadOnlyCollection<Event>(_events);
        private readonly List<Event> _events = new List<Event>();


    }
}
