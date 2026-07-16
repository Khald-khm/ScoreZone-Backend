using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Domain.Facility;
using ScoreZone.Domain.Reservation;

namespace ScoreZone.Application.Reservation.Mappings
{
    internal static class ReservationToDto
    {

        public static ReservedSlots ToDto(this ReservationEntity entity)
        => new(entity.CourtId, entity.TimeSlotNum);
    }
}