using ScoreZone.Application.FootballCourt.Mappings;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.Facility;

namespace ScoreZone.Application.Facility.Mappings
{
    internal static class FacilityToDto
    {
        public static FacilityDetailsDto ToDto(this FacilityEntity entity)
        => new(entity.Id, entity.Name, entity.Description, entity.PhoneNumber, entity.City, 
            entity.Address, entity.ProfileImage, entity.LocationLat, entity.LocationLng, entity.Status, 
            entity.FacitlityImages.Select(x => x.ToDto()).ToList(), entity.FootballCourts.Select(x => x.ToDto()).ToList());
        
        public static FacilityImageDto ToDto(this FacilityImage facilityImage)
        => new(facilityImage.FacilityId, facilityImage.ImageUrl);
    }
}