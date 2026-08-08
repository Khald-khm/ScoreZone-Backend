using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.DTOs;

namespace ScoreZone.Application.Reservation.Interfaces
{
    public interface IReservationService
    {
        Task<AppResult> AddAsync(AddReservationRequest request);

        Task<AppResult> UpdateAsync(Guid id, UpdateReservationRequest request);

        Task<AppResult<ReservationDetails>> GetDetailsByIdAsync(Guid id);

        Task<AppResult<IReadOnlyList<ReservedSlots>>> GetReservedSlotsAsync(ViewAvailableSlotsRequest request);

        Task<AppResult<PaginatedResultDto<MyReservation>>> GetMyReservationsAsync(int pageNumber, int pageSize);

        Task<AppResult> PayDepositeAsync(Guid id, PayDepositeRequest request);

        Task<AppResult> DailyReservationsAsync(DateOnly date);

        Task<AppResult> CheckInAsync(Guid reservationId, Guid playerId, int? completePayment);

        Task<AppResult<IReadOnlyList<SearchReservationDetails>>> Search(string searchWord);
        
    }
    
}