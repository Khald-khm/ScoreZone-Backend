namespace ScoreZone.Application.Shared.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> CreateToken(string identityId, Guid userId, string phoneNumber, IList<string> roles);
        
        string CreateRefreshToken();

    }
}