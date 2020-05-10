using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chatty.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chatty.Application.Features.Chat
{
    public sealed class GetAllChats
    {
        public class GetAllChatsQuery : IRequest<IEnumerable<ChatDto>>
        {
        }

        public class GetAllChatsQueryHandler : IRequestHandler<GetAllChatsQuery, IEnumerable<ChatDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllChatsQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ChatDto>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Chats
                    .ProjectTo<ChatDto>(_mapper.ConfigurationProvider)
                    .OrderBy(c => c.Id)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
