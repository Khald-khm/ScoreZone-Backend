using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.Facility;

namespace ScoreZone.Application.Facility.Interfaces
{
    public interface IFacilityRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(FacilityEntity facility);

        Task<(int count , IReadOnlyCollection<FacilityDetailsDto> items)> GetAllAsync(int skip, int take);
        Task<IReadOnlyCollection<FacilityShortDto>> GetAllShortAsync();

        Task<FacilityEntity?> GetByIdAsync(Guid id);
        
    }
    
}