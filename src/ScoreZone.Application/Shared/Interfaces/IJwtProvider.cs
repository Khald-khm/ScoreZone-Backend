namespace ScoreZone.Application.Shared.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> CreateToken(string identityId, Guid userId, string firstName, string lastName, string username, IList<string> roles);
        
        string CreateRefreshToken();

    }
}