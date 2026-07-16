using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Facility.Services;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Application.Reservation.Interfaces;
using ScoreZone.Application.Reservation.Services;

namespace ScoreZone.Application.Extensions
{
    public static class ApplicationDIHandler
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationDIHandler).Assembly;

            services.AddValidatorsFromAssembly(assembly);



            // ===========================
            //  Services
            // ===========================
            
            services.AddScoped<IFootballCourtService, FootballCourtService>();

            services.AddScoped<IFacilityService, FacilityService>();

            services.AddScoped<IReservationService, ReservationService>();


            return services;
        }
    }
}