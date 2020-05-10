using Chatty.WebApi;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Chatty.Application.IntegrationTests.Fixture
{
    public class HttpClientFixture : IClassFixture<WebApiFactoryFixture<Startup>>
    {
        private readonly HttpClient _client;

        public HttpClientFixture(WebApiFactoryFixture<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        public async Task<TResponse> Get<TResponse>(Uri address)
        {
            var httpResponse = await _client.GetAsync(address).ConfigureAwait(false);
            var jsonContent = await httpResponse.Content?.ReadAsStringAsync() ?? string.Empty;
            return JsonConvert.DeserializeObject<TResponse>(jsonContent);
        }
    }
}
