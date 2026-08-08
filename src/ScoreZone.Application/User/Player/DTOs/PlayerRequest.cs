using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.User.Player.DTOs
{
    public record AddPlayerRequest(
        string identityId,
        string firstName, 
        string lastName, 
        string phoneNumber, 
        City city, 
        string address, 
        IFormFile? profileImage, 
        string? profileImageUrl, 
        decimal? locationLat, 
        decimal? locationLng, 
        FacilityStatus status
    );

    public record UpdatePlayerRequest(
        string firstName, 
        string lastName, 
        City city, 
        string address, 
        IFormFile? profileImage, 
        string? profileImageUrl, 
        Gender gender,
        DateOnly birthDate,
        string? email
    );
}