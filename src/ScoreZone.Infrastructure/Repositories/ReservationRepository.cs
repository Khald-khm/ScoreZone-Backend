using Microsoft.EntityFrameworkCore;
using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Application.Reservation.Interfaces;
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
            await _context.Reservations.AddAsync(court);
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
                        combined.court.ProfileImage, combined.court.Type, combined.court.City, combined.reservation.PricePerMatch, facility.Name, 
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
            var query =  _context.Reservations
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
                ));
            
            var count = await query.CountAsync();

            var items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (count, items);
        }


        public async Task<IReadOnlyList<ReservationDetails>> DailyReservations(DateOnly date, List<Guid> courtIds)
        {
            var reservations = await _context.Reservations
                .AsNoTracking()
                .Where(x => courtIds.Contains(x.CourtId) && x.ReservationDate == date)
                .OrderBy(x => x.TimeSlotNum)
                .Select(res => new ReservationDetails(
                        res.Id, res.PlayerId, res.CourtId, res.FootballCourt.Name , 
                        res.FootballCourt.ProfileImage, res.FootballCourt.Type, res.FootballCourt.City, res.PricePerMatch, res.FootballCourt.Facility.Name, 
                        res.TimeSlotNum, res.Status, res.Deposite, 
                        res.Payment, res.ReservationDate, res.CheckedInAt))
                .ToListAsync();
            
            return reservations;
        }


        public async Task<IReadOnlyList<SearchReservationDetails>> Search(string searchWord)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Include(x => x.Player)
                .Include(x => x.FootballCourt)
                    .ThenInclude(x => x.Facility)
                .Join(_context.Players, 
                    reservation => reservation.PlayerId, 
                    player => player.Id, (reservation, player) => new SearchReservationDetails(reservation.Id, reservation.PlayerId, reservation.CourtId, reservation.Player.FirstName, reservation.Player.LastName, reservation.Player.PhoneNumber, reservation.FootballCourt.Name , 
                        reservation.FootballCourt.ProfileImage, reservation.FootballCourt.Type, reservation.FootballCourt.City, reservation.PricePerMatch, reservation.FootballCourt.Facility.Name, 
                        reservation.TimeSlotNum, reservation.Status, reservation.Deposite, 
                        reservation.Payment, reservation.ReservationDate, reservation.CheckedInAt))
                .Where(x => x.firstName.Contains(searchWord) || searchWord.Contains(x.firstName) || x.lastName.Contains(searchWord) || searchWord.Contains(x.lastName) || x.phoneNumber.Contains(searchWord) || searchWord.Contains(x.phoneNumber))
                .ToListAsync();

        }
    }
}