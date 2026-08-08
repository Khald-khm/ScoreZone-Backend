using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Facility.Services;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Application.Reservation.Interfaces;
using ScoreZone.Application.Reservation.Services;
using ScoreZone.Application.User.Employee.Interfaces;
using ScoreZone.Application.User.Owner.Interfaces;
using ScoreZone.Application.User.Player.Interfaces;

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
            
            services.AddScoped<IFacilityService, FacilityService>();

            services.AddScoped<IFootballCourtService, FootballCourtService>();

            services.AddScoped<IReservationService, ReservationService>();

            services.AddScoped<IOwnerService, OwnerService>();

            services.AddScoped<IPlayerService, PlayerService>();
            
            services.AddScoped<IEmployeeService, EmployeeService>();


            return services;
        }
    }
}