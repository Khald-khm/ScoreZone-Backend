using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.FootballCourt;

namespace ScoreZone.Application.FootballCourt.Mappings
{

    internal static class FootballCourtToDto
    {
        public static FootballCourtDetailsDto ToDto(this FootballCourtEntity entity)
            => new(entity.FacilityId, entity.OwnerId, entity.Name, entity.PhoneNumber, entity.City, 
                entity.Address, entity.ProfileImage, entity.Type, entity.Capacity, entity.PricePerMatch, 
                entity.IsPartialAllowed, entity.LocationLat, entity.LocationLng, entity.Status, 
                entity.CourtImages.Select(x => x.ToDto()).ToList());

        public static FootballCourtImageDto ToDto(this CourtImage courtImage)
        => new(courtImage.CourtId, courtImage.ImageUrl);
    }
    
}