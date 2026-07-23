using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.FootballCourt.DTOs
{
    public record AddFootballCourtRequest(
        Guid facilityId,
        Guid ownerId, 
        string name, 
        string phoneNumber, 
        City city, 
        string address, 
        IFormFile? profileImage,
        string? profileImageUrl,
        CourtType type, 
        int capacity,
        int pricePerMatch, 
        bool isPartialAllowed, 
        double locationLat, 
        double locationLng, 
        CourtStatus status
    );

    public record LocationCoordsRequest
    (
        City city,
        double locationLat, 
        double locationLng
    );
}