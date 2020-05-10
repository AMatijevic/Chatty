using System;
using System.Collections.Generic;
using System.Linq;

namespace Chatty.Application.Features.Event
{
    public class EventVm
    {
        public EventVm(DateTime occurrence, IEnumerable<string> events)
            => (Occurrence, Events) = (occurrence, events);

        public DateTime Occurrence { get; set; }
        public IEnumerable<string> Events { get; set; } = Enumerable.Empty<string>();
    }
}
