using ScoreZone.Domain.FootballCourt;

namespace ScoreZone.Application.FootballCourt.Interfaces
{
    public interface IFootballCourtRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(FootballCourtEntity court);

        Task<FootballCourtEntity?> GetByIdAsync(Guid id);
        
    }
    
}