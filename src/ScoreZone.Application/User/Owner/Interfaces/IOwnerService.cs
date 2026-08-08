using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.User.Owner.DTOs;

namespace ScoreZone.Application.User.Owner.Interfaces
{
    public interface IOwnerService
    {
        Task<AppResult> UpdateAsync(Guid? id, UpdateOwnerRequest request);
        Task<AppResult> DeleteAsync(Guid? id);

        Task<AppResult<PaginatedResultDto<OwnerDetailsResponse>>> GetAllAsync(int pageNumber, int pageSize);
        Task<AppResult<IReadOnlyCollection<OwnerShortResponse>>> GetAllShortAsync();
        Task<AppResult<OwnerDetailsResponse>> GetByIdAsync(Guid? id);
        
    }
    
}