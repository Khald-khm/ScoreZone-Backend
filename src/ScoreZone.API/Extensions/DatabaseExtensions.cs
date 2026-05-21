using Microsoft.EntityFrameworkCore;
using ScoreZone.Infrastructure.Data;

namespace ScoreZone.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDatabaseConfigurations(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sql =>
                {
                    sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });
        }
    }
}