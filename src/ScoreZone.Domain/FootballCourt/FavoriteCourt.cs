using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Exceptions;
using ScoreZone.Domain.User.Player;

namespace ScoreZone.Domain.FootballCourt
{
    public class FavoriteCourt : Entity
    {
        public Guid CourtId { get; set; }
        public Guid PlayerId { get; set; }
        
        // Navigation Property
        public FootballCourtEntity FootballCourt { get; set; } = null!;
        public PlayerEntity Player { get; set; } = null!;

        private FavoriteCourt() {} // For EF Core

        
        public void Add(Guid courtId, Guid playerId)
        {
            if(courtId == Guid.Empty)
                throw new DomainException(400, "Court Id is Required.");
            if(playerId == Guid.Empty)
                throw new DomainException(400, "Player Id is Required.");
            
            CourtId = courtId;
            PlayerId = playerId;
        }
    }
}