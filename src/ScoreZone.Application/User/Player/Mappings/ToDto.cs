using ScoreZone.Application.Auth;
using ScoreZone.Application.User.Player.DTOs;
using ScoreZone.Domain.User.Player;

namespace ScoreZone.Application.User.Player.Mappings
{
    internal static class PlayerToDto
    {


        public static PlayerDetailsResponse ToDto(this PlayerEntity entity)
        => new(entity.Id, entity.FirstName, entity.LastName, entity.PhoneNumber, entity.City, 
            entity.Address, entity.ProfileImage);
        
        public static UpdateProfileDTO ToAuth(this UpdatePlayerRequest request)
        => new(request.firstName, request.lastName, request.gender, request.birthDate, request.email, request.city, request.address);

    }
}