using Chatty.Application.Features.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Chatty.Application.UnitTests.Event
{
    public class EventsVmCreationTest
    {
        [Fact]
        public void GetNotGroupedEventsVmSuccessfully()
        {
            var data = new Dictionary<DateTime, IEnumerable<EventDto>>()
            {
                { new DateTime(2020, 05, 10, 12, 0, 0),
                    new List<EventDto>()
                    {
                        new EventDto { UserNickname = "Bob", Occurrence = new DateTime(2020, 05, 10, 12, 0, 0), Type = Domain.Enums.EventType.EnterTheRoom },
                        new EventDto { UserNickname = "Alice", Occurrence = new DateTime(2020, 05, 10, 12, 04, 0), Type = Domain.Enums.EventType.EnterTheRoom },
                        new EventDto { UserNickname = "Kira", Occurrence = new DateTime(2020, 05, 10, 12, 09, 0), Type = Domain.Enums.EventType.EnterTheRoom },
                    }
                },
                { new DateTime(2020, 05, 10, 12, 10, 0),
                    new List<EventDto>()
                    {
                        new EventDto { UserNickname = "Bob", Occurrence = new DateTime(2020, 05, 10, 12, 12, 0), Type = Domain.Enums.EventType.Comment, Content = "Hello" },
                        new EventDto { UserNickname = "Alice", Occurrence = new DateTime(2020, 05, 10, 12, 14, 0), Type = Domain.Enums.EventType.HighFiveAnotherUser },
                        new EventDto { UserNickname = "Kira", Occurrence = new DateTime(2020, 05, 10, 12, 19, 0), Type = Domain.Enums.EventType.Comment, Content = "Hello Bob" },
                    }
                },
            };

            var eventsVm = EventsVm.Create(data, 10);

            Assert.Equal(2, eventsVm.Events.Count());

            var first = eventsVm.Events.FirstOrDefault();
            Assert.NotNull(first);
            Assert.Equal(new DateTime(2020, 05, 10, 12, 0, 0), first.Occurrence);
            Assert.Equal(3, first.Events.Count());

            var last = eventsVm.Events.LastOrDefault();
            Assert.NotNull(last);
            Assert.Equal(new DateTime(2020, 05, 10, 12, 10, 0), last.Occurrence);
            Assert.Equal(3, last.Events.Count());
        }

        [Fact]
        public void GetGroupedEventsVmSuccessfully()
        {
            var data = new Dictionary<DateTime, IEnumerable<EventDto>>()
            {
                { new DateTime(2020, 05, 10, 12, 0, 0),
                    new List<EventDto>()
                    {
                        new EventDto { UserNickname = "Bob", Occurrence = new DateTime(2020, 05, 10, 12, 0, 0), Type = Domain.Enums.EventType.EnterTheRoom },
                        new EventDto { UserNickname = "Alice", Occurrence = new DateTime(2020, 05, 10, 12, 04, 0), Type = Domain.Enums.EventType.EnterTheRoom },
                        new EventDto { UserNickname = "Kira", Occurrence = new DateTime(2020, 05, 10, 12, 09, 0), Type = Domain.Enums.EventType.EnterTheRoom },
                    }
                },
                { new DateTime(2020, 05, 10, 13, 0, 0),
                    new List<EventDto>()
                    {
                        new EventDto { UserNickname = "Bob", Occurrence = new DateTime(2020, 05, 10, 13, 12, 0), Type = Domain.Enums.EventType.Comment, Content = "Hello" },
                        new EventDto { UserNickname = "Alice", Occurrence = new DateTime(2020, 05, 10, 13, 14, 0), Type = Domain.Enums.EventType.HighFiveAnotherUser },
                        new EventDto { UserNickname = "Kira", Occurrence = new DateTime(2020, 05, 10, 13, 19, 0), Type = Domain.Enums.EventType.Comment, Content = "Hello Bob" },
                    }
                },
            };

            var eventsVm = EventsVm.Create(data, 60);

            Assert.Equal(2, eventsVm.Events.Count());

            var first = eventsVm.Events.FirstOrDefault();
            Assert.NotNull(first);
            Assert.Equal(new DateTime(2020, 05, 10, 12, 0, 0), first.Occurrence);
            Assert.Single(first.Events);

            var last = eventsVm.Events.LastOrDefault();
            Assert.NotNull(last);
            Assert.Equal(new DateTime(2020, 05, 10, 13, 0, 0), last.Occurrence);
            Assert.Equal(2, last.Events.Count());
        }
    }
}
