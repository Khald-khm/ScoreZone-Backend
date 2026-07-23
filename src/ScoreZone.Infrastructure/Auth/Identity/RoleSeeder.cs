using ScoreZone.Domain.Shared.Enum;
using Microsoft.AspNetCore.Identity;

namespace ScoreZone.Infrastructure.Auth.Identity
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach(var role in Enum.GetNames(typeof(Roles)))
            {
                if(!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}