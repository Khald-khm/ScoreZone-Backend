using ScoreZone.Domain.Facility;
using ScoreZone.Domain.Reservation;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;
using ScoreZone.Domain.User.Employee;
using ScoreZone.Domain.User.Owner;

namespace ScoreZone.Domain.FootballCourt
{
    public class FootballCourtEntity : Entity
    {
        public Guid FacilityId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public City City { get; set; }
        public string Address { get; set; } = null!;
        public string? ProfileImage { get; set; }
        public int rating { get; set; }
        public CourtType Type { get; set; }
        public int Capacity { get; set; }
        public int PricePerMatch { get; set; }
        public bool IsPartialAllowed { get; set; }
        public double LocationLat { get; set; }
        public double LocationLng { get; set; }
        public CourtStatus Status { get; set; }

        // DON'T FORGET SPECIAL COURTS
        

        // Navigation Property
        public List<CourtImage> CourtImages { get; set; } = new();
        public List<ReservationEntity> Reservations { get; set; } = new();
        public FacilityEntity Facility { get; set; } = null!;
        public OwnerEntity Owner { get; set; } = null!;
        public List<EmployeeEntity> Employees { get; set; } = new();


        private FootballCourtEntity(){} // For EF Core

        public FootballCourtEntity(Guid facilityId, Guid ownerId, string name, string phoneNumber, 
                    City city, string address, string? profileImage, CourtType type, int capacity,
                    int pricePerMatch, bool isPartialAllowed, double locationLat, double locationLng, CourtStatus status)
        {
            BusinessRules(facilityId, name, phoneNumber, city, address, profileImage, type, 
                    capacity, pricePerMatch, isPartialAllowed, locationLat, locationLng, status);

            FacilityId = facilityId;
            OwnerId = ownerId;
            Name = name;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
            ProfileImage = profileImage ?? null;
            rating = 0;
            Type = type;
            Capacity = capacity;
            PricePerMatch = pricePerMatch;
            IsPartialAllowed = isPartialAllowed;
            LocationLat = locationLat;
            LocationLng = locationLng;
            Status = status;
        }



        public void Update(Guid facilityId, string name, string phoneNumber, City city, 
                    string address, string? profileImage, CourtType type, int capacity, int pricePerMatch, 
                    bool isPartialAllowed, double locationLat, double locationLng, CourtStatus status)
        {

            BusinessRules(facilityId, name, phoneNumber, city, address, profileImage, type, 
                    capacity, pricePerMatch, isPartialAllowed, locationLat, locationLng, status);
                    
            FacilityId = facilityId;
            Name = name;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
            ProfileImage = profileImage ?? null;
            rating = 0;
            Type = type;
            Capacity = capacity;
            PricePerMatch = pricePerMatch;
            IsPartialAllowed = isPartialAllowed;
            LocationLat = locationLat;
            LocationLng = locationLng;
            Status = status;
        }



        // Change Status
        public void Pend()
        {
            Status = CourtStatus.Pending;
        }
        public void Accept()
        {
            Status = CourtStatus.Active;
        }
        public void Reject()
        {
            Status = CourtStatus.Rejected;
        }
        public void Block()
        {
            Status = CourtStatus.Blocked;
        }




        public void BusinessRules(Guid facilityId, string name, string phoneNumber, City city, 
                    string address, string? profileImage, CourtType type, int capacity, int pricePerMatch, 
                    bool isPartialAllowed, double locationLat, double locationLng, CourtStatus status)
        {
            if(facilityId == Guid.Empty)
                throw new DomainException(400, "Facility Id Field is Required.");
            if(string.IsNullOrWhiteSpace(name))
                throw new DomainException(400, "Name Field is Required.");
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException(400, "Phone Number Field is Required.");
            if(!Enum.IsDefined(typeof(City), city))
                throw new DomainException(400, "City Field is Required.");
            if(string.IsNullOrWhiteSpace(address))
                throw new DomainException(400, "Address Field is Required.");
            if(!Enum.IsDefined(typeof(CourtType), type))
                throw new DomainException(400, "Type Field is Required.");
            if(capacity <= 0)
                throw new DomainException(400, "Capacity Field is Required & Must Be Greater Than 0.");
            if(pricePerMatch <= 0)
                throw new DomainException(400, "Price Per Match Field is Required & Must Be Greater Than 0.");
            if(locationLat <= 0)
                throw new DomainException(400, "Location Latitude Field is Required & Must Be Greater Than 0.");
            if(locationLng <= 0)
                throw new DomainException(400, "Location Longitude Field is Required & Must Be Greater Than 0.");
            if(!Enum.IsDefined(typeof(CourtStatus), status))
                throw new DomainException(400, "Status Field is Required.");
        }
        
    }
}