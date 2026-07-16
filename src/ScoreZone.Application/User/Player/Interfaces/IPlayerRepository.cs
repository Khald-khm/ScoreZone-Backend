using ScoreZone.Domain.User.Player;

namespace ScoreZone.Application.User.Player.Interfaces
{
    public interface IPlayerRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(PlayerEntity player);

        Task<PlayerEntity?> GetByIdAsync(Guid id);
        
    }
    
}