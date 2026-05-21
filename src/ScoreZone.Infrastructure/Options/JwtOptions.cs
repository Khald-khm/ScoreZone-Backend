
namespace ScoreZone.Infrastructure.Options
{
    public class JwtOptions
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; } = "ScoreZone.API";
        public required string Audience { get; set; } = "ScoreZone.API";
        public required int ExpiryMintues { get; set; } = 60;
    }
}