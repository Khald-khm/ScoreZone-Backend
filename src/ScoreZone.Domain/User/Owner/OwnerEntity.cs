using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;
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
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
        }
        
    }
}