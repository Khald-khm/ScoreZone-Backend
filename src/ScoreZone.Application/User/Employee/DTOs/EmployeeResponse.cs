using Microsoft.AspNetCore.Http;
using ScoreZone.Domain.Facility;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.User.Employee.DTOs
{
    public record EmployeeDetailsResponse(
        Guid id,
        Guid ownerId,
        string firstName, 
        string lastName, 
        string phoneNumber, 
        City city, 
        string address, 
        string? profileImageUrl
    );
}