using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Domain.Reservation;

namespace ScoreZone.Application.Reservation.Mappings
{
    internal static class ReservationToEntity
    {

        public static ReservationEntity ToEntity(this AddReservationRequest request, Guid playerId, int pricePerMatch)
        => new(playerId, request.courtId, request.timeSlotNum, pricePerMatch, request.reservationDate, request.status);

        public static ReservationEntity ToEntity(this UpdateReservationRequest request, Guid playerId, int pricePerMatch)
        => new(playerId, request.courtId, request.timeSlotNum, pricePerMatch, request.reservationDate, request.status);
    }
}