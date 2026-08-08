using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Infrastructure.Auth.JWT;
using ScoreZone.Infrastructure.Options;
using ScoreZone.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using ScoreZone.Infrastructure.Auth.Identity;
using Microsoft.AspNetCore.Identity;
using ScoreZone.Infrastructure.Data;
using ScoreZone.Application.Auth;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Infrastructure.Repositories;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Reservation.Interfaces;
using ScoreZone.Application.User.Owner.Interfaces;
using ScoreZone.Application.User.Player.Interfaces;
using ScoreZone.Application.User.Employee.Interfaces;

namespace ScoreZone.Infrastructure.Extensions
{
    public static class InfrastructureDIHandler
    {
        public static IServiceCollection AddInfrastructureDepndencies(this IServiceCollection services)
        {
            // var assembly = typeof(InfrastructureDIHandler).Assembly;

            // This is required by CurrentUserAccessor to read the Token
            services.AddHttpContextAccessor();

            

            // ==========================
            // Identity & Auth
            // ==========================
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IAuthService, AuthService>();



            // ==========================
            //  File Service
            // ==========================
            services.AddScoped<IFileService, FileService>();

            services.AddOptions<FileStorageOptions>()
                .BindConfiguration("FileStorage")
                .ValidateDataAnnotations()
                .ValidateOnStart();
            


            // ==========================
            //  JWT Authentication
            // ==========================
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddOptions<JwtOptions>()
                .BindConfiguration("JWT")
                .ValidateOnStart();
            




            // ==========================
            //  MediatR & Domain Events
            // ==========================
            services.AddScoped<DomainEventDispatcher>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DomainEventDispatcher).Assembly);
                // Add your domain events here


            });


            // ==========================
            //  Repositories
            // ==========================

            services.AddScoped<IFootballCourtRepository, FootballCourtRepository>();

            services.AddScoped<IFacilityRepository, FacilityRepository>();

            services.AddScoped<IReservationRepository, ReservationRepository>();

            services.AddScoped<IOwnerRepository, OwnerRepository>();

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();






            return services;
        }
    }
}