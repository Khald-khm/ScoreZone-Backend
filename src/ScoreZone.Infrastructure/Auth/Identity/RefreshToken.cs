namespace ScoreZone.Infrastructure.Auth.Identity
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;
        
    }
}