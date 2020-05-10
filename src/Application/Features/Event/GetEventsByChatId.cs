using AutoMapper;
using Chatty.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chatty.Application.Features.Event
{
    public sealed class GetEventsByChatId
    {
        public class GetEventsByChatIdQuery : Aggregatable, IRequest<EventsVm>
        {
            public int ChatId { get; set; }
        }

        public class GetEventsByChatIdQueryHandler : IRequestHandler<GetEventsByChatIdQuery, EventsVm>
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

            public async Task<EventsVm> Handle(GetEventsByChatIdQuery request, CancellationToken cancellationToken)
            {
                var chat = await _context.Chats
                    .AsNoTracking()
                    .Include(c => c.Events).ThenInclude(e => e.User)
                    .FirstOrDefaultAsync(c => c.Id == request.ChatId);

                var aggregationInterval = request.GetAggregationValueInMinutes();

                var items = _aggregatorService.Aggregate<Domain.Entities.Event>(chat?.Events, aggregationInterval)
                    .ToDictionary(k => k.Key, v => v.Value.Select(i => _mapper.Map<EventDto>(i)));

                return EventsVm.Create(items, aggregationInterval);
            }
        }
    }
}
