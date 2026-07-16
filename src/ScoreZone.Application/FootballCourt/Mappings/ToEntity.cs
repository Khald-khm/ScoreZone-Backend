using ScoreZone.Application.FootballCourt.DTOs;
using ScoreZone.Domain.FootballCourt;

namespace ScoreZone.Application.FootballCourt.Mappings
{
    internal static class FootballCourtToEntity
    {

        public static FootballCourtEntity ToEntity(this AddFootballCourtRequest request)
        => new(request.facilityId, request.ownerId, request.name, request.phoneNumber, request.city, 
            request.address, request.profileImageUrl, request.type, request.capacity, request.pricePerMatch, 
            request.isPartialAllowed, request.locationLat, request.locationLng, request.status);
    }
}