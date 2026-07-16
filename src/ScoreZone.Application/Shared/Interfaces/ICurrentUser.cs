using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Shared.Interfaces
{
    public interface ICurrentUser
    {
        string? identityId { get; }
        Guid? userId { get; }
        string? username { get; }
        string? role { get; }
    }
}