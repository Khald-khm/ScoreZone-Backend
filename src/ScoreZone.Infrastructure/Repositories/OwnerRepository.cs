using Microsoft.EntityFrameworkCore;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Infrastructure.Data;
using ScoreZone.Application.User.Owner.Interfaces;
using ScoreZone.Domain.User.Owner;
using ScoreZone.Application.User.Owner.DTOs;

namespace ScoreZone.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext _context;

        public OwnerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(OwnerEntity court)
        {
            await _context.Owners.AddAsync(court);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Owners.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<(int count, IReadOnlyCollection<OwnerDetailsResponse> items)> GetAllAsync(int skip, int take)
        {
            var query = _context.Owners
                .Select(x => new OwnerDetailsResponse(
                    x.Id, x.FirstName, x.LastName, x.PhoneNumber, x.City, x.Address, x.ProfileImage
                ));

            var count = await query.CountAsync();

            var items = await query.Skip(skip)
                .Take(take)
                .ToListAsync();
            
            return (count, items);
        }


        public async Task<IReadOnlyCollection<OwnerShortResponse>> GetAllShortAsync()
        {
            return await _context.Owners
                .Select(x => new OwnerShortResponse(
                    x.Id, x.FirstName, x.LastName
                )).ToListAsync();
        }

        public async Task<OwnerEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Owners
                .Include(x => x.Employees)
                .Include(x => x.FootballCourts.Where(x => x.Status == CourtStatus.Active))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Guid>> MyFootballCourts(Guid ownerId)
        {
            return await _context.FootballCourts
                .AsNoTracking()
                .Where(x => x.OwnerId == ownerId)
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}