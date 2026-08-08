using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.User.Player.DTOs;

namespace ScoreZone.Application.User.Player.Interfaces
{
    public interface IPlayerService
    {
        Task<AppResult> AddAsync(AddPlayerRequest request);
        Task<AppResult> UpdateAsync(Guid? id, UpdatePlayerRequest request);
        Task<AppResult> DeleteAsync(Guid? id);

        Task<AppResult<PlayerDetailsResponse>> GetByIdAsync(Guid? id);
        
    }
    
}