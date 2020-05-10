using Chatty.Domain.Entities;
using Chatty.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Chatty.Infrastructure.UnitTests.Services
{
    public class AggregatorServiceTest
    {
        AggregatorService _aggregatrService;

        public AggregatorServiceTest()
        {
            _aggregatrService = new AggregatorService();
        }

        [Fact]
        public void GetAggregatedEventsBy1Minute_ResultTwoGroupsWithOneItem()
        {
            var list = new List<Event>()
            {
                new Event(new DateTime(2020, 5, 9, 14,00,10), Domain.Enums.EventType.EnterTheRoom, string.Empty),
                new Event(new DateTime(2020, 5, 9, 14,01,30), Domain.Enums.EventType.EnterTheRoom, string.Empty),
            };

            var result = _aggregatrService.Aggregate<Event>(list);

            Assert.Equal(2, result.Count());
            Assert.Single(result.Values.FirstOrDefault());
            Assert.Single(result.Values.LastOrDefault());
        }

        [Fact]
        public void GetAggregatedEventsBy2Minute_ResultOneGroupWithTwoItems()
        {
            var list = new List<Event>()
            {
                new Event(new DateTime(2020, 5, 9, 14,00,10), Domain.Enums.EventType.EnterTheRoom, string.Empty),
                new Event(new DateTime(2020, 5, 9, 14,01,30), Domain.Enums.EventType.EnterTheRoom, string.Empty),
            };

            var result = _aggregatrService.Aggregate<Event>(list, 2);

            Assert.Single(result);
            Assert.Equal(2, result.Values.FirstOrDefault().Count());
        }
    }
}
