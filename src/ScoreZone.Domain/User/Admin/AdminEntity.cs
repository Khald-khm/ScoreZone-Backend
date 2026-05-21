using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Domain.User.Admin
{
    public class AdminEntity : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public City Location { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }
        public AdminStatus Status { get; set; }



        public AdminEntity(string firstName, string lastName, string phoneNumber, City location, string address, string profileImage)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Location = location;
            Address = address;
            ProfileImage = profileImage;
        }



        public void Update(string firstName, string lastName, string phoneNumber, City location, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Location = location;
            Address = address;
        }
        
    }
}