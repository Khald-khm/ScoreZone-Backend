using Microsoft.EntityFrameworkCore;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.Facility;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Infrastructure.Data;
using ScoreZone.Application.Facility.Mappings;

namespace ScoreZone.Infrastructure.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly ApplicationDbContext _context;

        public FacilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(FacilityEntity court)
        {
            await _context.AddAsync(court);
        }

        public async Task<(int count, IReadOnlyCollection<FacilityDetailsDto> items)> GetAllAsync(int skip, int take)
        {
            var query = _context.Facilities
                .Where(x => x.Status == FacilityStatus.Active)
                .Select(x => new FacilityDetailsDto(
                    x.Id, x.Name, x.Description, x.PhoneNumber, x.City, x.Address, x.ProfileImage, x.LocationLat, x.LocationLng, x.Status,
                    x.FacitlityImages.Select(x => new FacilityImageDto(x.FacilityId, x.ImageUrl)).ToList(), new List<FootballCourtDetailsDto>()
                ));

            var count = await query.CountAsync();

            var items = await query.Skip(skip)
                .Take(take)
                .ToListAsync();
            
            return (count, items);
        }


        public async Task<IReadOnlyCollection<FacilityShortDto>> GetAllShortAsync()
        {
            return await _context.Facilities
                .Where(x => x.Status == FacilityStatus.Active)
                .Select(x => new FacilityShortDto(
                    x.Id, x.Name
                )).ToListAsync();
        }

        public async Task<FacilityEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Facilities
                .Include(x => x.FacitlityImages)
                .Include(x => x.FootballCourts.Where(x => x.Status == CourtStatus.Active))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}