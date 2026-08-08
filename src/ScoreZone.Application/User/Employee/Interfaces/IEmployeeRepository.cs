using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Application.User.Employee.DTOs;
using ScoreZone.Domain.User.Employee;

namespace ScoreZone.Application.User.Employee.Interfaces
{
    public interface IEmployeeRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(EmployeeEntity employee);
        Task DeleteAsync(Guid id);

        Task<EmployeeEntity?> GetByIdAsync(Guid id);

        Task<bool> OwnsEmployee(Guid ownerId, Guid employeeId);

        Task<List<Guid>> MyFootballCourts(Guid employeeId);

        Task<IReadOnlyList<EmployeeEntity>> MyEmployees(Guid ownerId);
        
    }
    
}