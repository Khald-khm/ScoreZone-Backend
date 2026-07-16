using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Domain.Facility;

namespace ScoreZone.Application.Facility.Mappings
{
    internal static class FacilityToEntity
    {

        public static FacilityEntity ToEntity(this AddFacilityRequest request)
        => new(request.name, request.description, request.phoneNumber, request.city, 
            request.address, request.profileImageUrl, request.locationLat, request.locationLng, request.status);
    }
}