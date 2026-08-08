using ScoreZone.Application.User.Owner.DTOs;
using ScoreZone.Domain.User.Owner;

namespace ScoreZone.Application.User.Owner.Interfaces
{
    public interface IOwnerRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(OwnerEntity owner);
        Task DeleteAsync(Guid id);

        Task<(int count, IReadOnlyCollection<OwnerDetailsResponse> items)> GetAllAsync(int skip, int take);

        Task<IReadOnlyCollection<OwnerShortResponse>> GetAllShortAsync();

        Task<OwnerEntity?> GetByIdAsync(Guid id);

        Task<List<Guid>> MyFootballCourts(Guid ownerId);
        
    }
    
}