using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Shared.DTOs
{
    public record FootballCourtDetailsDto(
        Guid facilityId,
        Guid ownerId, 
        string? name, 
        string phoneNumber, 
        City city, 
        string address,
        string? profileImageUrl,
        CourtType type, 
        int capacity,
        int pricePerMatch, 
        bool isPartialAllowed, 
        decimal locationLat, 
        decimal locationLng, 
        CourtStatus status,
        List<FootballCourtImageDto> courtImages
    );

    public record FootballCourtImageDto(Guid courtId, string imageUrl);
}