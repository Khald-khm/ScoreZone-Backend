using ScoreZone.Domain.Reservation.Enums;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Reservation.DTOs
{
    public record ReservationDetails(
        Guid id,
        Guid playerId,
        Guid courtId,
        string courtName,
        string? courtImage,
        CourtType courtType,
        City courtCity,
        int pricePerMatch,
        string facilityName,
        int timeSlotNum,
        ReservationStatus status,
        int deposite,
        int payment,
        DateOnly reservationDate,
        DateTime? checkedInAt
    );

    public record MyReservation(
        Guid id,
        Guid playerId,
        Guid courtId,
        string courtName,
        string? courtImage,
        CourtType courtType,
        int timeSlotNum,
        ReservationStatus status,
        DateOnly reservationDate
    );

    public record ReservedSlots(
        Guid id,
        int timeSlotNum
    );

    public record SearchReservationDetails(
        Guid id,
        Guid playerId,
        Guid courtId,
        string firstName,
        string lastName,
        string phoneNumber,
        string courtName,
        string? courtImage,
        CourtType courtType,
        City courtCity,
        int pricePerMatch,
        string facilityName,
        int timeSlotNum,
        ReservationStatus status,
        int deposite,
        int payment,
        DateOnly reservationDate,
        DateTime? checkedInAt
    );
}