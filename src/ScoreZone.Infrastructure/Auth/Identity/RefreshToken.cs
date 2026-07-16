namespace ScoreZone.Infrastructure.Auth.Identity
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string? Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        
    }
}