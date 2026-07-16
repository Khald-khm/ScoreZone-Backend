using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Facility.DTOs
{
    public record AddFacilityRequest(
        string name, 
        string? description, 
        string phoneNumber, 
        City city, 
        string address, 
        IFormFile? profileImage, 
        string? profileImageUrl, 
        decimal? locationLat, 
        decimal? locationLng, 
        FacilityStatus status
    );
}