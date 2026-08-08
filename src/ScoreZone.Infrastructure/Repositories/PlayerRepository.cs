using Microsoft.EntityFrameworkCore;
using ScoreZone.Infrastructure.Data;
using ScoreZone.Application.User.Player.Interfaces;
using ScoreZone.Domain.User.Player;

namespace ScoreZone.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(PlayerEntity player)
        {
            await _context.Players.AddAsync(player);
        }

        // public async Task<(int count, IReadOnlyCollection<PlayerDetailsResponse> items)> GetAllAsync(int skip, int take)
        // {
        //     var query = _context.Players
        //         .Select(x => new PlayerDetailsResponse(
        //             x.Id, x.FirstName, x.LastName, x.PhoneNumber, x.City, x.Address, x.ProfileImage
        //         ));

        //     var count = await query.CountAsync();

        //     var items = await query.Skip(skip)
        //         .Take(take)
        //         .ToListAsync();
            
        //     return (count, items);
        // }


        // public async Task<IReadOnlyCollection<PlayerShortResponse>> GetAllShortAsync()
        // {
        //     return await _context.Players
        //         .Select(x => new PlayerShortResponse(
        //             x.Id, x.FirstName, x.LastName
        //         )).ToListAsync();
        // }

        public async Task<PlayerEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Players.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

    }
}