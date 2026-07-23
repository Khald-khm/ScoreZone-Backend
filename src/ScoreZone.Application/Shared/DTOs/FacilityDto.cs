using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Shared.DTOs
{
    
    public record FacilityDetailsDto(
        Guid id,
        string name, 
        string? description, 
        string phoneNumber, 
        City city, 
        string address,
        string? profileImageUrl, 
        double? locationLat, 
        double? locationLng, 
        FacilityStatus status,
        IReadOnlyCollection<FacilityImageDto> FacilityImages,
        IReadOnlyCollection<FootballCourtDetailsDto> FootballCourts
    );

    public record FacilityShortDto(
        Guid Id,
        string name
    );

    public record FacilityImageDto(Guid facilityId, string imageUrl);
    
}