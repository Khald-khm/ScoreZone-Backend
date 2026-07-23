using ScoreZone.Application.User.Owner.DTOs;
using ScoreZone.Domain.User.Owner;

namespace ScoreZone.Application.User.Owner.Mappings
{
    internal static class OwnerToDto
    {

        public static OwnerDetailsResponse ToDto(this OwnerEntity entity)
        => new(entity.Id, entity.FirstName, entity.LastName, entity.PhoneNumber, entity.City, 
            entity.Address, entity.ProfileImage);
    }
}