using ScoreZone.Application.User.Owner.DTOs;
using ScoreZone.Domain.User.Owner;

namespace ScoreZone.Application.User.Owner.Mappings
{
    internal static class OwnerToEntity
    {

        public static OwnerEntity ToEntity(this AddOwnerRequest request)
        => new(request.identityId, request.firstName, request.lastName, request.phoneNumber, request.city, 
            request.address, request.profileImageUrl);
    }
}