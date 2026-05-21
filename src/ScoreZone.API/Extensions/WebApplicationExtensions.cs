using ScoreZone.Infrastructure.Data;
using ScoreZone.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ScoreZone.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseDataBaseMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }

        public static async void UseSeedDataAsync(this WebApplication app)
        {
            
            await app.SeedAsync();
        }
        
        public static void UseApplicationMiddleware(this WebApplication app)
        {

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}