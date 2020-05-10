using System;
using System.Collections.Generic;
using System.Linq;

namespace Chatty.Application.Features.Event
{
    public class EventsVm
    {
        public const int OneHour = 60; //minutes

        public EventsVm(IEnumerable<EventVm> events)
        {
            Events = events;
        }
        public IEnumerable<EventVm> Events { get; set; }

        public static EventsVm Create(Dictionary<DateTime, IEnumerable<EventDto>> items, int aggregationInterval)
        {
            var events = Enumerable.Empty<EventVm>();

            if (items?.Any() != true)
            {
                return new EventsVm(events);
            }

            if (aggregationInterval >= OneHour)
            {
                //Grouped ViewModel
                events = items.ToDictionary(
                        k => k.Key,
                        v => v.Value.GroupBy(t => t.Type).Select(b => new GroupedEventDto { Type = b.Key, NumberOfOccurrences = b.Count() }))
                    .Select(item => new EventVm(item.Key, item.Value.Select(e => e.ToString())));
            }
            else
            {
                events = items.Select(item => new EventVm(item.Key, item.Value.Select(e => e.ToString())));
            }

            return new EventsVm(events);

        }
    }
}
