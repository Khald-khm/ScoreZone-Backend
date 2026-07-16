using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Exceptions;
using ScoreZone.Domain.User.Player;

namespace ScoreZone.Domain.Facility
{
    public class FacilityRate : Entity
    {
        public Guid FacilityId { get; set; }
        public Guid PlayerId { get; set; }
        public int Rate { get; set; }

        // Navigation Property
        public FacilityEntity Facility { get; set; } = null!;
        public PlayerEntity Player { get; set; } = null!;
        

        private FacilityRate() {} // For EF Core

        public void Add(Guid facilityId, Guid playerId, int rate)
        {
            if(facilityId == Guid.Empty)
                throw new DomainException(400, "Court Id is Required.");
            if(playerId == Guid.Empty)
                throw new DomainException(400, "Player Id is Required.");
            if(rate <= 0)
                throw new DomainException(400, "Rate is Required.");

            FacilityId = facilityId;
            PlayerId = playerId;
            Rate = rate;
        }
    }
}