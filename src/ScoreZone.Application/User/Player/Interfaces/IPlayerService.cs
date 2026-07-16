using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.User.Player.DTOs;

namespace ScoreZone.Application.User.Player.Interfaces
{
    public interface IPlayerService
    {
        Task<AppResult> AddAsync(AddPlayerRequest request);

        Task<AppResult> GetByIdAsync(Guid id);
        
    }
    
}