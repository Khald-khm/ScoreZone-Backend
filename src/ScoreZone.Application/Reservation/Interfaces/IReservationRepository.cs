using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Domain.Reservation;

namespace ScoreZone.Application.Reservation.Interfaces
{
    public interface IReservationRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(ReservationEntity facility);

        Task<ReservationEntity?> GetByIdAsync(Guid id);

        Task<ReservationDetails?> GetDetailsByIdAsync(Guid id);

        Task<IReadOnlyList<ReservationEntity>> GetAllByDayAsync(Guid courtId, DateOnly date);

        Task<(int count, IReadOnlyList<MyReservation> items)> GetMyReservationsAsync(Guid playerId, int skip, int pageSize);
        
    }
    
}