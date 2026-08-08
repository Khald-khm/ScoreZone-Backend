using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.User.Employee.DTOs
{
    public record AddEmployeeRequest(
        string identityId,
        Guid ownerId,
        string firstName, 
        string lastName,  
        string phoneNumber, 
        City city, 
        string address, 
        IFormFile? profileImage, 
        string? profileImageUrl
    );
    
    public record UpdateEmployeeRequest(
        string firstName, 
        string lastName, 
        City city, 
        string address, 
        IFormFile? profileImage, 
        string? profileImageUrl, 
        Gender gender,
        DateOnly birthDate,
        string? email
    );
}