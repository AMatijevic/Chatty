using Chatty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatty.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.Username)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Password)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Nickname)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
