using Microsoft.EntityFrameworkCore;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Domain.Facility;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Infrastructure.Data;

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

        public async Task<FacilityEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Facilities
                .Include(x => x.FacitlityImages)
                .Include(x => x.FootballCourts.Where(x => x.Status == CourtStatus.Active))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}