using Microsoft.EntityFrameworkCore;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Infrastructure.Data;

namespace ScoreZone.Infrastructure.Repositories
{
    public class FootballCourtRepository : IFootballCourtRepository
    {
        private readonly ApplicationDbContext _context;

        public FootballCourtRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(FootballCourtEntity court)
        {
            await _context.AddAsync(court);
        }

        public async Task<FootballCourtEntity?> GetByIdAsync(Guid id)
        {
            return await _context.FootballCourts
                .Include(x => x.CourtImages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}