using Chatty.Domain.Enums;
using System;

namespace Chatty.Application.Features.Event
{
    public class GroupedEventDto
    {
        public EventType Type { get; set; }
        public int NumberOfOccurrences { get; set; }
        public int? NumberOfPersons { get; set; }

        public override string ToString()
        {
            var text = Type switch
            {
                EventType.Comment => "comments",
                EventType.EnterTheRoom => "people entered",
                EventType.LeveTheRoom => "left",
                EventType.HighFiveAnotherUser => "person high-fives",
                _ => throw new NotImplementedException()
            };

            return NumberOfPersons.HasValue ?
                $"1 {text} {NumberOfPersons} other people"
                : $"{NumberOfOccurrences} {text}";
        }
    }
}
