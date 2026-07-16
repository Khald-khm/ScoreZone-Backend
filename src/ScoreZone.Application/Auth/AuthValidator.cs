using FluentValidation;

namespace ScoreZone.Application.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.firstName)
                .NotEmpty()
                .WithMessage("First Name Field is Required.")
                .MinimumLength(3)
                .WithMessage("Username Field Must Be More Than 2 Characters.");
            
            RuleFor(x => x.lastName)
                .NotEmpty()
                .WithMessage("Last Name Field is Required.")
                .MinimumLength(3)
                .WithMessage("Username Field Must Be More Than 2 Characters.");
            
            RuleFor(x => x.gender)
                .IsInEnum()
                .WithMessage("Gender is Required and Must Be Valid Value.");
            
            RuleFor(x => x.birthDate)
                .NotEmpty()
                .WithMessage("Birth Date Field is Required.")
                .LessThan(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Birth Date Must Be Before Today.");

            // RuleFor(x => x.username)
            //     .NotEmpty()
            //     .WithMessage("Username Field is Required.")
            //     .MinimumLength(3)
            //     .WithMessage("Username Field Must Be More Than 2 Characters.")
            //     .Must(x => !x.Contains(" "))
            //     .WithMessage("Username Field Must Not Contain Any Spaces");

            RuleFor(x => x.password)
                .NotEmpty()
                .WithMessage("Password Field is Required.")
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Password Must Be Strong");

            RuleFor(x => x.phone)
                .NotEmpty()
                .WithMessage("Phone Field is Required.");

            RuleFor(x => x.role)
                .NotEmpty()
                .WithMessage("Role Field is Required.")
                .IsInEnum()
                .WithMessage("Role Field Must Be Valid Value.");
        }
    }
}