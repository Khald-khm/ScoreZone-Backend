using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Domain.Reservation;

namespace ScoreZone.Application.Reservation.Mappings
{
    internal static class ReservationToDto
    {

        public static ReservedSlots ToDto(this ReservationEntity entity)
        => new(entity.Id, entity.TimeSlotNum);
    }
}