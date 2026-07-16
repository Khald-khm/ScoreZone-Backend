using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Domain.Facility
{
    public class FacilityImage : Entity
    {
        public Guid FacilityId { get; set; }
        public string ImageUrl { get; set; } = null!;

        
        // Navigation Property
        public FacilityEntity Facility { get; set; } = null!;
        

        private FacilityImage(){} // For EF Core
        public void Add(Guid facilityId, string imageUrl)
        {
            if(facilityId == Guid.Empty)
                throw new DomainException(400, "Facility Id is Required.");
            if(string.IsNullOrWhiteSpace(imageUrl))
                throw new DomainException(400, "Image is Required.");

            FacilityId = facilityId;
            ImageUrl = imageUrl;
        }
    }
}