using Chatty.Application.Features.Event;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Chatty.Application.Features.Event.GetAllEvents;
using static Chatty.Application.Features.Event.GetEventsByChatId;

namespace Chatty.WebApi.Controllers
{
    public class EventsController : ApiController
    {
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<EventsVm>> Get(
            [FromQuery(Name = "Aggregation type")] Domain.Enums.AggregationType aggregationType = Domain.Enums.AggregationType.Minute,
            [FromQuery(Name = "Aggregation value")] int aggregationValue = 1)
        {
            return await Mediator.Send(new GetAllEventsQuery
            {
                AggregationType = aggregationType,
                AggregationValue = aggregationValue == 0 ? 1 : aggregationValue
            }).ConfigureAwait(false);
        }

        [HttpGet("{chatId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<EventsVm>> Get(
            int chatId,
            [FromQuery(Name = "Aggregation type")] Domain.Enums.AggregationType aggregationType = Domain.Enums.AggregationType.Minute,
            [FromQuery(Name = "Aggregation value")] int aggregationValue = 1)
        {
            return await Mediator.Send(new GetEventsByChatIdQuery
            {
                ChatId = chatId,
                AggregationType = aggregationType,
                AggregationValue = aggregationValue == 0 ? 1 : aggregationValue
            }).ConfigureAwait(false);
        }
    }
}
