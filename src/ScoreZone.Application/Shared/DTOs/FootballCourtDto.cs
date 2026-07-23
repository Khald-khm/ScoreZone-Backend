using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Shared.DTOs
{
    public record FootballCourtDetailsDto(
        Guid id,
        Guid facilityId,
        Guid ownerId, 
        string? name, 
        string? facilityName,
        string phoneNumber, 
        City city, 
        string address,
        string? profileImageUrl,
        CourtType type, 
        int capacity,
        int pricePerMatch, 
        bool isPartialAllowed, 
        double locationLat, 
        double locationLng, 
        CourtStatus status,
        List<FootballCourtImageDto> courtImages,
        double? distance = null
    );

    public record FootballCourtImageDto(Guid courtId, string imageUrl);
}