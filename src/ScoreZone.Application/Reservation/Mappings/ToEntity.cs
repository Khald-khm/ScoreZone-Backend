using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Domain.Reservation;

namespace ScoreZone.Application.Reservation.Mappings
{
    internal static class ReservationToEntity
    {

        public static ReservationEntity ToEntity(this AddUpdateReservationRequest request)
        => new(request.playerId, request.courtId, request.timeSlotNum, request.status, request.reservationDate);
    }
}