using Chatty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatty.Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(t => t.Chat)
              .WithMany(ta => ta.Events)
              .HasForeignKey(t => t.ChatId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.User)
               .WithMany(ta => ta.Events)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Type)
                .IsRequired();
        }
    }
}
