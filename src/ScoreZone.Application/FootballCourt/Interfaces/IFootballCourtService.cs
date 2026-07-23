using ScoreZone.Application.FootballCourt.DTOs;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.FootballCourt.Interfaces
{
    public interface IFootballCourtService
    {
        Task<AppResult> AddAsync(AddFootballCourtRequest request);

        Task<AppResult<PaginatedResultDto<FootballCourtDetailsDto>>> GetAllAsync(int pageNumber, int pageSize);

        Task<AppResult<FootballCourtDetailsDto>> GetByIdAsync(Guid id);

        Task<AppResult<PaginatedResultDto<FootballCourtDetailsDto>>> BrowseNearbyCourtsAsync(LocationCoordsRequest request, int pageNumber, int pageSize);
        
        Task<AppResult> ChangeStatusAsync(Guid id, CourtStatus status);
        
    }
    
}