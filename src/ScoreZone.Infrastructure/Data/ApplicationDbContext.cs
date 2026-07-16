using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Infrastructure.Auth.Identity;
using ScoreZone.Infrastructure.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreZone.Domain.User.Player;
using ScoreZone.Domain.User.Owner;
using ScoreZone.Domain.User.Admin;
using ScoreZone.Domain.User.Employee;
using ScoreZone.Domain.Facility;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Reservation;
using ScoreZone.Domain.Notification;

namespace ScoreZone.Infrastructure.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly DomainEventDispatcher _domainEventDispatcher;
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            DomainEventDispatcher domainEventDispatcher,
            ILogger<ApplicationDbContext> logger
        ) : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _logger = logger;
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<AdminEntity> Admins { get; set; }
        
        public DbSet<PlayerEntity> Players { get; set; }

        public DbSet<OwnerEntity> Owners { get; set; }
        
        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<FacilityEntity> Facilities { get; set; }

        public DbSet<FacilityImage> FacilityImages { get; set; }

        public DbSet<FacilityRate> FacilityRates { get; set; }
        
        public DbSet<FootballCourtEntity> FootballCourts { get; set; }

        public DbSet<CourtImage> CourtImages { get; set; }

        public DbSet<CourtRate> CourtRates { get; set; }
        
        public DbSet<FavoriteCourt> FavoriteCourts { get; set; }

        public DbSet<ReservationEntity> Reservations { get; set; }

        public DbSet<NotificationEntity> Notifications { get; set; }
        

        















        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogWarning("Custom SaveChangesAsync START. Pending changes: {Count}", ChangeTracker.Entries().Count());
            var domainEntities = ChangeTracker.Entries<Entity>()
                                .Where(x => x.Entity.DomainEvents.Any())
                                .ToList();
            

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();


            foreach(var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);

            var result = await base.SaveChangesAsync();

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