using ScoreZone.Domain.Shared.Entities;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;

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
            BusinessRules(identityId, firstName, lastName, phoneNumber, city, address);

            IdentityId = identityId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
            ProfileImage = profileImage ?? null;
        }



        public void Update(string identityId, string firstName, string lastName, string phoneNumber, City city, string address)
        {
            BusinessRules(identityId, firstName, lastName, phoneNumber, city, address);

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            City = city;
            Address = address;
        }


        private static void BusinessRules(string identityId, string firstName, string lastName, string phoneNumber,
                     City city, string address)
        {
            if(string.IsNullOrWhiteSpace(identityId))
                throw new DomainException(400, "Identity Id Field is Required.");

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