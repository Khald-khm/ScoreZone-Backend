using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Domain.Facility
{
    public class FacilityEntity : Entity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public City City { get; set; }
        public string Address { get; set; } = null!;
        public string? ProfileImage { get; set; }
        public double? LocationLat { get; set; }
        public double? LocationLng { get; set; }
        public int rating { get; set; }
        public FacilityStatus Status { get; set; }

        // Navigation Property
        public List<FootballCourtEntity> FootballCourts { get; set; } = new ();
        public List<FacilityImage> FacitlityImages { get; set; } = new();



        private FacilityEntity() {} // For EF Core
        
        public FacilityEntity(string name, string? description, string phoneNumber, City city, string address, 
                string? profileImage, double? locationLat, double? locationLng, FacilityStatus status)
        {

            BusinessRules(name, phoneNumber, city, address, locationLat, locationLng, status);

            Name = name;
            Description = description ?? null;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
            ProfileImage = profileImage ?? null;
            LocationLat = locationLat ?? null;
            LocationLng = locationLng ?? null;
            rating = 0;
            Status = status;
        }


        public void Update(string name, string? description, string phoneNumber, City city, string address, 
                string? profileImage, double? locationLat, double? locationLng, FacilityStatus status)
        {

            BusinessRules(name, phoneNumber, city, address, locationLat, locationLng, status);

            Name = name;
            Description = description ?? null;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
            ProfileImage = profileImage ?? null;
            LocationLat = locationLat ?? null;
            LocationLng = locationLng ?? null;
            rating = 0;
            Status = status;
        }


        // Change Status
        public void Pend()
        {
            Status = FacilityStatus.Pending;
        }
        public void Accept()
        {
            Status = FacilityStatus.Active;
        }
        public void Reject()
        {
            Status = FacilityStatus.Rejected;
        }
        public void Block()
        {
            Status = FacilityStatus.Blocked;
        }


        private static void BusinessRules(string name, string phoneNumber, City city, string address, 
                double? locationLat, double? locationLng, FacilityStatus status)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new DomainException(400, "Name Field is Required.");
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException(400, "Phone Number Field is Required.");
            if(!Enum.IsDefined(typeof(City), city))
                throw new DomainException(400, "City Field is Required.");
            if(string.IsNullOrWhiteSpace(address))
                throw new DomainException(400, "Address Field is Required.");
            if(locationLat <= 0)
                throw new DomainException(400, "Location Latitude Field is Required & Must Be Greater Than 0.");
            if(locationLng <= 0)
                throw new DomainException(400, "Location Longitude Field is Required & Must Be Greater Than 0.");
            if(!Enum.IsDefined(typeof(FacilityStatus), status))
                throw new DomainException(400, "Status Field is Required.");
        }
        
    }
}