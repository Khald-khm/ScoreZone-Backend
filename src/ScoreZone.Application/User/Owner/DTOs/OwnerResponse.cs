using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Facility;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.User.Owner.DTOs
{
    public record OwnerDetailsResponse(
        Guid id,
        string firstName,  
        string lastName,  
        string phoneNumber, 
        City city, 
        string address, 
        string? profileImageUrl
    );
    
    public record OwnerShortResponse(
        Guid id,
        string firstName,
        string lastName
    );
}