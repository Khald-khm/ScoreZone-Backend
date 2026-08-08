using ScoreZone.Application.User.Player.DTOs;
using ScoreZone.Domain.User.Player;

namespace ScoreZone.Application.User.Player.Mappings
{
    internal static class PlayerToEntity
    {

        public static PlayerEntity ToEntity(this AddPlayerRequest request)
        => new(request.identityId, request.firstName, request.lastName, request.phoneNumber, request.city, 
            request.address, request.profileImageUrl);

    }
}