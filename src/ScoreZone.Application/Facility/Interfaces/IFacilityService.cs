using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Results;

namespace ScoreZone.Application.Facility.Interfaces
{
    public interface IFacilityService
    {
        Task<AppResult> AddAsync(AddFacilityRequest request);

        Task<AppResult<FacilityDetailsDto>> GetByIdAsync(Guid id);
        
    }
    
}