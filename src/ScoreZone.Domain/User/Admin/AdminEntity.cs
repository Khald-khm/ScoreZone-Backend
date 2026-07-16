using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Domain.User.Admin
{
    public class AdminEntity : Entity
    {
        public string IdentityId { get; set; } = null!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string? ProfileImage { get; set; } = null;



        public AdminEntity(string identityId, string firstName, string lastName, string phoneNumber, City city, string address, string? profileImage)
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