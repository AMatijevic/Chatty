using Chatty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Chatty.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Chat> Chats { get; set; }

        DbSet<Event> Events { get; set; }

        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
