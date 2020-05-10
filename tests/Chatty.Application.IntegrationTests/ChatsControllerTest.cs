using Chatty.Application.Features.Chat;
using Chatty.Application.IntegrationTests.Fixture;
using Chatty.WebApi;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Chatty.Application.IntegrationTests
{

    public sealed class ChatsControllerTest
    {
        public class ChatsControllerBaseTest : HttpClientFixture
        {
            public ChatsControllerBaseTest(WebApiFactoryFixture<Startup> factory) : base(factory) { }

            protected Uri GetAllChatsUrl => new Uri("api/Chats", UriKind.Relative);
        }

        [Collection("Integration")]
        public class GetAllChatsTest : ChatsControllerBaseTest
        {
            public GetAllChatsTest(WebApiFactoryFixture<Startup> factory) : base(factory) { }

            [Fact]
            public async Task Success()
            {
                var httpResponse = await Get<IEnumerable<ChatDto>>(GetAllChatsUrl).ConfigureAwait(false);
                httpResponse.Should().HaveCount(3);
                httpResponse.First().Should().NotBeNull();
                httpResponse.First().Subject.Should().Be("StandUp");
                httpResponse.Last().Should().NotBeNull();
                httpResponse.Last().Subject.Should().Be("Retro");
            }
        }
    }
}
