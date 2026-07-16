using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Shared.DTOs
{
    
    public record FacilityDetailsDto(
        string name, 
        string? description, 
        string phoneNumber, 
        City city, 
        string address,
        string? profileImageUrl, 
        decimal? locationLat, 
        decimal? locationLng, 
        FacilityStatus status,
        IReadOnlyCollection<FacilityImageDto> FacilityImages,
        IReadOnlyCollection<FootballCourtDetailsDto> FootballCourts
    );

    public record FacilityImageDto(Guid facilityId, string imageUrl);
    
}