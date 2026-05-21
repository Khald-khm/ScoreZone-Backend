using ScoreZone.Infrastructure.Auth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ScoreZone.Infrastructure.Extensions
{
    public static class SeedData
    {
        
        public static async Task SeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            await RoleSeeder.SeedRoles(roleManager);
        }
    }
}