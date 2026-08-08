using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;
using ScoreZone.Domain.User.Employee;

namespace ScoreZone.Domain.User.Owner
{
    public class OwnerEntity : Entity
    {
        public string IdentityId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public City City { get; set; }
        public string Address { get; set; } = null!;
        public string? ProfileImage { get; set; } = null;
        
        // Navigation Property
        public List<FootballCourtEntity> FootballCourts { get; set; } = new ();

        public List<EmployeeEntity> Employees { get; set; } = new ();


        private OwnerEntity() {} // For EF Core


        public OwnerEntity(string identityId, string firstName, string lastName, string phoneNumber, City city, string address, string? profileImage)
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

        private static void BusinessRules(string firstName, string lastName, string phoneNumber,
                     City city, string address)
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