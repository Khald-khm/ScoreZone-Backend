using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.User.Owner;

namespace ScoreZone.Domain.User.Employee
{
    public class EmployeeEntity : Entity
    {
        public string IdentityId { get; set; } = null!;
        public Guid OwnerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public City City { get; set; }
        public string Address { get; set; } = null!;
        public string? ProfileImage { get; set; } = null;
        

        // Navigation Property
        public OwnerEntity Owner { get; set; } = null!;


        private EmployeeEntity() {} // For EF Core


        public EmployeeEntity(Guid ownerId, string firstName, string lastName, string phoneNumber, City city, string address, string? profileImage)
        {
            OwnerId = ownerId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
            ProfileImage = profileImage ?? null;
        }



        public void Update(string firstName, string lastName, string phoneNumber, City city, string address, string? profileImage)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
            ProfileImage = profileImage ?? null;
        }
        
    }
}