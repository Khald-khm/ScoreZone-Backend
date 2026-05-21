using ScoreZone.API.Services;
using ScoreZone.Application.Extensions;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Infrastructure.Extensions;

namespace ScoreZone.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            ApplicationDIHandler.AddApplicationDependencies(services);
            InfrastructureDIHandler.AddInfrastructureDepndencies(services);
            services.AddScoped<ICurrentUser, CurrentUser>();

        }
    }
}