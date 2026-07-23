using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Facility.Interfaces
{
    public interface IFacilityService
    {
        Task<AppResult> AddAsync(AddFacilityRequest request);

        Task<AppResult<PaginatedResultDto<FacilityDetailsDto>>> GetAllAsync(int pageNumber, int pageSize);

        Task<AppResult<IReadOnlyCollection<FacilityShortDto>>> GetAllShortAsync();

        Task<AppResult<FacilityDetailsDto>> GetByIdAsync(Guid id);
        
        Task<AppResult> ChangeStatusAsync(Guid id, FacilityStatus status);

    }
    
}