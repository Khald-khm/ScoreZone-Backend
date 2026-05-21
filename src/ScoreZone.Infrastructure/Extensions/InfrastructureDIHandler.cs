using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Infrastructure.Auth.JWT;
using ScoreZone.Infrastructure.Options;
using ScoreZone.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

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












            return services;
        }
    }
}