using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.User.Owner.DTOs
{
    public record AddOwnerRequest(
        string identityId,
        string firstName, 
        string lastName, 
        string phoneNumber, 
        City city, 
        string address, 
        IFormFile? profileImage,
        string profileImageUrl
    );
}