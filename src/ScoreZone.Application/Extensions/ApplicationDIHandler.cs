using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ScoreZone.Application.Extensions
{
    public static class ApplicationDIHandler
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationDIHandler).Assembly;

            services.AddValidatorsFromAssembly(assembly);



            return services;
        }
    }
}