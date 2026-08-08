using ScoreZone.Application.User.Employee.DTOs;
using ScoreZone.Domain.User.Employee;

namespace ScoreZone.Application.User.Employee.Mappings
{
    internal static class EmployeeToEntity
    {

        public static EmployeeEntity ToEntity(this AddEmployeeRequest request)
        => new(request.identityId, request.ownerId, request.firstName, request.lastName, request.phoneNumber, request.city, 
            request.address, request.profileImageUrl);
    }
}