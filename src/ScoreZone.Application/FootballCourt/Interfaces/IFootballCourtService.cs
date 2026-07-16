using ScoreZone.Application.FootballCourt.DTOs;
using ScoreZone.Application.Shared.Results;

namespace ScoreZone.Application.FootballCourt.Interfaces
{
    public interface IFootballCourtService
    {
        Task<AppResult> AddAsync(AddFootballCourtRequest request);

        Task<AppResult> GetByIdAsync(Guid id);

        Task<AppResult> BrowseNearbyCourtsAsync(LocationCoordsRequest request);
        
    }
    
}