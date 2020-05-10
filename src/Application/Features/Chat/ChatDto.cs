using Chatty.Application.Common.Mappings;
using Chatty.Domain.Enums;
using System;

namespace Chatty.Application.Features.Chat
{
    public class ChatDto : IMapFrom<Domain.Entities.Chat>
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public ChatType Type { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
