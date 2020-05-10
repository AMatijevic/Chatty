using AutoMapper;
using Chatty.Application.Common.Mappings;
using Chatty.Domain.Enums;
using System;

namespace Chatty.Application.Features.Event
{
    public class EventDto : IMapFrom<Domain.Entities.Event>
    {
        public string ChatId { get; set; }
        public string UserNickname { get; set; }
        public DateTime Occurrence { get; set; }
        public EventType Type { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Event, EventDto>()
                .ForMember(d => d.UserNickname,
                opt => opt.MapFrom(s => s.User.Nickname ?? string.Empty));
        }

        public override string ToString()
        {
            var text = Type switch
            {
                EventType.Comment => $"comments: {Content}",
                EventType.EnterTheRoom => "Enters the room",
                EventType.LeveTheRoom => "Leaves the room",
                EventType.HighFiveAnotherUser => "High fives",
                _ => throw new NotImplementedException()
            };
            return $"{Occurrence:t}: {UserNickname} {text}";
        }
    }
}
