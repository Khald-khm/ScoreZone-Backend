using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.FootballCourt.Interfaces
{
    public interface IFootballCourtRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(FootballCourtEntity court);
        Task<(int count , IReadOnlyCollection<FootballCourtDetailsDto> items)> GetAllAsync(int skip, int take);
        Task<(int count, IReadOnlyCollection<FootballCourtEntity> items)> GetAllActiveAsync(City city);
        Task<FootballCourtEntity?> GetByIdAsync(Guid id);
        
    }
    
}