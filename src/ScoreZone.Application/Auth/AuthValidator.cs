using FluentValidation;

namespace ScoreZone.Application.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.firstName)
                .NotEmpty()
                .WithMessage("First Name Field is Required.");
            
            RuleFor(x => x.lastName)
                .NotEmpty()
                .WithMessage("Last Name Field is Required.");
            
            RuleFor(x => x.gender)
                .IsInEnum()
                .WithMessage("Gender is Required and Must Be Valid Value.");
            
            RuleFor(x => x.birthDate)
                .NotEmpty()
                .WithMessage("Birth Date Field is Required.");

            RuleFor(x => x.username)
                .NotEmpty()
                .WithMessage("Username Field is Required.");

            // TODO: MAKE IT STRONG PASSWORD 
            RuleFor(x => x.password)
                .NotEmpty()
                .WithMessage("Password Field is Required.");

            RuleFor(x => x.phone)
                .NotEmpty()
                .WithMessage("Phone Field is Required.");

            RuleFor(x => x.role)
                .NotEmpty()
                .WithMessage("Role Field is Required.");
        }
    }
}