using ScoreZone.Domain.Reservation.Enums;

namespace ScoreZone.Application.Reservation.DTOs
{
    public record AddReservationRequest(
        Guid playerId, 
        Guid courtId, 
        int timeSlotNum, 
        ReservationStatus status,
        DateOnly reservationDate
    );

    public record ViewAvailableSlotsRequest(
        Guid courtId, DateOnly date
    );

    public record PayDepositeRequest(
        Guid reservationId, int depositeAmount
    );
}