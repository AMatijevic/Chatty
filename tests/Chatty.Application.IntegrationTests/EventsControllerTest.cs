using Chatty.Application.Features.Event;
using Chatty.Application.IntegrationTests.Fixture;
using Chatty.WebApi;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Chatty.Application.IntegrationTests
{

    public sealed class EventsControllerTest
    {
        public class EventsControllerBaseTest : HttpClientFixture
        {
            public EventsControllerBaseTest(WebApiFactoryFixture<Startup> factory) : base(factory) { }

            protected Uri GetEvents_ByChatId => new Uri("api/Events/1", UriKind.Relative);
            protected Uri GetEvents_ByNotExistingChatId => new Uri("api/Events/7", UriKind.Relative);

            protected Uri GetAllEvents_AggregatedBy_1Minute_Url => new Uri("api/Events", UriKind.Relative);
            protected Uri GetAllEvents_AggregatedBy_30Minutes_Url => new Uri("api/Events?Aggregation type=1&Aggregation value=30", UriKind.Relative);
            protected Uri GetAllEvents_AggregatedBy_90Minutes_Url => new Uri("api/Events?Aggregation type=1&Aggregation value=90", UriKind.Relative);
            protected Uri GetAllEvents_AggregatedBy_1Day_Url => new Uri("api/Events?Aggregation type=3&Aggregation value=1", UriKind.Relative);
            protected Uri GetAllEvents_Invalid_Url => new Uri("api/Events?Aggregation type=0&Aggregation value=0", UriKind.Relative);

        }

        [Collection("Integration")]
        public class GetEventsByChatIdTest : EventsControllerBaseTest
        {
            public GetEventsByChatIdTest(WebApiFactoryFixture<Startup> factory) : base(factory) { }

            [Fact]
            public async Task Success()
            {
                var httpResponse = await Get<EventsVm>(GetEvents_ByChatId).ConfigureAwait(false);

                httpResponse.Should().NotBeNull();
                httpResponse.Events.Should().HaveCount(7);
                httpResponse.Events.First().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 9, 5, 00, 0));
                httpResponse.Events.First().Events.Should().HaveCount(1);
                httpResponse.Events.First().Events.First().Should().Contain("Enters the room");

                httpResponse.Events.Last().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 9, 5, 21, 0));
                httpResponse.Events.Last().Events.Should().HaveCount(1);
                httpResponse.Events.Last().Events.Last().Should().Contain("Leaves the room");
            }

            [Fact]
            public async Task ChatNotExisting_Success()
            {
                var httpResponse = await Get<EventsVm>(GetEvents_ByNotExistingChatId).ConfigureAwait(false);
                httpResponse.Should().NotBeNull();
                httpResponse.Events.Should().BeEmpty();
            }
        }

        [Collection("Integration")]
        public class GetAllEventsTest : EventsControllerBaseTest
        {
            public GetAllEventsTest(WebApiFactoryFixture<Startup> factory) : base(factory) { }

            [Fact]
            public async Task AggregatedBy1Min_Success()
            {
                var httpResponse = await Get<EventsVm>(GetAllEvents_AggregatedBy_1Minute_Url).ConfigureAwait(false);
                httpResponse.Should().NotBeNull();
                httpResponse.Events.Should().HaveCount(37);
                httpResponse.Events.First().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 9, 5, 00, 0));
                httpResponse.Events.First().Events.Should().HaveCount(1);
                httpResponse.Events.First().Events.First().Should().Contain("Enters the room");

                httpResponse.Events.Last().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 10, 4, 45, 0));
                httpResponse.Events.Last().Events.Should().HaveCount(1);
                httpResponse.Events.Last().Events.Last().Should().Contain("Leaves the room");
            }

            [Fact]
            public async Task AggregatedBy30Min_Success()
            {
                var httpResponse = await Get<EventsVm>(GetAllEvents_AggregatedBy_30Minutes_Url).ConfigureAwait(false);
                httpResponse.Should().NotBeNull();
                httpResponse.Events.Should().HaveCount(11);
                httpResponse.Events.First().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 9, 5, 00, 0));
                httpResponse.Events.First().Events.Should().HaveCount(7);
                httpResponse.Events.First().Events.First().Should().Contain("Enters the room");
                httpResponse.Events.First().Events.Last().Should().Contain("Leaves the room");

                httpResponse.Events.Last().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 10, 4, 30, 0));
                httpResponse.Events.Last().Events.Should().HaveCount(1);
                httpResponse.Events.Last().Events.Last().Should().Contain("Leaves the room");
            }

            [Fact]
            public async Task AggregatedBy90Min_Success()
            {
                var httpResponse = await Get<EventsVm>(GetAllEvents_AggregatedBy_90Minutes_Url).ConfigureAwait(false);
                httpResponse.Should().NotBeNull();
                httpResponse.Events.Should().HaveCount(7);
                httpResponse.Events.First().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 9, 4, 30, 0));
                httpResponse.Events.First().Events.Should().HaveCount(4);
                httpResponse.Events.First().Events.First().Should().Contain("2 people entered");
                httpResponse.Events.First().Events.Last().Should().Contain("2 comments");

                httpResponse.Events.Last().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 10, 4, 30, 0));
                httpResponse.Events.Last().Events.Should().HaveCount(1);
                httpResponse.Events.Last().Events.Last().Should().Contain("1 left");
            }

            [Fact]
            public async Task AggregatedBy1Day_Success()
            {
                var httpResponse = await Get<EventsVm>(GetAllEvents_AggregatedBy_1Day_Url).ConfigureAwait(false);
                httpResponse.Should().NotBeNull();
                httpResponse.Events.Should().HaveCount(2);
                httpResponse.Events.First().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 9, 0, 0, 0));
                httpResponse.Events.First().Events.Should().HaveCount(5);
                httpResponse.Events.First().Events.First().Should().Contain("5 people entered");
                httpResponse.Events.First().Events.Last().Should().Contain("8 comments");

                httpResponse.Events.Last().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 10, 0, 0, 0));
                httpResponse.Events.Last().Events.Should().HaveCount(5);
                httpResponse.Events.Last().Events.First().Should().Contain("3 people entered");
                httpResponse.Events.Last().Events.Last().Should().Contain("6 comments");
            }

            [Fact]
            public async Task InvalidUrlParams_Success()
            {
                var httpResponse = await Get<EventsVm>(GetAllEvents_Invalid_Url).ConfigureAwait(false);
                httpResponse.Should().NotBeNull();
                httpResponse.Events.Should().HaveCount(37);
                httpResponse.Events.First().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 9, 5, 00, 0));
                httpResponse.Events.First().Events.Should().HaveCount(1);
                httpResponse.Events.First().Events.First().Should().Contain("Enters the room");

                httpResponse.Events.Last().Occurrence.Should().BeSameDateAs(new DateTime(2020, 5, 10, 4, 45, 0));
                httpResponse.Events.Last().Events.Should().HaveCount(1);
                httpResponse.Events.Last().Events.Last().Should().Contain("Leaves the room");
            }
        }
    }
}
