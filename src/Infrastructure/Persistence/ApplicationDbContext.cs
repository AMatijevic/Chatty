using Chatty.Application.Common.Interfaces;
using Chatty.Domain.Common;
using Chatty.Domain.Entities;
using Chatty.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Chatty.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        private IDbContextTransaction _currentTransaction;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            Validate();
            UpdateTrackingColumns();

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void Validate()
        {
            var validationErrors = ChangeTracker
                .Entries<BaseEntity>()
                .SelectMany(e => e.Entity.GetValidateResults())
                .Where(r => r != ValidationResult.Success)
                .Select(v => v.ErrorMessage)
                .ToArray();

            if (validationErrors.Any())
            {
                throw new PersistenceException(validationErrors);
            }
        }

        private void UpdateTrackingColumns()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[nameof(entry.Entity.CreatedBy)] = "CurrentUser";
                        entry.CurrentValues[nameof(entry.Entity.Created)] = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.CurrentValues[nameof(entry.Entity.LastModifiedBy)] = "CurrentUser";
                        entry.CurrentValues[nameof(entry.Entity.LastModified)] = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
