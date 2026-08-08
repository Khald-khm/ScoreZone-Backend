using ScoreZone.Domain.Facility;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Reservation;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Domain.User.Player
{
    public class PlayerEntity : Entity
    {
        public string IdentityId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public City City { get; set; }
        public string Address { get; set; } = null!;
        public string? ProfileImage { get; set; } = null;

        // Navigation Property
        public List<CourtRate> CourteRates { get; set; } = new();

        public List<FacilityRate> FacilityRates { get; set; } = new();

        public List<FavoriteCourt> FavoriteCourts { get; set; } = new();

        public List<ReservationEntity> Reservations { get; set; } = new();


        private PlayerEntity() {} // For EF Core

        public PlayerEntity(string identityId, string firstName, string lastName, string phoneNumber, City city, string address, string? profileImage)
        {
            BusinessRules(firstName, lastName, phoneNumber, city, address);

            if(string.IsNullOrWhiteSpace(identityId))
                throw new DomainException(400, "Identity Id Field is Required.");

            IdentityId = identityId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
            ProfileImage = profileImage ?? null;
        }



        public void Update(string firstName, string lastName, string phoneNumber, City city, string address)
        {
            BusinessRules(firstName, lastName, phoneNumber, city, address);

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
        }

        private static void BusinessRules(string firstName, string lastName, string phoneNumber, City city, string address)
        {

            if(string.IsNullOrWhiteSpace(firstName))
                throw new DomainException(400, "First Name Field is Required.");
            
            if(string.IsNullOrWhiteSpace(lastName))
                throw new DomainException(400, "Last Name Field is Required.");
            
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException(400, "Phone Number Field is Required.");
            
            if(!Enum.IsDefined(typeof(City), city))
                throw new DomainException(400, "City Field is Required.");
            
            if(string.IsNullOrWhiteSpace(address))
                throw new DomainException(400, "Address Field is Required With Valid Value.");
            
        }
        
    }
}