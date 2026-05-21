using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Domain.Facility
{
    public class FacilityEntity : Entity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public City Location { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }
        public FacilityStatus Status { get; set; }
        private readonly List<FootballCourtEntity> _footballCourts = new ();
        public IReadOnlyList<FootballCourtEntity> FootballCourts => _footballCourts.AsReadOnly();



        public FacilityEntity(string name, string phoneNumber, City location, string address, string profileImage)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Location = location;
            Address = address;
            ProfileImage = profileImage;
        }

        

        public void Update(string name, string phoneNumber, City location, string address, string profileImage, FacilityStatus status)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Location = location;
            Address = address;
            ProfileImage = profileImage;
            Status = status;
        }
        
    }
}