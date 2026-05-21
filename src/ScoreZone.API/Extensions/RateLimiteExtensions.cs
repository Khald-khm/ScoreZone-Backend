using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace ScoreZone.API.Extensions
{
    public static class RateLimiteExtensions
    {
        
        public static IServiceCollection AddRateLimiterPolicies(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("AuthPoliciy", context =>  
                    RateLimitPartition.GetFixedWindowLimiter(partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    { 
                        PermitLimit = 5, 
                        Window = TimeSpan.FromMinutes(1)
                    })
                );

                options.AddPolicy("UploadPolicy", context =>
                    RateLimitPartition.GetFixedWindowLimiter(partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 3,
                        Window = TimeSpan.FromMinutes(5)
                    })
                );

                options.AddPolicy("StandardPolicy", context => 
                    RateLimitPartition.GetFixedWindowLimiter(partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1)
                    })
                );



                // Fallback RateLimiter
                options.AddPolicy("GlobalFallback", context => 
                    RateLimitPartition.GetFixedWindowLimiter(partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 1000,
                        Window = TimeSpan.FromMinutes(1)
                    })
                );

            });



            return services;
        }
    }
}