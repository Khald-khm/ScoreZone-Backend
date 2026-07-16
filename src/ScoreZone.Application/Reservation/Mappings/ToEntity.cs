using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Domain.Facility;
using ScoreZone.Domain.Reservation;

namespace ScoreZone.Application.Reservation.Mappings
{
    internal static class ReservationToEntity
    {

        public static ReservationEntity ToEntity(this AddReservationRequest request)
        => new(request.playerId, request.courtId, request.timeSlotNum, request.status, request.reservationDate);
    }
}