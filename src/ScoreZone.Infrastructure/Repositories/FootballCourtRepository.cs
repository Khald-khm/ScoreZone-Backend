using Microsoft.EntityFrameworkCore;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Enum;
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

        public async Task<(int count , IReadOnlyCollection<FootballCourtDetailsDto> items)> GetAllAsync(int skip, int take)
        {
            var query = _context.FootballCourts
                .Select(x => new FootballCourtDetailsDto(
                    x.Id, x.FacilityId, x.OwnerId, x.Name, x.Facility.Name, x.PhoneNumber, x.City, x.Address,
                    x.ProfileImage, x.Type, x.Capacity, x.PricePerMatch, x.IsPartialAllowed, x.LocationLat,
                    x.LocationLng, x.Status, x.CourtImages.Select(i => new FootballCourtImageDto(i.CourtId, i.ImageUrl)).ToList()
                ));

            var count = await query.CountAsync();

            var items = await query.Skip(skip)
                .Take(take)
                .ToListAsync();
            
            return (count, items);
        }

        public async Task<(int count, IReadOnlyCollection<FootballCourtEntity> items)> GetAllActiveAsync(City city)
        {
            var query = _context.FootballCourts
                .Where(x => x.Status == CourtStatus.Active && x.City == city)
                // .Select(x => new FootballCourtDetailsDto(
                //     x.FacilityId, x.OwnerId, x.Name, x.Facility.Name, x.PhoneNumber, x.City, x.Address,
                //     x.ProfileImage, x.Type, x.Capacity, x.PricePerMatch, x.IsPartialAllowed, x.LocationLat,
                //     x.LocationLng, x.Status, x.CourtImages.Select(i => new FootballCourtImageDto(i.CourtId, i.ImageUrl)).ToList()
                // ));
                .Include(x => x.CourtImages)
                .Include(x => x.Facility);

            var count = await query.CountAsync();
                
            var items = await query.ToListAsync();

            return (count, items);
        }

        public async Task<FootballCourtEntity?> GetByIdAsync(Guid id)
        {
            return await _context.FootballCourts
                .Include(x => x.CourtImages)
                .Include(x => x.Facility)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}