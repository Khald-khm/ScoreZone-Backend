using Microsoft.Extensions.Logging;
using ScoreZone.Application.User.Employee.DTOs;
using ScoreZone.Application.User.Employee.Mappings;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Domain.Shared.Exceptions;
using ScoreZone.Domain.User.Employee;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Application.Auth;
using ScoreZone.Application.Shared.DTOs;

namespace ScoreZone.Application.User.Employee.Interfaces
{
    public class EmployeeService : BaseApplicationService, IEmployeeService
    {

        private readonly IEmployeeRepository _repo;
        private readonly IFootballCourtRepository _courtRepo;
        private readonly IFileService _fileService;
        private readonly IAuthService _authService;
        private readonly ICurrentUser _currentUser;

        public EmployeeService(
                IEmployeeRepository repo, 
                IFootballCourtRepository courtRepo,
                IFileService fileService,
                IAuthService authService,
                ICurrentUser currentUser,
                IServiceProvider serviceProvider, 
                ILogger<EmployeeService> logger) 
        : base(serviceProvider, logger) 
        {
            _repo = repo;
            _courtRepo = courtRepo;
            _fileService = fileService;
            _authService = authService;
            _currentUser = currentUser;
        }

        public async Task<AppResult> AddAsync(AddEmployeeRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var employee = request.ToEntity();
                
                employee.ProfileImage = null;
                if(request.profileImage is not null)
                {
                    var imageUrl = await _fileService.UploadFileAsync(request.profileImage);
                    employee.ProfileImage = imageUrl;
                }


                await _repo.AddAsync(employee);
                await _repo.SaveChangesAsync();
            });
        }


        public async Task<AppResult> UpdateAsync(Guid? id, UpdateEmployeeRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var employeeId = id ?? _currentUser.userId.Value;

                var employee = await _repo.GetByIdAsync(employeeId);

                if(employee is null)
                    throw new AppException(404, "Owner Not Found.");
                
                employee.Update(request.firstName, request.lastName, employee.PhoneNumber, request.city, request.address);
                
                // UPDATE OR DELETE PROFILE IMAGE
                if(string.IsNullOrWhiteSpace(request.profileImageUrl))
                {
                    if(employee.ProfileImage is not null)
                    {
                        await _fileService.DeleteFileAsync(employee.ProfileImage);
                        employee.ProfileImage = null;
                    }   

                    if(request.profileImage is not null)
                        employee.ProfileImage = await _fileService.UploadFileAsync(request.profileImage);
                }

                await _authService.UpdateProfileAsync(employee.IdentityId, request.ToAuth());

                await _repo.SaveChangesAsync();

            });
        }

        public async Task<AppResult> DeleteAsync(Guid? id)
        {
            return await ExecuteAsync(async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var employeeId = id ?? _currentUser.userId.Value;

                var owner = await _repo.GetByIdAsync(employeeId);
                
                if(owner is null)
                    throw new AppException(404, "Owner Not Found.");
                
                if(owner.ProfileImage is not null)
                    await _fileService.DeleteFileAsync(owner.ProfileImage);
                
                await _repo.DeleteAsync(employeeId);
                
            });
        }


        public async Task<AppResult<EmployeeDetailsResponse>> GetByIdAsync(Guid? id)
        {
            return await ExecuteAsync(id, async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var employeeId = id ?? _currentUser.userId.Value;

                if(id is not null && _currentUser.role != "Admin")
                    throw new AppException(403, "You Are Not Allowed.");
                
                var employee = await _repo.GetByIdAsync(employeeId);

                if(employee is null)
                    throw new AppException(404, "Employee Not Found.");

                return employee.ToDto();
            });
        }


        public async Task<AppResult<IReadOnlyList<EmployeeDetailsResponse>>> MyEmployees()
        {
            return await ExecuteAsync(async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var ownerId = _currentUser.userId.Value;

                var employees = await _repo.MyEmployees(ownerId);

                IReadOnlyList<EmployeeDetailsResponse> result = employees.Select(x => x.ToDto()).ToList().AsReadOnly(); 
                
                return result;
            });
        }


        public async Task<AppResult> AddCourtAsync(Guid courtId, Guid employeeId)
        {
            return await ExecuteAsync(courtId, async () =>
            {
                var ownerId = _currentUser.userId!.Value;

                if(ownerId == Guid.Empty)
                    throw new AppException(404, "Owner Id Cannot Be Found.");
                
                var ownsEmployee = await _repo.OwnsEmployee(ownerId, employeeId);

                if(!ownsEmployee)
                    throw new AppException(403, "Cannot Access This Employee.");
                
                var employee = await _repo.GetByIdAsync(employeeId);
                
                if(employee is null)
                    throw new AppException(404, "Employee Not Found.");

                var court = await _courtRepo.GetByIdAsync(courtId);

                if(court is null)
                    throw new AppException(404, "Football Court Not Found.");

                employee.AddCourt(court);

                await _repo.SaveChangesAsync();

            });
        }
        
    }
    
}