using System.ComponentModel.DataAnnotations;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ScoreZone.Infrastructure.Auth.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Gender Gender { get; private set; }
        public DateOnly BirthDate { get; private set; }

        public ICollection<RefreshToken> RefreshTokens = 
                new List<RefreshToken>();

        // Username => Already in IdentityUser
        // Phone => Already in IdentityUser
        // Email => Already in IdneityUser


        private AppUser() {} // needed by Identity/EF Core

        public AppUser(string firstName, string lastName, string username, string phoneNumber,
                    string? email, Gender gender, DateOnly birthDate)
        {
            BusinessRules(firstName, lastName, username, phoneNumber, gender, birthDate);

            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            
            UserName = username;
            PhoneNumber = phoneNumber;
            Email = email;
        }


        private static void BusinessRules(string firstName, string lastName, string username, string phoneNumber, 
                    Gender gender, DateOnly birthDate)
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new DomainException(400, "First Name Field is Required.");
            
            if(string.IsNullOrWhiteSpace(lastName))
                throw new DomainException(400, "Last Name Field is Required.");

            if(string.IsNullOrWhiteSpace(username))
                throw new DomainException(400, "Username Field is Required.");
            
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException(400, "Phone Number Field is Required.");
            
            if(!Enum.IsDefined(typeof(Gender), gender))
                throw new DomainException(400, "Gender Field is Required With Valid Value.");
            
            if(birthDate == default)
                throw new DomainException(400, "Birth Date Field is Required.");
            
        }

    }
}