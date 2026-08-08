using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.User.Employee.DTOs;

namespace ScoreZone.Application.User.Employee.Interfaces
{
    public interface IEmployeeService
    {
        Task<AppResult> AddAsync(AddEmployeeRequest request);

        Task<AppResult> UpdateAsync(Guid? id, UpdateEmployeeRequest request);

        Task<AppResult> DeleteAsync(Guid? id);

        Task<AppResult<EmployeeDetailsResponse>> GetByIdAsync(Guid? id);

        Task<AppResult<IReadOnlyList<EmployeeDetailsResponse>>> MyEmployees();

        Task<AppResult> AddCourtAsync(Guid courtId, Guid employeeId);
        
    }
    
}