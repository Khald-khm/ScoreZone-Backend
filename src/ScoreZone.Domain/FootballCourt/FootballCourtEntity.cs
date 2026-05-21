using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Domain.FootballCourt
{
    public class FootballCourtEntity : Entity
    {
        public Guid FacilityId { get; set; }
        public string PhoneNumber { get; set; }
        public City Location { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }
        public CourtStatus Status { get; set; }



        public FootballCourtEntity(Guid facilityId, string phoneNumber, City location, string address, string profileImage)
        {
            FacilityId = facilityId;
            PhoneNumber = phoneNumber;
            Location = location;
            Address = address;
            ProfileImage = profileImage;
        }



        public void Update(Guid facilityId, string phoneNumber, City location, string address, string profileImage, CourtStatus status)
        {
            FacilityId = facilityId;
            PhoneNumber = phoneNumber;
            Location = location;
            Address = address;
            ProfileImage = profileImage;
            Status = status;
        }
        
    }
}