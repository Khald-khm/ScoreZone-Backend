using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.User.Player.DTOs
{
    public record PlayerDetailsResponse(
        Guid id, 
        string firstName, 
        string lastName, 
        string phoneNumber, 
        City city, 
        string address, 
        string? profileImageUrl
    );
}