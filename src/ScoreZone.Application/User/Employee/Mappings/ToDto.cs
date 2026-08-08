using ScoreZone.Application.Auth;
using ScoreZone.Application.User.Employee.DTOs;
using ScoreZone.Domain.User.Employee;

namespace ScoreZone.Application.User.Employee.Mappings
{
    internal static class EmployeeToDto
    {

        public static EmployeeDetailsResponse ToDto(this EmployeeEntity entity)
        => new(entity.Id, entity.OwnerId, entity.FirstName, entity.LastName, entity.PhoneNumber, entity.City, 
            entity.Address, entity.ProfileImage);
        
        public static UpdateProfileDTO ToAuth(this UpdateEmployeeRequest request)
        => new(request.firstName, request.lastName, request.gender, request.birthDate, request.email, request.city, request.address);

    }
}