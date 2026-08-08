namespace ScoreZone.Application.Shared.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> CreateToken(string identityId, Guid userId, IList<string> roles);
        
        string CreateRefreshToken();

    }
}