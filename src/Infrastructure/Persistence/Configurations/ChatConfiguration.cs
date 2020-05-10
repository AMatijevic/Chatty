using Chatty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatty.Infrastructure.Persistence.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.Property(t => t.Subject)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(t => t.Type)
                .IsRequired();
        }
    }
}
