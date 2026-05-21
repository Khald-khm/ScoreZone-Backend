using System.Text;
using Microsoft.IdentityModel.Tokens;
using ScoreZone.Infrastructure.Options;

namespace ScoreZone.API.Extensions
{
    public static class AuthenticationExtension
    {        

        public static void AddJwtAuthenticationConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JWT").Get<JwtOptions>();
            // var jwtOptions = JwtOptions

            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings!.Issuer,
                        ValidAudience = jwtSettings.Audience,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Key)
                        )
                    };
                });
            
        }
    }
}