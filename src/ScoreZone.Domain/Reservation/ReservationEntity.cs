using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Reservation.Enums;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Exceptions;
using ScoreZone.Domain.User.Player;

namespace ScoreZone.Domain.Reservation
{
    public class ReservationEntity : Entity
    {
        public Guid PlayerId { get; set; }
        public Guid CourtId { get; set; }
        public int TimeSlotNum { get; set; }
        public ReservationStatus Status { get; set; }
        public int Deposite { get; set; }
        public int Payment { get; set; }
        public DateOnly ReservationDate { get; set; }
        public DateTime? CheckedInAt { get; set; }

        // Navigation Property
        public FootballCourtEntity FootballCourt { get; set; } = null!;
        public PlayerEntity Player { get; set; } = null!;


        private ReservationEntity() {} // For EF Core

        public ReservationEntity(Guid playerId, Guid courtId, int timeSlotNum, ReservationStatus status, DateOnly reservationDate)
        {
            BusinessRules(playerId, courtId, timeSlotNum, status, reservationDate);

            PlayerId = playerId;
            CourtId = courtId;
            TimeSlotNum = timeSlotNum;
            Status = status;
            Deposite = 0;
            Payment = 0;
            ReservationDate = reservationDate;
        }

        public void Update(ReservationEntity reservation)
        {
            BusinessRules(reservation.PlayerId, reservation.CourtId, reservation.TimeSlotNum, reservation.Status, reservation.ReservationDate);

            PlayerId = reservation.PlayerId;
            CourtId = reservation.CourtId;
            TimeSlotNum = reservation.TimeSlotNum;
            Status = reservation.Status;
            ReservationDate = reservation.ReservationDate;
        }

        private void BusinessRules(Guid playerId, Guid courtId, int timeSlotNum, ReservationStatus status, DateOnly reservationDate)
        {
            if(playerId == Guid.Empty)
                throw new DomainException(400, "Player Id Field is Required.");
            if(courtId == Guid.Empty)
                throw new DomainException(400, "Court Id Field is Required.");
            if(timeSlotNum <= 0)
                throw new DomainException(400, "Time Slot Number is Required.");
            if(reservationDate < DateOnly.FromDateTime(DateTime.Now))
                throw new DomainException(400, "Reservation Date Cannot Be in The Past.");
            
        }


        public void PayDeposite(int depositeAmount)
        {
            if(depositeAmount <= 0)
                throw new DomainException(400, "Deposite Must Be Greater Than Zero.");
                
            Deposite = depositeAmount;
        }

        public void CompletePayment (int payAmount)
        {
            if(payAmount <= 0)
                throw new DomainException(400, "Payment Must Be Greater Than Zero.");
            Payment = payAmount;
        }
        
    }
}