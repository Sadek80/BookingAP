using BookingAP.Application.Abstractions.Scheduling;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Exceptions;
using BookingAP.Infrastructure.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace BookingAP.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IBackgroundJobService _backgroundService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _backgroundService = this.GetInfrastructure().GetRequiredService<IBackgroundJobService>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                AddDomainEventsAsOutboxMessages();

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("Try Again Later.", ex);
            }
        }

        private void AddDomainEventsAsOutboxMessages()
        {
            var domainEvents = ChangeTracker
                .Entries<IEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .AsEnumerable();

            _backgroundService.Enqueue<ProcessCoreEventJob>(e => e.Process(domainEvents));
        }
    }
}
