using Chatty.Domain.Common.Interfaces;
using Chatty.Domain.Enums;
using System;

namespace Chatty.Domain.Entities
{
    public class Event : IAggregatable
    {
        private Event() { }

        public Event(DateTime occurrence, EventType type, string content)
        {
            Occurrence = occurrence;
            Type = type;
            Content = content;
        }

        public Event(DateTime occurrence, EventType type, string content, int chatId, int userId)
        {
            Occurrence = occurrence;
            Type = type;
            Content = content;
            ChatId = chatId;
            UserId = userId;
        }

        public int Id { get; protected set; }
        public int ChatId { get; protected set; }
        public Chat Chat { get; protected set; }
        public int UserId { get; protected set; }
        public User User { get; protected set; }
        public DateTime Occurrence { get; protected set; }
        public EventType Type { get; protected set; }
        public string Content { get; protected set; }
    }
}
