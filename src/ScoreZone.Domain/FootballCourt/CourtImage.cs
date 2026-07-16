using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Domain.FootballCourt
{
    public class CourtImage : Entity
    {
        public Guid CourtId { get; set; }
        public string ImageUrl { get; set; } = null!;
        
        // Navigation Property
        public FootballCourtEntity FootballCourt { get; set; } = null!;


        private CourtImage() {} // For EF Core

        
        public void Add(Guid courtId, string imageUrl)
        {
            if(courtId == Guid.Empty)
                throw new DomainException(400, "Court Id is Required.");
            if(string.IsNullOrWhiteSpace(imageUrl))
                throw new DomainException(400, "Image is Required.");

            CourtId = courtId;
            ImageUrl = imageUrl;
        }
    }
}