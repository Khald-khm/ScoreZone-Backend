using Microsoft.EntityFrameworkCore;
using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Application.Reservation.Interfaces;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.Reservation;
using ScoreZone.Infrastructure.Data;

namespace ScoreZone.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(ReservationEntity court)
        {
            await _context.AddAsync(court);
        }

        public async Task<ReservationEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ReservationDetails?> GetDetailsByIdAsync(Guid id)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Join(_context.FootballCourts, 
                    reservation => reservation.CourtId, 
                    court => court.Id, (reservation, court) => new {reservation, court})
                .Join(_context.Facilities, 
                    combined => combined.court.FacilityId, 
                    facility => facility.Id, (combined, facility) => 
                    new ReservationDetails(
                        combined.reservation.Id, combined.reservation.PlayerId, combined.reservation.CourtId, combined.court.Name, 
                        combined.court.ProfileImage, combined.court.Type, combined.court.City, facility.Name, 
                        combined.reservation.TimeSlotNum, combined.reservation.Status, combined.reservation.Deposite, 
                        combined.reservation.Payment, combined.reservation.ReservationDate, combined.reservation.CheckedInAt))
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<ReservationEntity>> GetAllByDayAsync(Guid courtId, DateOnly date)
        {
            return await _context.Reservations
                .Where(x => x.CourtId == courtId && x.ReservationDate == date)
                .ToListAsync();
        }

        public async Task<(int count, IReadOnlyList<MyReservation> items)> GetMyReservationsAsync(Guid playerId, int skip, int pageSize)
        {
            var items =  await _context.Reservations
                .AsNoTracking()
                .Where(x => x.PlayerId == playerId)
                .Join(_context.FootballCourts, 
                    reservation => reservation.CourtId, 
                    court => court.Id, 
                    (reservation, court) => new MyReservation
                    (
                        reservation.Id,
                        playerId,
                        reservation.CourtId,
                        court.Name,
                        court.ProfileImage,
                        court.Type,
                        reservation.TimeSlotNum,
                        reservation.Status,
                        reservation.ReservationDate
                ))
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            
            var count = await _context.Reservations
                .AsNoTracking()
                .Where(x => x.PlayerId == playerId)
                .Join(_context.FootballCourts, 
                    reservation => reservation.CourtId, 
                    court => court.Id, 
                    (reservation, court) => new MyReservation
                    (
                        reservation.Id,
                        playerId,
                        reservation.CourtId,
                        court.Name,
                        court.ProfileImage,
                        court.Type,
                        reservation.TimeSlotNum,
                        reservation.Status,
                        reservation.ReservationDate
                )).CountAsync();

            return (count, items);
        }
    }
}