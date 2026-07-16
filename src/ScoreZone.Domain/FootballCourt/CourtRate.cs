using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Exceptions;
using ScoreZone.Domain.User.Player;

namespace ScoreZone.Domain.FootballCourt
{
    public class CourtRate : Entity
    {
        public Guid CourtId { get; set; }
        public Guid PlayerId { get; set; }
        public int Rate { get; set; }

        // Navigation Property
        public FootballCourtEntity FootballCourt { get; set; } = null!;
        public PlayerEntity Player { get; set; } = null!;
        
        private CourtRate() {} // For EF Core

        public void Add(Guid courtId, Guid playerId, int rate)
        {
            if(courtId == Guid.Empty)
                throw new DomainException(400, "Court Id is Required.");
            if(playerId == Guid.Empty)
                throw new DomainException(400, "Player Id is Required.");
            if(rate <= 0)
                throw new DomainException(400, "Rate is Required.");

            CourtId = courtId;
            PlayerId = playerId;
            Rate = rate;
        }
    }
}