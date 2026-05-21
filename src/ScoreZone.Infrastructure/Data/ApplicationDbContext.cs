using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Infrastructure.Auth.Identity;
using ScoreZone.Infrastructure.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ScoreZone.Infrastructure.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly DomainEventDispatcher _domainEventDispatcher;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            DomainEventDispatcher domainEventDispatcher
        ) : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        















        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            
            var domainEntities = ChangeTracker.Entries<Entity>()
                                .Where(x => x.Entity.DomainEvents.Any())
                                .ToList();
            

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();


            foreach(var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            var result = await base.SaveChangesAsync();

            await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);

            return result;
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This line tells EF Core: 
            // "Go find every class that implements IEntityTypeConfiguration 
            // in this project and apply it."
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}