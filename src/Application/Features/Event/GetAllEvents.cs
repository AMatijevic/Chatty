using AutoMapper;
using Chatty.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chatty.Application.Features.Event
{
    public sealed class GetAllEvents
    {
        public class GetAllEventsQuery : Aggregatable, IRequest<EventsVm> { }

        public class GetEventsByChatIdQueryHandler : IRequestHandler<GetAllEventsQuery, EventsVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAggregatorService _aggregatorService;

            public GetEventsByChatIdQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IAggregatorService aggregatorService)
            {
                _context = context;
                _mapper = mapper;
                _aggregatorService = aggregatorService;
            }

            public async Task<EventsVm> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
            {
                var events = await _context.Events
                    .AsNoTracking()
                    .Include(c => c.User)
                    .ToListAsync()
                    .ConfigureAwait(false);

                var aggregationInterval = request.GetAggregationValueInMinutes();

                var items = _aggregatorService.Aggregate<Domain.Entities.Event>(events, aggregationInterval)
                    .ToDictionary(k => k.Key, v => v.Value.Select(i => _mapper.Map<EventDto>(i)));

                return EventsVm.Create(items, aggregationInterval);
            }
        }
    }
}
