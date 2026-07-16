using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Application.FootballCourt.DTOs;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Application.Shared.DTOs;

namespace ScoreZone.Application.Reservation.Interfaces
{
    public interface IReservationService
    {
        Task<AppResult> AddAsync(AddReservationRequest request);

        Task<AppResult<ReservationDetails>> GetDetailsByIdAsync(Guid id);

        Task<AppResult<IReadOnlyList<ReservedSlots>>> ViewReservedSlotsAsync(ViewAvailableSlotsRequest request);

        Task<AppResult<PaginatedResultDto<MyReservation>>> GetMyReservationsAsync(Guid playerId, int pageNumber, int pageSize);

        Task<AppResult> PayDepositeAsync(PayDepositeRequest request);
        
    }
    
}