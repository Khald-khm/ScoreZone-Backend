using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Facility;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.User.Player.DTOs
{
    public record PlayerDetailsResponse(
        string name, 
        string? description, 
        string phoneNumber, 
        City city, 
        string address, 
        IFormFile? profileImage, 
        string? profileImageUrl, 
        decimal? locationLat, 
        decimal? locationLng, 
        FacilityStatus status,
        List<FacilityImage> FacilityImages,
        List<FootballCourtEntity> FootballCourts
    );
}