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
        double? locationLat, 
        double? locationLng, 
        FacilityStatus status
    );
}