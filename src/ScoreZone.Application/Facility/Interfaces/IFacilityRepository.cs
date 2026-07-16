using ScoreZone.Domain.Facility;

namespace ScoreZone.Application.Facility.Interfaces
{
    public interface IFacilityRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(FacilityEntity facility);

        Task<FacilityEntity?> GetByIdAsync(Guid id);
        
    }
    
}